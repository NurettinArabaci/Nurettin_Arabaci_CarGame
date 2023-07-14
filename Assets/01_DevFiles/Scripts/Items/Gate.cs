using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Gate : Matchable
{
    private Collider _collider;
    private MeshRenderer _mesh;


    private void Awake()
    {
        Initialize();

    }

    //caches components and set
    private void Initialize()
    {
        _collider = GetComponent<Collider>();
        _mesh = GetComponent<MeshRenderer>();

        _collider.isTrigger = true;
    }

    // if you reached the gate, this function will run
    public void EnterGate()
    {
        if (activate == ActiveState.Passive) return;
        
        GateController.Instance.ChangeTransform();

    }

    //the gate changed active or passive
    public override void ChangeState(ActiveState _state)
    {
        base.ChangeState(_state);
        _mesh.enabled = activate == ActiveState.Active;
    }


}
