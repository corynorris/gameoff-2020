using UnityEngine;
using System;
// using GridDirectionExtensions;

public class HorizontalPlant : Plant
{
    GridDirection lastDirection = GridDirection.N;

    private int lastSpawn = 0;

    int turnsOld = 0;

    public override void ClaimGrowth()
    {

        turnsOld++;
        if (turnsOld > 1) return;

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
