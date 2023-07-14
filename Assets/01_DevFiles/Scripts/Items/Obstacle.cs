using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, IAttackable
{
    public bool Attack(ActiveState _state)
    {
        return false;
    }

    
}
