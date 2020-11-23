using UnityEngine;
using System.Collections;


public static class Vector2IntExtensions
{

    public static GridDirection DirectionTo(this Vector2Int vec, Vector2Int target)
    {
        // N, NE, E, SE, S, SW, W, NW
        GridDirection direction = GridDirection.N;

        //if (vec.x == target.x && vec.y == target.y) throw new System.Exception("Cannot calculate the direction between two identical points");

        if (target.x == vec.x) { 
            if (target.y > vec.y)
            {
                direction = GridDirection.N;
            }
            else if (target.y < vec.y)
            {
                direction = GridDirection.S;
            }
        }
        if (target.y == vec.y) {            
            if (target.x > vec.x)
            {
                direction = GridDirection.E;
            }
            else if (target.x < vec.x)
            {
                direction = GridDirection.W;
            }
        }

        if (target.x > vec.x)
        {
            if (target.y > vec.y)
            {
                direction = GridDirection.NE;
            }
            else if (target.y < vec.y)
            {
                direction = GridDirection.SE;
            }
        }
        if (target.x < vec.x)
        {
            if (target.y > vec.y)
            {
                direction = GridDirection.NW;
            }
            else if (target.y < vec.y)
            {
                direction = GridDirection.SE;
            }
        }

        return direction;

    }


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