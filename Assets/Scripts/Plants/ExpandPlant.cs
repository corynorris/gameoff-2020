using UnityEngine;
using System;
// using GridDirectionExtensions;

public class ExpandPlant : Plant
{
    GridDirection lastDirection = GridDirection.N;

    private int lastSpawn = 0;

    int turnsSinceLastGrowth = 0;

    public override void ClaimGrowth()
    {

        turnsSinceLastGrowth++;

        int numPlantsTouching = NumPlantsTouching();

        if (turnsSinceLastGrowth < numPlantsTouching) return;

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
