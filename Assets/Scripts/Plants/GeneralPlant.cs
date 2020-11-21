using UnityEngine;
using System.Collections.Generic;

public class GeneralPlant : Plant
{
      
    [ Tooltip("Expand in direction when a pattern is matched.")]
    public ActionPattern[] actionPatterns;



    private bool MatchesPattern(List<GridDirection> pattern)
    {
        foreach (GridDirection direction in pattern)
        {
            CellController cell = this.Grid.GetCellInDirection(this.X, this.Y, direction);

            if (!(cell is Plant))
            {
                return false;
            }
        }

        return true;
    }


    public override bool Grow()
    {
        bool hasGrown = false;
        foreach (ActionPattern actionPattern in actionPatterns)
        {
            if (MatchesPattern(actionPattern.GetPattern())) {
                if (actionPattern.action == Action.Die)
                {
                    this.Kill();
                    return hasGrown;
                }
            }
        }
        
        foreach (ActionPattern actionPattern in actionPatterns)
        {
            if (MatchesPattern(actionPattern.GetPattern())) {
                if (actionPattern.action == Action.Expand)
                {
                    CellController cell = this.Grid.GetCellInDirection(this.X, this.Y, actionPattern.expandDirection);

                    if (CanClaim(cell))
                    {
                        cell.Claim(this);
                        hasGrown = true;
                    }
                }
            }
        }
        return hasGrown;

    }


}
