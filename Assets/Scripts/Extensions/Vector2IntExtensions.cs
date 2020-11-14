using UnityEngine;
using System.Collections;


public static class Vector2IntExtensions
{
    public static Vector2Int MoveInGridDirection(this Vector2Int vec, GridDirection direction)
    {
        switch (direction)
        {
            case GridDirection.N:
                vec.y++;
                break;
            case GridDirection.NE:
                vec.y++;
                vec.x++;
                break;
            case GridDirection.E:
                vec.x++;
                break;
            case GridDirection.SE:
                vec.y--;
                vec.x++;
                break;
            case GridDirection.S:
                vec.y--;
                break;
            case GridDirection.SW:
                vec.y--;
                vec.x--;
                break;
            case GridDirection.W:
                vec.x--;
                break;
            case GridDirection.NW:
                vec.y++;
                vec.x--;
                break;
        }


        return vec;

    }
}