using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private Vector2Int _position;
    private Object _object;
    public static int Size;

    public bool HasObject()
    {
        return _object != null;
    }

    public Object GetObject()
    {
        return _object;
    }

    public void DropObject(Object newObject)
    {
        _object = newObject;
    }

    public void PickUpObject()
    {
        _object = null;
    }
}
