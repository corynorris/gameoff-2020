using System;
using UnityEngine;

public class CellController : MonoBehaviour, IComparable
{
    public int priority = 0;
    [HideInInspector] 
    public int numNeighbours = 0;

    private int x;
    private int y;

    private bool isAlive = true;
    private CellController claimant = null;

    private Animator animator;

    public GridController Grid { set; get; }

    public int X
    {
        get { return x; }
        set { x = value; }
    }

    public int Y
    {
        get { return y; }
        set { y = value; }
    }


    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("spawn");
    }

    public void Kill()
    {
        this.isAlive = false;
    }

    public void Claim(CellController claimant)
    {
        this.claimant = claimant;
    }

    public bool HasClaimant()
    {
        return claimant != null;
    }

    public bool IsFree()
    {
        return IsEmpty() && !HasClaimant();
    }

    public CellController GetClaimant()
    {
        return claimant;
    }

    public void Reset()
    {
        this.isAlive = true;
        this.claimant = null;
    }



    public virtual bool IsEmpty()
    {
        return false;
    }


    public int GetPriority()
    {
        return priority;
    }



    public int CompareTo(object obj)
    {
        CellController other = (CellController)obj;
        return other.GetPriority().CompareTo(this.GetPriority());
    }

 

    public bool IsDead()
    {
        return !this.isAlive;
    }


    public void RecalculateNeighbours()
    {
        numNeighbours = 0;
        foreach (GridDirection direction in GridDirectionHelpers.AllDirections)
        {
            CellController neighbour = this.Grid.GetCellInDirection(this.X, this.Y, direction);

            if (neighbour && !neighbour.IsEmpty())
            {
                numNeighbours++;
            }
        }

    }

    // too late to make this abstract w/out refactoring
    public virtual void MakeClaims() { }
    public virtual void ProduceEffects() { }

}