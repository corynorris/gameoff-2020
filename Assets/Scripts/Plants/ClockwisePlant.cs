using UnityEngine;
using System;
// using GridDirectionExtensions;

public class ClockwisePlant : Plant
{
    GridDirection lastDirection = GridDirection.N;

    private int lastSpawn = 0;

    private CellController GetNextEmptyNeighbour()
    {


        for (int i = 0; i < 8; i++)
        {
            CellController neighbour = this.Grid.GetCellInDirection(this.X, this.Y, lastDirection);
            lastDirection = lastDirection.Next();

            if (CanClaim(neighbour))
            {
                return neighbour;
            }


        }
        return null;
    }
    public override void ClaimGrowth()
    {
        CellController cellToClaim = GetNextEmptyNeighbour();

        if (cellToClaim != null)
        {
            cellToClaim.Claim(this);
        }

    }


}
