using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    protected Tile _tile;
    protected bool _isMovable;
    protected GameGrid _gameGrid;

    public bool IsMovable { get { return _isMovable; } set { _isMovable = value; } }

    public virtual void Initialize(GameGrid gameGrid, bool isMovable=true)
    {
        _gameGrid = gameGrid;
        _isMovable = isMovable;
    }

    public void SetTile(Tile tile)
    {
        _tile = tile;
        transform.position = new Vector3(tile.transform.position.x, 0.5f, tile.transform.position.z);
    }

    public Tile GetTile()
    {
        return _tile;
    }

    public void MoveToTile(Tile tile)
    {
        _tile.PickUpObject();
        tile.DropObject(this);
    }
}
