using UnityEngine;
using System;
// using GridDirectionExtensions;

public class ConwayPlant : Plant
{

    public override bool Grow()
    {
        bool hasGrown = false;
        if (numNeighbours <= 1 || numNeighbours >3)
        {
            this.Kill();
        } else 
        {
            
            foreach (GridDirection direction in GridDirectionHelpers.AllDirections)
            {
                CellController neighbour = this.Grid.GetCellInDirection(this.X, this.Y, direction);


                if (neighbour.numNeighbours == 3 && CanClaim(neighbour))
                {
                    neighbour.Claim(this);
                    hasGrown = true;
                }

            }
        }
        return hasGrown;

    }


}
