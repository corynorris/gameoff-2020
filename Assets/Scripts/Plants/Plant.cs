using UnityEngine;
public abstract class Plant : CellController
{
    [Tooltip("Rate at which growth slows down based on age n^2.")]
    public float turnDelayBetweenGrowth = 1f;

    [Tooltip("Rate at which growth slows down based on age delayBetweenGrowth^rateOfGrowthDecay.")]
    public float rateOfGrowthDecay = 0f;

    [Tooltip("Number of turns before the plant dies, 0 for infinite.")]
    public int maxTurnsAlive = 0;

    [Tooltip("Number of turns before the plant stops claiming new tiles, 0 for infinite.")]
    public int maxTurnsOfGrowing = 0;

    [Tooltip("Max neighbours before the plant dies.")]
    public int maxNeighboursBeforeOvercrowding = 8;

    [Tooltip("Amount of gas produced each growth period")]
    public int gasProduction = 2;

    [Tooltip("Amount of gas consumed each growth period")]
    public int gasConsumption = 1;

    [Tooltip("Amount of gas produced each growth period")]
    public Gas gasConsumptionType = Gas.Argon;

    [Tooltip("Amount of gas produced each growth period")]
    public Gas gasProductionType = Gas.Oxygen;

    protected int turnsAlive = 0;
    protected int turnsOfGrowth = 0;
    protected int turnsUntilGrowth = 0;


    public abstract void Grow();

    public override void MakeClaims()
    {
        turnsAlive++;

        if (maxTurnsAlive > 0 && turnsAlive > maxTurnsAlive )
        {
            Debug.Log("Died from age");
            this.Kill();
            return;
        }

        if (maxTurnsOfGrowing > 0 && turnsAlive > maxTurnsOfGrowing)
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


        if (turnsUntilGrowth-- <= 0)
        {
            if(this.Grid.ResourceController.GetTotalGas(this.gasConsumptionType) >= this.gasConsumption)
            {
                turnsOfGrowth++;
                this.Grid.ResourceController.ConsumeGas(this.gasConsumptionType, this.gasConsumption);
                Grow();

                turnsUntilGrowth = Mathf.FloorToInt(turnDelayBetweenGrowth * Mathf.Exp(rateOfGrowthDecay * turnsOfGrowth));
            }
            
        } 
    }

    public override void ProduceEffects()
    {
        // TODO: send the gas to be produced somewhere
        if(this.Grid.ResourceController.GetTotalGas(this.gasConsumptionType) >= this.gasConsumption)
        {
            this.Grid.ResourceController.ConsumeGas(this.gasConsumptionType, this.gasConsumption);
            this.Grid.ResourceController.ProduceGas(this.gasProductionType, this.gasProduction);
        }
        
        

    }





    protected bool CanClaim(CellController cellController)
    {
       return cellController && cellController.IsFree();
    }

}