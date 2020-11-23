using UnityEngine;
using System;
// using GridDirectionExtensions;

public class VerticalPlant : Plant
{

    public override bool Grow()
    {
        bool hasGrown = false;

        foreach (GridDirection direction in GridDirectionHelpers.AllDirections)
        {
            CellController neighbour = this.Grid.GetCellInDirection(this.X, this.Y, direction);

            if (CanClaim(neighbour))
            {
                neighbour.Claim(this);
                hasGrown = true;
            }

        }
        
        return hasGrown;

    }


}
