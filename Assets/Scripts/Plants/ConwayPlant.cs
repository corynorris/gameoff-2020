using UnityEngine;
using System;
// using GridDirectionExtensions;

public class ConwayPlant : Plant
{

    int turnsPassed = 0;

    public override void MakeClaims()
    {

        //if (turnsPassed++ <= 1) return;

        
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
                }

            }
        }


    }


}
