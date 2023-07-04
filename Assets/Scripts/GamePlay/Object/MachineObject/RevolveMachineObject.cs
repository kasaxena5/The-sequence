using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolveMachineObject : MachineObject
{
    private void MoveObjectFromPickupToDrop(Tile pickupTile, Tile dropTile)
    {
        if (pickupTile && pickupTile.HasObject() && dropTile && !dropTile.HasObject())
        {
            GridObject gridObject = pickupTile.GetObject();
            gridObject.MoveToTile(dropTile);
        }
    }

    private void Revolve(bool clockwise)
    {
        Vector2Int nextDirection = Helper.GetNextRevolveDirection(_direction, clockwise);
        Tile pickupTile = _gameGrid.GetNeighbourTile(_tile, _direction);
        Tile dropTile = _gameGrid.GetNeighbourTile(_tile, nextDirection);


        MoveObjectFromPickupToDrop(pickupTile, dropTile);
        Rotate(clockwise);
    }

    public override void PerformAction()
    {
        Revolve(_polarity);
    }

    public override void RevertAction()
    {
        Revolve(!_polarity);
    }
}
