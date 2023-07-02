using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action
{
    private MachineObject _machineObject;

    public Action(MachineObject machineObject)
    {
        _machineObject = machineObject;
    }

    public void Execute()
    {
        _machineObject.PerformAction();
    }

    public void Undo()
    {
        _machineObject.RevertAction();
    }
}
