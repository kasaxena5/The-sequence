using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineObjectDropper : MonoBehaviour
{
    [SerializeField] private float _distance = 10f;
    [SerializeField] private GameGrid _gameGrid;
    [SerializeField] private MachineObject _machineObject;

    [SerializeField] MachineObjectEvent _machineObjectPickedEvent;
    [SerializeField] MachineObjectEvent _machineObjectDroppedEvent;
    [SerializeField] MachineObjectEvent _machineObjectDeletedEvent;

    private Tile _tile;

    private void OnEnable()
    {
        _machineObjectPickedEvent.Subscribe(PickMachine);
    }

    private void OnDisable()
    {
        _machineObjectPickedEvent.UnSubscribe(PickMachine);
    }

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
            _machineObject.gameObject.layer = LayerMask.NameToLayer("Default");
            _machineObject.Initialize(_gameGrid);
            _tile.DropObject(_machineObject);

            _machineObjectDroppedEvent.RaiseEvent(_machineObject);
            _machineObject = null;

        }
        else
        {
            _machineObjectDroppedEvent.RaiseEvent(_machineObject);
            _machineObjectDeletedEvent.RaiseEvent(_machineObject);
            _machineObject = null;

        }
    }

    private void PickMachine(MachineObject machineObject)
    {
        _machineObject = machineObject;
        _machineObject.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }

    private void Update()
    {
        if (_machineObject)
        {
            MoveMachine();
            if (Input.GetKeyDown(KeyCode.Mouse0))
                DropMachine();
        }
    }
}
