using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineObjectPool : MonoBehaviour
{
    [System.Serializable]
    class Machine
    {
        [SerializeField] public MachineObject MachineObjectPrefab;
        [SerializeField] public Vector2Int SpawnPosition;
    }

    [Header("Prefabs")]
    [SerializeField] private GameGrid _gameGrid;
    [SerializeField] private Machine[] _machineObjects;
    [SerializeField] private ActionRecorder _actionRecorder;
    
    [Header("Configs")]
    [SerializeField] private bool _drawGizmos;

    private int _currentSequenceNumber = 0;
    private List<MachineObject> _machines = new();

    public void Record()
    {
        if (_currentSequenceNumber < _machines.Count)
        {
            Action action = new Action(_machines[_currentSequenceNumber]);
            _actionRecorder.Record(action);
            _currentSequenceNumber++;
        }
    }

    public void Rewind()
    {
        if (_currentSequenceNumber > 0)
        {
            _actionRecorder.Rewind();
            _currentSequenceNumber--;
        }
    }

    public void InitializeMachines()
    {
        foreach(Machine machine in _machineObjects)
        {
            int x = machine.SpawnPosition.x;
            int z = machine.SpawnPosition.y;

            MachineObject machineObject = Instantiate(machine.MachineObjectPrefab);
            machineObject.Initialize(_gameGrid);
            _machines.Add(machineObject);
            if (!_gameGrid.TryDropObject(x, z, machineObject))
                Debug.LogError($"Machine Position [{x},{z}] already has an object or is out of bounds!");
            
        }
    }
    
    #region Gizmos
    private void DrawMachineGizmos(int x, int z)
    {
        Gizmos.color = new Color(1f, 1f, 1f, 0.5f);
        Gizmos.DrawCube(new Vector3(x, 0.5f, z), new Vector3(0.9f, 0.9f, 0.9f));

        Gizmos.color = new Color(1f, 1f, 1f, 1f);
        Gizmos.DrawWireCube(new Vector3(x, 0.5f, z), new Vector3(0.9f, 0.9f, 0.9f));
    }

    private void OnDrawGizmos()
    {
        if (!_drawGizmos || Application.isPlaying)
            return;
        
        foreach (Machine machine in _machineObjects)
        {
            int x = machine.SpawnPosition.x;
            int z = machine.SpawnPosition.y;
            DrawMachineGizmos(x, z);
        }
    }

    #endregion
}
