using UnityEngine;
using System;
// using GridDirectionExtensions;

public class ExpandPlant : Plant
{
    [Tooltip("Directions to expand.")]
    public GridDirection[] directionsToExpand;


    public override void Grow()
    {

        foreach (GridDirection direction in directionsToExpand)
        {
            CellController neighbour = this.Grid.GetCellInDirection(this.X, this.Y, direction);


            if (CanClaim(neighbour))
            {
                neighbour.Claim(this);
            }

        }
    }


}
