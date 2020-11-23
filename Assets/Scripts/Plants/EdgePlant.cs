using UnityEngine;
using System;
// using GridDirectionExtensions;

public class EdgePlant : Plant
{

    public bool HasNonEdgePlantNeighbour(CellController cell)
    {
        foreach (GridDirection direction in GridDirectionHelpers.AllDirections)
        {                        
            CellController neighbour = cell.Grid.GetCellInDirection(cell.X, cell.Y, direction);

            if (neighbour != null && neighbour is Plant && !(neighbour is EdgePlant))
            {
                return true;
            }
        }
        return false;
    }
     

    public override bool Grow()
    {
        bool hasGrown = false;

        foreach (GridDirection direction in GridDirectionHelpers.AllDirections)
        {
            CellController neighbour = this.Grid.GetCellInDirection(this.X, this.Y, direction);


            if (CanClaim(neighbour) && HasNonEdgePlantNeighbour(neighbour))
            {
                neighbour.Claim(this);
                hasGrown = true;
            }

        }

        return hasGrown;

    }



}
