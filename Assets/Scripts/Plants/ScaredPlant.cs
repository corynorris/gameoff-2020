using UnityEngine;
using System;
// using GridDirectionExtensions;

public class ScaredPlant : Plant
{
    public GridDirection[] DirectionsToRunFrom = GridDirectionHelpers.StraightDirections;
    public bool KeepRunning = false;
    private GridDirection runningDirection;

    private bool ShouldRunFromPlant(CellController neighbour)
    {
        return neighbour is Plant && !(neighbour is ScaredPlant);
    }

    private bool ShouldKeepRunning(GridDirection direction)
    {
        //return neighbour is ScaredPlant && bornFromParent;
        return (bornFromParent && KeepRunning && runningDirection == direction);
    }

    public override void Initialize(CellController cellParent)
    {
        base.Initialize(cellParent);
        runningDirection =  this.GetPosVector2Int().DirectionTo(GetClaimant().GetPosVector2Int()).Opposite();

    }

    public override bool Grow()
    {
        bool hasGrown = false;
        foreach (GridDirection direction in DirectionsToRunFrom)       {

            CellController neighbour = this.Grid.GetCellInDirection(this.X, this.Y, direction);

     
            if (ShouldRunFromPlant(neighbour) || ShouldKeepRunning(direction))
            {
                // Claim opposite direction

                CellController oppositeNeighbour = this.Grid.GetCellInDirection(this.X, this.Y, direction.Opposite());  

                if (CanClaim(oppositeNeighbour))
                {
                    oppositeNeighbour.Claim(this);
                    hasGrown = true;
                }

                
            }

          
        }
        return hasGrown;

    }


}
