using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineObjectItem : MonoBehaviour
{
    private int _count;
    [SerializeField] private MachineObject _machineObjectPrefab;
    [SerializeField] private MachineObjectEvent _machineObjectCreatedEvent;
    [SerializeField] private MachineObjectEvent _machineObjectPickedEvent;
    [SerializeField] private MachineObjectEvent _machineObjectDeletedEvent;

    private void OnEnable()
    {
        _machineObjectDeletedEvent.Subscribe(DeleteMachineObject);
    }

    private void OnDisable()
    {
        _machineObjectDeletedEvent.UnSubscribe(DeleteMachineObject);
    }

    private void DecrementCount()
    {
        _count--;
    }

    private void IncrementCount()
    {
        _count++;
    }

    public void SetCount(int count)
    {
        _count = count;
    }

    public void DeleteMachineObject(MachineObject machineObject)
    {
        if (machineObject.GetType() != _machineObjectPrefab.GetType())
            return;

        IncrementCount();
        Destroy(machineObject.gameObject);
    }

    public void CreateMachineObject()
    {
        MachineObject machineObject = Instantiate(_machineObjectPrefab);
        DecrementCount();
        _machineObjectCreatedEvent.RaiseEvent(machineObject);
        _machineObjectPickedEvent.RaiseEvent(machineObject);
    }

}
