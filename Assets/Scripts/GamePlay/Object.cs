using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    private Tile _tile;
    private bool _isStatic;
    private GameGrid gameGrid;

    void SetTile(Tile tile)
    {
        _tile = tile;
    }
}
