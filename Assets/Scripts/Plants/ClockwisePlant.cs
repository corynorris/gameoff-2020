using UnityEngine;
using System;
// using GridDirectionExtensions;

public class ClockwisePlant : Plant
{
    GridDirection lastDirection = GridDirection.N;


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
    public override void MakeClaims()
    {
        CellController cellToClaim = GetNextEmptyNeighbour();


        if (numNeighbours > 4) this.Kill();
        if (cellToClaim != null)
        {
            cellToClaim.Claim(this);
        }

    }


}
