using UnityEngine;
using System;
// using GridDirectionExtensions;

public class ScaredPlant : Plant
{
    public GridDirection[] DirectionsToRunFrom = GridDirectionHelpers.StraightDirections;
    public bool KeepRunning = false;

    [SerializeField]
    private bool isRunning = false;
    [SerializeField]

    private GridDirection runningDirection;


    private bool NeighbourIsPlant(CellController neighbour)
    {

        return neighbour is Plant && !(neighbour is ScaredPlant);
    }

    // Why does Instantiate(prefab) clear values from the class I'm giving it? Is there a interface I can override or w.e to do this automatically??
    public override void Initialize(CellController parent)
    {

        base.Initialize(parent);
        ScaredPlant scaredParent = parent as ScaredPlant;

        if (scaredParent)
        {
            runningDirection = scaredParent.runningDirection;
        }


    }

    private ScaredPlant DeepCopy()
    {
        ScaredPlant other = (ScaredPlant)this.MemberwiseClone();
        other.runningDirection = this.runningDirection;
        other.KeepRunning = this.KeepRunning;
        other.isRunning = this.isRunning;
        return other;

    }

    public override bool Grow()
    {
        bool hasGrown = false;

        if (isRunning && KeepRunning)
        {
            CellController neighbour = this.Grid.GetCellInDirection(this.X, this.Y, runningDirection);

            if (CanClaim(neighbour))
            {
                neighbour.Claim(this);
                hasGrown = true;
            }
        }


        foreach (GridDirection direction in DirectionsToRunFrom)
        {

            CellController neighbour = this.Grid.GetCellInDirection(this.X, this.Y, direction);

            if (NeighbourIsPlant(neighbour))
            {

                CellController oppositeNeighbour = this.Grid.GetCellInDirection(this.X, this.Y, direction.Opposite());

                if (CanClaim(oppositeNeighbour))
                {
                    if (!isRunning)
                    {
                        isRunning = true;
                        runningDirection = direction.Opposite();
                    }

                    ScaredPlant copy = DeepCopy();
                    copy.runningDirection = direction.Opposite();
                    oppositeNeighbour.Claim(copy);
                    hasGrown = true;
                }


            }


        }
        return hasGrown;

    }


}

