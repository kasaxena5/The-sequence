using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineObject : GridObject
{
    [SerializeField] private MachineObjectHUD _HUDPrefab;
    [SerializeField] private GameObject _positivePolarity;
    [SerializeField] private GameObject _negativePolarity;

    private MachineObjectHUD _HUD;
    private bool _isHUDActive;

    protected int _sequenceNumber;
    protected bool _polarity = true;
    protected Vector2Int _direction = Vector2Int.up;

    /*
    private void OnMouseDown()
    {
        _isHUDActive = !_isHUDActive;
        _HUD.gameObject.SetActive(_isHUDActive);
    }
    */

    public override void Initialize(GameGrid gameGrid, bool isMovable = true)
    {
        base.Initialize(gameGrid, isMovable);

        _HUD = Instantiate(_HUDPrefab);
        _HUD.Initialize(this);
        _isHUDActive = false;
        _HUD.gameObject.SetActive(_isHUDActive);
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

    public void ReversePolarity()
    {
        _polarity = !_polarity;

      
        _positivePolarity.SetActive(_polarity);
        _negativePolarity.SetActive(!_polarity);
        
    }

    public virtual void PerformAction()
    {
    }

    public virtual void RevertAction()
    {
    }
}
