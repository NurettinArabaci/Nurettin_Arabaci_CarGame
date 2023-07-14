using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoSingleton<GateController>
{
    [SerializeField] private List<Gate> _gates = new List<Gate>();

    private int _activeGateIndex = 0;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        for (int i = 0; i < _gates.Count; i++)
        {
            _gates[i].ChangeState(ActiveState.Active);

            if (i > 0)
            {
                _gates[i].ChangeState(ActiveState.Passive);
            }
        }
    }

    public void ChangeTransform()
    {
        _activeGateIndex++;

        foreach (var item in _gates)
        {
            item.ChangeState(ActiveState.Passive);
        }

        if (_activeGateIndex >= _gates.Count) return;
        ActiveGate().ChangeState(ActiveState.Active);
    }

    Gate ActiveGate()
    {
        return _gates[_activeGateIndex];
    }
}
