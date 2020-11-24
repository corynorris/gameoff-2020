using UnityEngine;
using System;
// using GridDirectionExtensions;

public class ScaredPlant : Plant
{
    public GridDirection[] DirectionsToRunFrom = GridDirectionHelpers.StraightDirections;
    public bool KeepRunning = false;

    public GridDirection runningDirection;


    private bool NeighbourIsPlant(CellController neighbour)
    {

        return neighbour is Plant && !(neighbour is ScaredPlant);
    }

    private bool AlreadyRunning(GridDirection direction)
    {
        //return neighbour is ScaredPlant && bornFromParent;
        return (bornFromParent && KeepRunning && runningDirection == direction);
    }

    public override void Initialize(CellController parent)
    {

        base.Initialize(parent);
        ScaredPlant scaredParent = parent as ScaredPlant;

        if (scaredParent)
        {
            runningDirection = scaredParent.runningDirection;
        }

 
    }

    public override bool Grow()
    {
        bool hasGrown = false;
        foreach (GridDirection direction in DirectionsToRunFrom)       {

            CellController neighbour = this.Grid.GetCellInDirection(this.X, this.Y, direction);
            
            //if (NeighbourIsPlant(neighbour))
            //{

            //    CellController oppositeNeighbour = this.Grid.GetCellInDirection(this.X, this.Y, runningDirection);

            //    if (CanClaim(oppositeNeighbour))
            //    {
            //        oppositeNeighbour.Claim(this);
            //        hasGrown = true;
            //    }
            //}

            if (NeighbourIsPlant(neighbour) || AlreadyRunning(direction))
            {
                // Claim opposite direction 
                if (NeighbourIsPlant(neighbour)) { 
                    runningDirection = direction.Opposite();
                }

                CellController oppositeNeighbour = this.Grid.GetCellInDirection(this.X, this.Y, runningDirection);  

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
