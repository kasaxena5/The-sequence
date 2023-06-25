using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineObject : GridObject
{
    protected int _sequenceNumber;
    protected bool _polarity;
    protected Vector2Int _direction = Vector2Int.up;

    public void SetDirection(Vector2Int direction)
    {
        _direction = direction;
    }

    protected void ReversePolarity()
    {
        _polarity = !_polarity;
    }

    public virtual void PerformAction()
    {
    }

    public virtual void RevertAction()
    {
    }
}
