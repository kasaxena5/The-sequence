using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    private Tile _tile;
    private bool _isMovable;
    private GameGrid gameGrid;

    public void SetTile(Tile tile)
    {
        _tile = tile;
        transform.position = new Vector3(tile.transform.position.x, 0.5f, tile.transform.position.z);
    }
}
