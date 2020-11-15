using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ActionPattern
{
    public Action action;
    public bool[] pattern = new bool[8];
    public GridDirection expandDirection;

    public List<GridDirection> GetPattern()
    {
        List <GridDirection> gridPattern = new List<GridDirection>();
        for (int i = 0; i < 8; i++)
        {
            if (pattern[i])
            {
                gridPattern.Add((GridDirection)i);
            }
        }
        return gridPattern;
    }
}