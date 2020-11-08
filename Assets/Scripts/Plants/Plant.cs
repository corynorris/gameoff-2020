using System;
using UnityEngine;
public abstract class Plant : CellController, IGrows
{
    public int priority;



    protected bool CanClaim(CellController cellController)
    {
        return cellController && !cellController.IsClaimed() && !cellController.IsEmpty();
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
    public int GetPriority()
    {
        return priority;
    }

    public abstract void ClaimGrowth();



}