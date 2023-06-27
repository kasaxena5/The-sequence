using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineObject : GridObject
{
    [SerializeField] private MachineObjectHUD _HUD;
    [SerializeField] private GameObject _positivePolarity;
    [SerializeField] private GameObject _negativePolarity;

    protected int _sequenceNumber;
    protected bool _polarity = true;
    protected Vector2Int _direction = Vector2Int.up;

    private void OnMouseDown()
    {
        _HUD.gameObject.SetActive(true);
    }

    public void SetDirection(Vector2Int direction)
    {
        _direction = direction;
    }

    public void Rotate(bool clockwise)
    {
        Vector2Int nextDirection;
        if (clockwise)
        {
            transform.Rotate(new Vector3(0, 90, 0));
            nextDirection = Helper.GetNextRevolveDirection(_direction, clockwise);
        }
        else
        {
            transform.Rotate(new Vector3(0, -90, 0));
            nextDirection = Helper.GetNextRevolveDirection(_direction, !clockwise);
        }
        SetDirection(nextDirection);
    }

    protected void ReversePolarity()
    {
        _polarity = !_polarity;

      
        _positivePolarity.SetActive(_polarity);
        _negativePolarity.SetActive(_polarity);
        
    }

    public virtual void PerformAction()
    {
    }

    public virtual void RevertAction()
    {
    }
}
