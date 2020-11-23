using UnityEngine;
using System;
// using GridDirectionExtensions;

public class ClockwisePlant : Plant
{
   public  GridDirection initialDirection = GridDirection.N;
    GridDirection lastDirection;

    protected override void Start()
    {
        base.Start();
        lastDirection = initialDirection;
    }


    private CellController GetNextEmptyNeighbour()
    {
     
        for (int i = 0; i < 8; i++)
        {
            CellController neighbour = this.Grid.GetCellInDirection(this.X, this.Y, lastDirection);
            lastDirection = lastDirection.Next();

            if (CanClaim(neighbour))
            {
             
                return neighbour;
            }


        }
        return null;
    }
    public override bool Grow()
    {
        bool hasGrown = false;
        CellController cellToClaim = GetNextEmptyNeighbour();


        if (cellToClaim != null)
        {   
            cellToClaim.Claim(this);
            hasGrown = true;
        }
        return hasGrown;
    }


}
