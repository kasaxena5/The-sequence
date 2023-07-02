using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MachineObjectEvent : ScriptableObject
{
    public delegate MachineObject MachineObjectFunction();
	private MachineObjectFunction OnEventRaised;

	public void Subscribe(MachineObjectFunction f)
    {
		OnEventRaised += f;
    }

	public MachineObject RaiseEvent()
	{
		if(OnEventRaised != null)
			return OnEventRaised();
		return null;
	}

}
