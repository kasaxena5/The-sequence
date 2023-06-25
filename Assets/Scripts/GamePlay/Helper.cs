using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
    public static Vector2Int GetNextRevolveDirection(Vector2Int direction, bool clockwise)
    {
        if(direction == Vector2Int.up)
            return clockwise ? Vector2Int.right: Vector2Int.left;
        if (direction == Vector2Int.right)
            return clockwise ? Vector2Int.down: Vector2Int.up;
        if (direction == Vector2Int.down)
            return clockwise ? Vector2Int.left: Vector2Int.right;
        if (direction == Vector2Int.left)
            return clockwise ? Vector2Int.up: Vector2Int.down;

        return Vector2Int.up;
    }
}
