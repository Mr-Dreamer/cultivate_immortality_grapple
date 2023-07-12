using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grapple;
using Grapple.Move;

public abstract class StateActionSO : ScriptableObject
{
    [SerializeField] protected int statePriority;//״̬���ȼ�

    public virtual void OnEnter(StateMachineSystem stateMachineSystem) { }

    public abstract void OnUpdate();

    public virtual void OnExit() { }

    public int GetStatePriority() => statePriority;

    public Animator m_Animator;

    public AICombatSystem m_AICombatSystem;

    public AIMovement m_AIMovement;

    public int VerticalID;
    public int HorizontalID;
    public int RunID;
}
