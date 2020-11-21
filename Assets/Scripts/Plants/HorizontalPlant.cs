using UnityEngine;
using System;
// using GridDirectionExtensions;

public class HorizontalPlant : Plant
{
    public override bool Grow()
    {
        bool hasGrown = false;
        foreach (GridDirection direction in GridDirectionHelpers.Horizontal)
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
