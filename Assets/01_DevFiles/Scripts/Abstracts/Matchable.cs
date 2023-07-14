using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActiveState { Active, Passive }

public abstract class Matchable : MonoBehaviour
{
    
    public ActiveState activate = ActiveState.Passive;

    public virtual void ChangeState(ActiveState _state)
    {
        activate = _state;
    }
}
