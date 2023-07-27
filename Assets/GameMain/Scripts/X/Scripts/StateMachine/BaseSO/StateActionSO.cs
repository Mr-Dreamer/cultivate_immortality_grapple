using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grapple;
using Grapple.Move;
using Grapple.Health;

public abstract class StateActionSO : ScriptableObject
{
    protected AICombatSystem m_AICombatSystem;
    protected AIMovement m_AIMovement;
    protected AIHealthSystem m_HealthSystem;
    protected Animator m_Animator;
    protected Transform m_Self;

    [SerializeField, Header("优先级")] protected int m_StatePriority;

    protected int m_AnimationMoveID = Animator.StringToHash("AnimationMove");
    protected int m_AIMovementID = Animator.StringToHash("Movement");
    protected int m_HorizontalID = Animator.StringToHash("Horizontal");
    protected int m_VerticalID = Animator.StringToHash("Vertical");
    protected int m_LAtkID = Animator.StringToHash("LAtk");
    protected int m_RunID = Animator.StringToHash("Run");

    protected float m_WalkSpeed = 1.5f;
    protected float m_RunSpeed = 5f;
    [SerializeField] protected float m_CurrentMoveSpeed;

    public void InitState(StateMachineSystem stateMachineSystem)
    {
        m_AICombatSystem = stateMachineSystem.GetComponentInChildren<AICombatSystem>();

        m_AIMovement = stateMachineSystem.GetComponent<AIMovement>();

        m_HealthSystem = stateMachineSystem.GetComponent<AIHealthSystem>();

        m_Animator = stateMachineSystem.GetComponentInChildren<Animator>();

        m_Self = stateMachineSystem.transform;
    }

    protected void SetHorizontalAnimation(float value)
    {
        m_Animator.SetFloat(m_HorizontalID, value);
        m_CurrentMoveSpeed = 0.85f;
    }

    protected void SetVerticalAnimation(float value)
    {
        m_Animator.SetFloat(m_VerticalID, value);
        m_CurrentMoveSpeed = 1.5f;
    }


    protected void ResetAnimation()
    {
        m_CurrentMoveSpeed = 0f;
        m_Animator.SetFloat(m_VerticalID, 0);
        m_Animator.SetFloat(m_HorizontalID, 0);

    }

    public virtual void OnEnter() { }

    public abstract void OnUpdate();

    public virtual void OnExit() { }

    public int GetStatePriority() => m_StatePriority;
}
