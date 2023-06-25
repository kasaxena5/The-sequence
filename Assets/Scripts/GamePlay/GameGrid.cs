using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private GridObject _obstaclePrefab;
    [SerializeField] private GridObject _targetObjectPrefab;

    // TODO: Remove this after testing
    [SerializeField] private PushPullMachineObject _pushPullMachineObjectPrefab;
    [SerializeField] private RevolveMachineObject _revolveMachineObjectPrefab;

    [Header("Configs")]
    [SerializeField] private Vector2Int _startPosition;
    [SerializeField] private Vector2Int _endPosition;
    [SerializeField] private int _width;
    [SerializeField] private int _depth;
    [SerializeField] private Vector2Int[] _obstaclePositions;
    [SerializeField] private bool _drawGizmos;

    // TODO: Remove this after testing
    [SerializeField] private Vector2Int _machinePosition;

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

        //TODO Remove After testing
        int x = _machinePosition.x;
        int z = _machinePosition.y;

        GridObject machineObject = Instantiate(_revolveMachineObjectPrefab);
        machineObject.Initialize(this);
        _gameGrid[x, z].DropObject(machineObject);
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
            _gameGrid[x, z].DropObject(obstacle);
        }
    }

    private void GenerateTargetObject()
    {
        int x = _startPosition.x;
        int z = _startPosition.y;

        GridObject targetObject = Instantiate(_targetObjectPrefab);
        _gameGrid[x, z].DropObject(targetObject);
    }

    public Tile GetNeighbourTile(Tile tile, Vector2Int direction)
    {
        if (!tile)
            return null;
        int dx = direction.x;
        int dz = direction.y;

        int x = tile.GetPosition().x;
        int z = tile.GetPosition().y;

        if(x + dx >= 0 && z + dz >= 0 && x + dx < _width && z + dz < _depth)
            return _gameGrid[x + dx, z + dz];
        return null;
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
