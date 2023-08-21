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

        [SerializeField] private CombatSkillBase m_CurrentSkill;

        public override void OnEnter()
        {
        }

        public override void OnUpdate()
        {
            AICombatAction();
        }

        public override void OnExit()
        {
        }

        private void AICombatAction()
        {
            if (m_CurrentSkill == null)
            {
                NoCombatMove();
                GetSkill();
            }
            else
            {
                m_CurrentSkill.InvokeSkill();

                if (!m_CurrentSkill.GetSkillIsDone())
                {
                    m_CurrentSkill = null;
                }
            }
        }

        private void GetSkill()
        {
            if (m_CurrentSkill == null)
            {
                m_CurrentSkill = m_AICombatSystem.GetAnDoneSkill();
            }
        }

        private void NoCombatMove()
        {
            if (m_Animator.CheckAnimationTag("Motion"))
            {
                //Debug.Log("#####zzw##" + m_AICombatSystem.GetCurrentTargetDistance());
                if (m_AICombatSystem.GetCurrentTargetDistance() < 2f + 0.1f)//AI远离玩家
                {
                    m_AIMovement.CharacterMoveInterface(-m_AICombatSystem.GetDirectionForTarget(), 1.4f, true);
                    m_Animator.SetFloat(m_VerticalID, -1, 0.25f, Time.deltaTime);
                    m_Animator.SetFloat(m_HorizontalID, 0, 0.25f, Time.deltaTime);
                    m_RandomHorizontal = GetRandomHorizontal;

                    if (m_AICombatSystem.GetCurrentTargetDistance() < 1.5f + 0.5f)
                    {
                        if (!m_Animator.CheckAnimationTag("Hit") || !m_Animator.CheckAnimationTag("Defen"))
                        {
                            m_Animator.Play("Attack_0", 0, 0f);

                            m_RandomHorizontal = GetRandomHorizontal;
                        }
                    }
                }
                else if (m_AICombatSystem.GetCurrentTargetDistance() > 2.5f + 0.1f && m_AICombatSystem.GetCurrentTargetDistance() < 6.1f + 0.5f)//AI左右移动
                {
                    if (HorizontalDirectionHasObject(m_RandomHorizontal))
                    {
                        switch (m_RandomHorizontal)
                        {
                            case 1:
                                m_RandomHorizontal = -1;
                                break;
                            case -1:
                                m_RandomHorizontal = 1;
                                break;
                            default:
                                break;
                        }
                    }

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

        private bool HorizontalDirectionHasObject(int direction)
        {
            return Physics.Raycast(m_AIMovement.transform.position, m_AIMovement.transform.right * direction, 1.5f, 1 << 8);
        }

        private int GetRandomHorizontal => Random.Range(-1, 2);
    }
}