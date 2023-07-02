using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MachineObjectItem
{
    public enum MachineType { PushPull, Revolve }
    
    public MachineType MachineObjectType;
    public int Count;
    public MachineObject MachineObjectPrefab;
    public GameObject MachineObjectIconPrefab;

}
