using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineObject : GridObject
{
    protected int _sequenceNumber;
    protected bool _polarity;
    protected Vector2Int _direction = Vector2Int.up;

    public virtual void PerformAction()
    {
    }

    public virtual void RevertAction()
    {
    }
}
