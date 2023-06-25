using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private GridObject _obstaclePrefab;
    [SerializeField] private GridObject _targetObjectPrefab;

    [Header("Configs")]
    [SerializeField] private Vector2Int _startPosition;
    [SerializeField] private Vector2Int _endPosition;
    [SerializeField] private int _width;
    [SerializeField] private int _depth;
    [SerializeField] private Vector2Int[] _obstaclePositions;
    [SerializeField] private bool _drawGizmos;

    public enum Mode
    {
        Build,
        Sequence
    }

    public Mode gameMode;

    private Tile[,] _gameGrid;

    private void Awake()
    {
        InitializeGameGrid();    
    }

    public void InitializeGameGrid()
    {
        GenerateTiles();
        GenerateObstacles();
        GenerateTargetObject();

    }

    private void GenerateTiles()
    {
        _gameGrid = new Tile[_width, _depth];
        for(int x = 0; x < _width; x++)
        {
            for(int z = 0; z < _depth; z++)
            {
                _gameGrid[x, z] = Instantiate(_tilePrefab);
                _gameGrid[x, z].InitializePosition(x, 0, z);
            }
        }
    }

    private void GenerateObstacles()
    {
        foreach(Vector2Int obstaclePosition in _obstaclePositions)
        {
            int x = obstaclePosition.x;
            int z = obstaclePosition.y;

            GridObject obstacle = Instantiate(_obstaclePrefab);
            if(!TryDropObject(x, z, obstacle))
                Debug.LogError($"Obstacle Position [{x},{z}] already has an object or is out of bounds!");
        }
    }

    private void GenerateTargetObject()
    {
        int x = _startPosition.x;
        int z = _startPosition.y;

        GridObject targetObject = Instantiate(_targetObjectPrefab);
        if (!TryDropObject(x, z, targetObject))
            Debug.LogError($"Start Position [{x},{z}] already has an object or is out of bounds!");
    }

    private bool AreInBounds(int x, int z)
    {
        return x >= 0 && z >= 0 && x < _width && z < _depth;
    }

    public Tile GetNeighbourTile(Tile tile, Vector2Int direction)
    {
        if (!tile)
            return null;
        
        int dx = direction.x;
        int dz = direction.y;

        int x = tile.GetPosition().x;
        int z = tile.GetPosition().y;

        if(AreInBounds(x + dx, z + dz))
            return _gameGrid[x + dx, z + dz];
        return null;
    }

    public bool TryDropObject(int x, int z, GridObject gridObject)
    {
        if (!AreInBounds(x, z))
            return false;
        if (_gameGrid[x, z].HasObject())
            return false;
        _gameGrid[x, z].DropObject(gridObject);
        return true;
    }

    #region Gizmos
    private void DrawTileGizmos(int x, int z)
    {
        Gizmos.color = new Color(0, 1f, 0, 0.5f);
        Gizmos.DrawCube(new Vector3(x, 0, z), new Vector3(0.9f, 0.1f, 0.9f));

        Gizmos.color = new Color(0, 1f, 0, 1f);
        Gizmos.DrawWireCube(new Vector3(x, 0, z), new Vector3(0.9f, 0.1f, 0.9f));
    }

    private void DrawStartTileGizmos(int x, int z)
    {
        Gizmos.color = new Color(1f, 0, 0, 0.5f);
        Gizmos.DrawCube(new Vector3(x, 0, z), new Vector3(0.9f, 0.1f, 0.9f));

        Gizmos.color = new Color(1f, 0, 0, 1f);
        Gizmos.DrawWireCube(new Vector3(x, 0, z), new Vector3(0.9f, 0.1f, 0.9f));
    }

    private void DrawEndTileGizmos(int x, int z)
    {
        Gizmos.color = new Color(1f, 1f, 1f, 0.5f);
        Gizmos.DrawCube(new Vector3(x, 0, z), new Vector3(0.9f, 0.1f, 0.9f));

        Gizmos.color = new Color(1f, 1f, 1f, 1f);
        Gizmos.DrawWireCube(new Vector3(x, 0, z), new Vector3(0.9f, 0.1f, 0.9f));
    }

    private void DrawObstacleGizmos(int x, int z)
    {
        Gizmos.color = new Color(1f, 1f, 0, 0.5f);
        Gizmos.DrawCube(new Vector3(x, 0.5f, z), new Vector3(0.9f, 0.9f, 0.9f));

        Gizmos.color = new Color(1f, 1f, 0, 1f);
        Gizmos.DrawWireCube(new Vector3(x, 0.5f, z), new Vector3(0.9f, 0.9f, 0.9f));
    }

    private void DrawTargetObjectGizmos(int x, int z)
    {
        Gizmos.color = new Color(0f, 0, 1f, 0.5f);
        Gizmos.DrawSphere(new Vector3(x, 0.5f, z), 0.25f);

        Gizmos.color = new Color(0f, 0, 1f, 1f);
        Gizmos.DrawWireSphere(new Vector3(x, 0.5f, z), 0.25f);
    }

    private void OnDrawGizmos()
    {
        if (!_drawGizmos || Application.isPlaying)
            return;
        for(int x = 0; x < _width; x++)
        {
            for(int z = 0; z < _depth; z++)
            {
                DrawTileGizmos(x, z);
            }
        }

        foreach (Vector2Int obstaclePosition in _obstaclePositions)
        {
            int x = obstaclePosition.x;
            int z = obstaclePosition.y;
            DrawObstacleGizmos(x, z);
        }

        DrawStartTileGizmos(_startPosition.x, _startPosition.y);
        DrawTargetObjectGizmos(_startPosition.x, _startPosition.y);

        DrawEndTileGizmos(_endPosition.x, _endPosition.y);
    }
    
    #endregion
}
