using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActionRecorder : MonoBehaviour
{
    private Stack<Action> _actions = new();

    public void Record(Action action)
    {
        action.Execute();
        _actions.Push(action);
    }

    public void Rewind()
    {
        Action action = _actions.Pop();
        action.Undo();
    }
}
