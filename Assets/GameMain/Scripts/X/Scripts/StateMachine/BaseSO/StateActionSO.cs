using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grapple;

public abstract class StateActionSO : ScriptableObject
{
    [SerializeField] protected int statePriority;//״̬���ȼ�

    public virtual void OnEnter(StateMachineSystem stateMachineSystem) { }

    public abstract void OnUpdate();

    public virtual void OnExit() { }

    public int GetStatePriority() => statePriority;

    public Animator m_Animator;

    public AICombatSystem m_AICombatSystem;
}
