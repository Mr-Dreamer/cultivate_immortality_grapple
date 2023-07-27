using System.Collections;
using System.Collections.Generic;
using Grapple;
using UnityEngine;
using Grapple.Move;
using Grapple.Health;

public abstract class ConditionSO : ScriptableObject
{
    protected AICombatSystem m_AICombatSystem;
    protected AIMovement m_AIMovement;
    protected AIHealthSystem m_AIHealthSystem;
    protected Animator m_Animator;

    [SerializeField] protected int m_Priority;



    public void InitCondition(StateMachineSystem stateSystem)
    {
        m_AICombatSystem = stateSystem.GetComponentInChildren<AICombatSystem>();

        m_AIMovement = stateSystem.GetComponent<AIMovement>();

        m_AIHealthSystem = stateSystem.GetComponent<AIHealthSystem>();

        m_Animator = stateSystem.GetComponentInChildren<Animator>();
    }

    public abstract bool ConditionSetUp();

    public int GetConditionPriority() => m_Priority;
}
