using System.Collections;
using System.Collections.Generic;
using Grapple;
using UnityEngine;


public abstract class ConditionSO : ScriptableObject
{
    [SerializeField] protected int priority;

    public AICombatSystem m_AICombatSystem;

    public virtual void Init(StateMachineSystem stateSystem) { }
    
    public abstract bool ConditionSetUp();

    public int GetConditionPriority() => priority;
}
