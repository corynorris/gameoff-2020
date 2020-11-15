using UnityEngine;
using System;
// using GridDirectionExtensions;

public class VerticalPlant : Plant
{

    public override void Grow()
    {
        foreach (GridDirection direction in GridDirectionHelpers.Vertical)
        {

            CellController neighbour = this.Grid.GetCellInDirection(this.X, this.Y, direction);

            if (CanClaim(neighbour))
            {
                neighbour.Claim(this);
            }

        }

    }


}
