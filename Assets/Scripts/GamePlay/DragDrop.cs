using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    [SerializeField] private float _distance = 10f;
    [SerializeField] private GameGrid _gameGrid;
    [SerializeField] private MachineObject _machineObject;

    private Tile _tile;


    private void MoveMachineToTile(Tile tile)
    {
        _machineObject.transform.position = tile.transform.position + new Vector3(0, 0.5f, 0);
        _tile = tile;
    }

    private void MoveMachineToDefaultDistance(Ray ray)
    {
        Vector3 rayPoint = ray.GetPoint(_distance);
        _machineObject.transform.position = rayPoint;
        _tile = null;
    }

    private void MoveMachine()
    {
        _machineObject.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject.TryGetComponent(out Tile tile))
            {
                if (!tile.HasObject())
                {
                    MoveMachineToTile(tile);
                }
                else
                {
                    MoveMachineToDefaultDistance(ray);
                }
            }
            else
            {
                MoveMachineToDefaultDistance(ray);
            }
        }
        else
        {
            MoveMachineToDefaultDistance(ray);
        }
    }

    private void DropMachine()
    {
        if(_tile)
        {
            _machineObject.Initialize(_gameGrid);
            _tile.DropObject(_machineObject);
            _machineObject = null;
        }
    }

    private void Update()
    {
        if (_machineObject)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
                DropMachine();
            MoveMachine();
        }
    }
}
