using System;
using UnityEngine;
public abstract class Plant : CellController
{
    protected bool CanClaim(CellController cellController)
    {
        return cellController && !cellController.IsClaimed() && cellController.IsEmpty();
    }


    protected int NumPlantsTouching()
    {
        int num = 0;
        foreach (GridDirection direction in GridDirectionHelpers.AllDirections)
        {
            CellController neighbour = this.Grid.GetCellInDirection(this.X, this.Y, direction);
            if (neighbour is Plant)
            {
                num++;
            }
        }
        return num;

    }



    public abstract void ClaimGrowth();
}