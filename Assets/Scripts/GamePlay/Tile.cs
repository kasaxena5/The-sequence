using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private Vector2Int _position;
    private GridObject _object;
    public static int Size;

    public void InitializePosition(int x, int y, int z)
    {
        transform.position = new Vector3(x, y, z);
        _position = new Vector2Int(x, z);
    }

    public Vector2Int GetPosition()
    {
        return _position;
    }

    public bool HasObject()
    {
        return _object != null;
    }

    public GridObject GetObject()
    {
        return _object;
    }

    public void DropObject(GridObject newObject)
    {
        _object = newObject;
        _object.SetTile(this);
    }

    public void PickUpObject()
    {
        _object = null;
    }
}
