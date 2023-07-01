using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineObjectHUD : MonoBehaviour
{
    private Canvas _canvas;
    private RectTransform _rectTransform;
    private MachineObject _machineObject;
    
    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.worldCamera = Camera.main;
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if(_machineObject)
            _rectTransform.position = _machineObject.transform.position + new Vector3(0, 1.5f, 0);
    }

    public void Initialize(MachineObject machineObject)
    {
        _machineObject = machineObject;
    }
    
    public void ReversePolarity()
    {
        _machineObject.ReversePolarity();
    }

    public void Rotate(bool clockwise)
    {
        _machineObject.Rotate(clockwise);
    }
}
