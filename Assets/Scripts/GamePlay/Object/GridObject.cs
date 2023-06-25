using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    protected Tile _tile;
    private bool _isMovable;
    protected GameGrid _gameGrid;

    public void Initialize(GameGrid gameGrid, bool isMovable=true)
    {
        _gameGrid = gameGrid;
        _isMovable = isMovable;
    }

    public void SetTile(Tile tile)
    {
        _tile = tile;
        transform.position = new Vector3(tile.transform.position.x, 0.5f, tile.transform.position.z);
    }

    public void MoveToTile(Tile tile)
    {
        tile.DropObject(this);
    }
}
