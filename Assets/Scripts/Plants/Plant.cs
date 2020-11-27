using System;
using UnityEngine;
public abstract class Plant : CellController
{


    [Tooltip("Rate at which growth slows down based on age n^2.")]
    public float turnDelayBetweenGrowth = 1f;

    [Tooltip("Use the parents age to determine growth rate")]
    public bool useParentsGrowthRate = false;

    [Tooltip("Rate at which growth slows down based on age delayBetweenGrowth^rateOfGrowthDecay.")]
    [Range(-0.5f, 0.5f)]
    public float rateOfGrowthDecay = 0f;

    [Tooltip("Number of turns before the plant dies, 0 for infinite.")]
    public int maxTurnsAlive = 0;

    [Tooltip("Number of turns before the plant stops claiming new tiles, 0 for infinite.")]
    public int maxTurnsOfGrowing = 0;

    [Tooltip("Max neighbours before the plant dies.")]
    public int maxNeighboursBeforeOvercrowding = 8;

    [Tooltip("Amount of gas produced each growth period")]
    public int gasProduction = 2;

    [Tooltip("Amount of gas produced each growth period")]
    public Gas gasProductionType = Gas.Oxygen;

    [Tooltip("Amount of gas consumed each growth period")]
    public int gasConsumption = 1;

    [Tooltip("Amount of gas produced each growth period")]
    public Gas gasConsumptionType = Gas.Argon;

    [SerializeField]
    protected int turnsAlive = 0;
    protected int turnsOfGrowth = 0;
    protected int turnsUntilGrowth = 0;

    [SerializeField]
    protected bool bornFromParent = false;
    protected int parentTurnsAlive = 0;
    protected int parentTurnsOfGrowth = 0;
    protected GridDirection spawnDirection;


    public abstract bool Grow();

    private bool isDecaying = false;
    private int turnsUntilDecay;

    private Animator animator;


    public enum NoResourceOptions
    {
        Die, StopGrowing, StopGrowingAndGasProduction, StopGrowingAndAllGas, Decay
    }
    public NoResourceOptions onResourceDepletion = NoResourceOptions.Die;

    protected virtual void Start()
    {
        clickable = false;
        animator = GetComponent<Animator>();
        animator.SetTrigger("spawn");
    }


    public override void MakeClaims()
    {
        Age();

        if (isDecaying)
        {
            Debug.Log("Decaying, won't age");
            return;
        }


        if (maxTurnsAlive > 0 && turnsAlive > maxTurnsAlive)
        {
            Debug.Log("Died from age");
            this.Kill();
            return;
        }

        if (maxTurnsOfGrowing > 0 && turnsOfGrowth > maxTurnsOfGrowing)
        {
            Debug.Log("Too old to grow");
            return;
        }

        if (maxNeighboursBeforeOvercrowding > 0 && numNeighbours > maxNeighboursBeforeOvercrowding)
        {
            Debug.Log("Died from overcrowding");
            this.Kill();
            return;
        }



        if (isAlive && turnsUntilGrowth-- <= 0)
        {
            if (Grow())
            {
                TrackGrowth();
            }

          
        }

    }


    private void Age()
    {
        turnsAlive++;
        parentTurnsAlive++;
    }

    private bool HasGas()
    {
        return this.Grid.ResourceController.GetTotalGas(this.gasConsumptionType) > 0;
    }

    private void TrackGrowth()
    {

        turnsOfGrowth++;
        parentTurnsOfGrowth++;


        if (useParentsGrowthRate)
        {
            this.turnsUntilGrowth = Mathf.FloorToInt(turnDelayBetweenGrowth * Mathf.Exp(rateOfGrowthDecay * parentTurnsOfGrowth));
        }
        else
        {
            this.turnsUntilGrowth = Mathf.FloorToInt(turnDelayBetweenGrowth * Mathf.Exp(rateOfGrowthDecay * turnsOfGrowth));
        }


        this.turnsUntilGrowth = Mathf.Max(0, this.turnsUntilGrowth);
    }

    public override void ProduceEffects()
    {

        if (this.Grid.ResourceController.GetTotalGas(this.gasConsumptionType) <= 0)
        {
            if (onResourceDepletion == NoResourceOptions.Die)
            {
                this.Kill();
            }

            if (onResourceDepletion == NoResourceOptions.StopGrowingAndAllGas)
            {
                this.turnsUntilGrowth++;
            }


            if (onResourceDepletion == NoResourceOptions.StopGrowingAndGasProduction)
            {
                this.turnsUntilGrowth++;
                this.Grid.ResourceController.ConsumeGas(this.gasConsumptionType, this.gasConsumption);
            }

            if (onResourceDepletion == NoResourceOptions.StopGrowing)
            {
                this.turnsUntilGrowth = 2;
                this.Grid.ResourceController.ProduceGas(this.gasProductionType, this.gasProduction);
                this.Grid.ResourceController.ConsumeGas(this.gasConsumptionType, this.gasConsumption);
            }

            if (onResourceDepletion == NoResourceOptions.Decay)
            {
                this.turnsUntilGrowth = 2;

                if (!isDecaying)
                {
                    isDecaying = true;
                    turnsUntilDecay = turnsAlive;
                }
                else
                {
                    turnsUntilDecay -= 2;
                }

                if (turnsUntilDecay <= 0)
                {
                    this.Kill();
                }
            }


        }
        else
        {
            isDecaying = false;
            // TODO: send the gas to be produced somewhere
            this.Grid.ResourceController.ProduceGas(this.gasProductionType, this.gasProduction);
            this.Grid.ResourceController.ConsumeGas(this.gasConsumptionType, this.gasConsumption);
        }

    }


    public override void Initialize(CellController cellParent)
    {
        base.Initialize(cellParent);
        this.turnsAlive = 0;
        this.turnsOfGrowth = 0;

        Plant parent = cellParent as Plant;

        if (parent)
        {
             this.bornFromParent = true;
            this.parentTurnsAlive = parent.turnsAlive;
            this.parentTurnsOfGrowth = parent.turnsOfGrowth;
        }

        TrackGrowth();
    }


    protected override bool ShouldCountAsNeighbour(CellController neighbour)
    {
        if (neighbour is Plant)
        {
            return true;
        }

        return false;
    }


    protected bool CanClaim(CellController cellController)
    {
        return cellController && cellController.IsFree();
    }

}