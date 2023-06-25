using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPullMachineObject : MachineObject
{
    public override void PerformAction()
    {
        Tile pickupTile = _gameGrid.GetNeighbourTile(_tile, _direction);
        Tile dropTile = _gameGrid.GetNeighbourTile(pickupTile, _direction);


        GridObject gridObject = pickupTile?.GetObject();

        if(gridObject && pickupTile && dropTile)
        {
            gridObject.MoveToTile(dropTile);
        }

    }

    public override void RevertAction()
    {

        Tile dropTile = _gameGrid.GetNeighbourTile(_tile, _direction);
        Tile pickupTile = _gameGrid.GetNeighbourTile(dropTile, _direction);
        
        GridObject gridObject = pickupTile?.GetObject();
        if (gridObject && pickupTile && dropTile)
        {
            gridObject.MoveToTile(dropTile);
        }
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
