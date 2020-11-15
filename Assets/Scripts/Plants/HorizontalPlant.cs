using UnityEngine;
using System;
// using GridDirectionExtensions;

public class HorizontalPlant : Plant
{
    public override void Grow()
    {
        foreach (GridDirection direction in GridDirectionHelpers.Horizontal)
        {
            CellController neighbour = this.Grid.GetCellInDirection(this.X, this.Y, direction);

            if (CanClaim(neighbour))
            {
                neighbour.Claim(this);
            }

        }
    }

}
