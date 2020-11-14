using UnityEngine;
using System;
// using GridDirectionExtensions;

public class ExpandPlant : Plant
{

    int turnsSinceLastGrowth = 0;

    public override void MakeClaims()
    {

        turnsSinceLastGrowth++;


        //if (turnsSinceLastGrowth < numNeighbours) return;

        foreach (GridDirection direction in GridDirectionHelpers.StraightDirections)
        {
            CellController neighbour = this.Grid.GetCellInDirection(this.X, this.Y, direction);


            if (CanClaim(neighbour))
            {
                neighbour.Claim(this);
            }

        }

        turnsSinceLastGrowth = 0;
    }


}
