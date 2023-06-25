using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolveMachineObject : MachineObject
{
    public override void PerformAction()
    {
        Vector2Int nextDirection = Helper.GetNextRevolveDirection(_direction, true);
        Tile pickupTile = _gameGrid.GetNeighbourTile(_tile, _direction);
        Tile dropTile = _gameGrid.GetNeighbourTile(_tile, nextDirection);


        GridObject gridObject = pickupTile?.GetObject();

        if (gridObject && pickupTile && dropTile)
        {
            gridObject.MoveToTile(dropTile);
        }
        SetDirection(nextDirection);
    }

    public override void RevertAction()
    {
        Vector2Int nextDirection = Helper.GetNextRevolveDirection(_direction, false);
        Tile pickupTile = _gameGrid.GetNeighbourTile(_tile, _direction);
        Tile dropTile = _gameGrid.GetNeighbourTile(_tile, nextDirection);


        GridObject gridObject = pickupTile?.GetObject();

        if (gridObject && pickupTile && dropTile)
        {
            gridObject.MoveToTile(dropTile);
        }
        SetDirection(nextDirection);
    }

    // TODO: Remove after testing
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            PerformAction();
        if (Input.GetKeyDown(KeyCode.R))
            RevertAction();
    }
}
