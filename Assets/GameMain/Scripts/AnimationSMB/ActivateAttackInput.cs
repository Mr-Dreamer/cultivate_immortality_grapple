using System.Collections;
using System.Collections.Generic;
using Grapple.Combat;
using UnityEngine;

public enum DetectionAttack
{
    Attack,
    Parry,
    Roll,
    Hit,
    Dead
}

public class ActivateAttackInput : StateMachineBehaviour
{
    [SerializeField] private DetectionAttack m_DetectionAttack;

    private PlayerCombatSystem m_CombatSystem;

    [SerializeField] private float m_MaxAllowAttackTime;
    private float m_CurrentAllowAttackTime;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (m_CombatSystem == null) m_CombatSystem = animator.GetComponent<PlayerCombatSystem>();

        m_CurrentAllowAttackTime = m_MaxAllowAttackTime;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (!m_CombatSystem.GetAllowAttackInput())
        {
            if (m_CurrentAllowAttackTime > 0)
            {
                m_CurrentAllowAttackTime -= Time.deltaTime;

                if (m_CurrentAllowAttackTime <= 0)
                {
                    m_CombatSystem.SetAllowAttackInput(true);
                }
            }
        }
    }
}