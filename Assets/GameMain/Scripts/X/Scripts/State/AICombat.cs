// ---------------------------------------------------------------
// 文件名称：AICombat.cs
// 创 建 者：赵志伟
// 创建时间：2023/07/06
// 功能描述：
// ---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using Grapple.Move;
using UnityEngine;

namespace Grapple
{
    [CreateAssetMenu(fileName = "AICombat", menuName = "StateMachine/State/AICombat ")]
    public class AICombat : StateActionSO
    {
        private int m_RandomHorizontal;

        public override void OnEnter()
        {
        }

        public override void OnUpdate()
        {
            NoCombatMove();
        }

        public override void OnExit()
        {
        }

        private void NoCombatMove()
        {
            if (m_Animator.CheckAnimationTag("Motion"))
            {
                if (m_AICombatSystem.GetCurrentTarget() == null)
                {
                    Debug.Log("#####zzw##target is null");
                    return;
                }
                if (m_AICombatSystem.GetCurrentTargetDistance() < 2.5f + 0.1f)//AI远离玩家
                {
                    m_AIMovement.CharacterMoveInterface(-m_AICombatSystem.GetDirectionForTargt(), 1.4f, true);
                    m_Animator.SetFloat(m_VerticalID, -1, 0.25f, Time.deltaTime);
                    m_Animator.SetFloat(m_HorizontalID, 0, 0.25f, Time.deltaTime);
                    m_RandomHorizontal = GetRandomHorizontal;

                    if (m_AICombatSystem.GetCurrentTargetDistance() < 1.5f + 0.5f)
                    {
                        m_Animator.Play("Attack_0", 0, 0);
                    }
                }
                else if (m_AICombatSystem.GetCurrentTargetDistance() > 2.5f + 0.1f && m_AICombatSystem.GetCurrentTargetDistance() < 6.1f + 0.5f)//AI左右移动
                {
                    m_AIMovement.CharacterMoveInterface(m_AIMovement.transform.right * (m_RandomHorizontal == 0 ? 1 : m_RandomHorizontal), 1.4f, true);
                    m_Animator.SetFloat(m_VerticalID, 0, 0.25f, Time.deltaTime);
                    m_Animator.SetFloat(m_HorizontalID, m_RandomHorizontal == 0 ? 1 : m_RandomHorizontal, 0.25f, Time.deltaTime);
                }
                else if (m_AICombatSystem.GetCurrentTargetDistance() > 6.1f + 0.5f)//AI朝玩家移动 
                {
                    m_AIMovement.CharacterMoveInterface(m_AIMovement.transform.forward, 1.4f, true);
                    m_Animator.SetFloat(m_VerticalID, 1, 0.25f, Time.deltaTime);
                    m_Animator.SetFloat(m_HorizontalID, 0, 0.25f, Time.deltaTime);
                }
            }
            else
            {
                m_Animator.SetFloat(m_VerticalID, 0);
                m_Animator.SetFloat(m_HorizontalID, 0);
                m_Animator.SetFloat(m_RunID, 0);
            }
        }

        private int GetRandomHorizontal => Random.Range(-1, 2);
    }
}