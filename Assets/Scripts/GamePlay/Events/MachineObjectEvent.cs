using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MachineObjectEvent : ScriptableObject
{
    public delegate void MachineObjectFunction(MachineObject machineObject);
	private MachineObjectFunction OnEventRaised;

	public void Subscribe(MachineObjectFunction f)
    {
		OnEventRaised += f;
    }

    public void UnSubscribe(MachineObjectFunction f)
    {
        OnEventRaised -= f;
    }

	public void RaiseEvent(MachineObject machineObject)
	{
        OnEventRaised?.Invoke(machineObject);
    }

}
