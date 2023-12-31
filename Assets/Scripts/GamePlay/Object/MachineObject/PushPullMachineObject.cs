using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPullMachineObject : MachineObject
{
    private void MoveObjectFromPickupToDrop(Tile pickupTile, Tile dropTile)
    {
        if (pickupTile && pickupTile.HasObject() && dropTile && !dropTile.HasObject())
        {
            GridObject gridObject = pickupTile.GetObject();
            gridObject.MoveToTile(dropTile);
        }
    }

    private void PushOrPull(bool push)
    {
        Tile pickupTile;
        Tile dropTile;
        if (push)
        {
            pickupTile = _gameGrid.GetNeighbourTile(_tile, _direction);
            dropTile = _gameGrid.GetNeighbourTile(pickupTile, _direction);
        }
        else
        {

            dropTile = _gameGrid.GetNeighbourTile(_tile, _direction);
            pickupTile = _gameGrid.GetNeighbourTile(dropTile, _direction);
        }

        MoveObjectFromPickupToDrop(pickupTile, dropTile);
    }
    public override void PerformAction()
    {
        PushOrPull(_polarity);
    }

    public override void RevertAction()
    {
        PushOrPull(!_polarity);
    }
}
