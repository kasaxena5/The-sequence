using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    [SerializeField] private Vector2Int _startPosition;
    [SerializeField] private Vector2Int _endPosition;
    private Object _targetObject;

    public enum Mode
    {
        Build,
        Sequence
    }

    public Mode gameMode;

    private Tile[,] gameGrid;

    public void InitializeGameGrid()
    {

    }

}
