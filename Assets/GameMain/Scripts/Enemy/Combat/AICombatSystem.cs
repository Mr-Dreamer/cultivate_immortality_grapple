// ---------------------------------------------------------------
// 文件名称：AICombatSystem.cs
// 创 建 者：赵志伟
// 创建时间：2023/07/05
// 功能描述：AI攻击类
// ---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grapple.Combat;
using System;

namespace Grapple
{
    public class AICombatSystem : CharacterCombatSystemBase
    {
        [SerializeField, Header("检测中心点")]
        private Transform m_DetectionCenter;
        [SerializeField, Header("检测半径")]
        private float m_DetectionRang;
        [SerializeField, Header("敌人层")]
        private LayerMask m_WhatIsEnemy;
        [SerializeField, Header("障碍物层")]
        private LayerMask m_WhatIsBos;
        [SerializeField, Header("当前目标")]
        private Transform m_CurrentTarget;

        private int m_LockOn = Animator.StringToHash("LockOn");

        Collider[] m_ColliderTarget = new Collider[1];
        private Collider[] m_DetectionedTarget = new Collider[1];

        [SerializeField] private float m_AnimationMoveMult;

        [SerializeField, Header("���ܴ���")] private List<CombatSkillBase> m_Skills = new List<CombatSkillBase>();

        private void Update()
        {
            AIView();
            LockOnTarget();
        }

        /// <summary>
        /// AI检测敌对玩家并记录当前敌人对象
        /// </summary>
        private void AIView()
        {
            int targetCount = Physics.OverlapSphereNonAlloc(m_DetectionCenter.position, m_DetectionRang , m_ColliderTarget, m_WhatIsEnemy);
            if (targetCount > 0)
            {
                if (!Physics.Raycast((transform.root.position + transform.root.up * 0.5f), (m_ColliderTarget[0].transform.position - transform.root.position).normalized, out var hit, m_DetectionRang, m_WhatIsBos))
                {
                    if (Vector3.Dot((m_ColliderTarget[0].transform.position - transform.root.position).normalized, transform.root.forward) > 0.25f)
                    {
                        m_CurrentTarget = m_ColliderTarget[0].transform;
                    }
                }
            }
        }

        /// <summary>
        /// 调整AI朝向，并修改锁敌状态
        /// </summary>
        private void LockOnTarget()
        {
            if (m_Animator.CheckAnimationTag("Motion") && m_CurrentTarget != null)
            {
                m_Animator.SetFloat(m_LockOn, 1);
                transform.root.rotation = transform.LockOnTarget(m_CurrentTarget, transform.root, 50);
            }
            else
            {
                m_Animator.SetFloat(m_LockOn, 0);  
            }
        }

        /// <summary>
        /// 获取当前敌人对象
        /// </summary>
        /// <returns>敌人对象Transform</returns>
        public Transform GetCurrentTarget()
        {
            return m_CurrentTarget;
        }

        /// <summary>
        /// 更新翻滚/攻击时的位移
        /// </summary>
        private void UpdateAnimationMove()
        {
            if (m_Animator.CheckAnimationTag("Roll"))
            {
                m_CharacterMovementBase.CharacterMoveInterface(transform.root.forward, m_Animator.GetFloat(m_AnimationMoveID) * m_AnimationMoveMult, true);
            }

            if (m_Animator.CheckAnimationTag("Attack"))
            {
                m_CharacterMovementBase.CharacterMoveInterface(transform.root.forward, m_Animator.GetFloat(m_AnimationMoveID) * m_AnimationMoveMult, true);
            }
        }

        /// <summary>
        /// 在攻击状态下是否允许调整朝向
        /// </summary>
        private void OnAnimatorActionAutoLockON()
        {
            if (CanAttackLockOn())
            {
                if (m_Animator.CheckAnimationTag("Attack") || m_Animator.CheckAnimationTag("GSAttack"))
                {
                    transform.root.rotation = transform.LockOnTarget(m_CurrentTarget, transform.root.transform, 50f);
                }
            }
        }

        /// <summary>
        /// 检测动画播放的进度来决定是否朝向敌人
        /// </summary>
        /// <returns>是否允许朝向敌人</returns>
        private bool CanAttackLockOn()
        {
            if (m_Animator.CheckAnimationTag("Attack") || m_Animator.CheckAnimationTag("GSAttack"))
            {
                if (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.75f)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 检索敌人
        /// </summary>
        private void DetectionTarget()
        {
            //敌人个数
            int targetCount = Physics.OverlapSphereNonAlloc(m_DetectionCenter.position, m_AttackDetectionRang, m_DetectionedTarget, m_EnemyLayer);

            //取第一个
            if (targetCount > 0)
            {
                SetCurrentTarget(m_DetectionedTarget[0].transform);
            }
        }

        /// <summary>
        /// 设置AI当前目标
        /// </summary>
        /// <param name="target">目标对象</param>
        private void SetCurrentTarget(Transform target)
        {
            if (m_CurrentTarget == null || m_CurrentTarget != target)
            {
                m_CurrentTarget = target;
            }
        }

        /// <summary>
        /// 初始化所有技能
        /// </summary>
        private void InitAllSkill()
        {
            if (m_Skills.Count == 0) return;

            for (int i = 0; i < m_Skills.Count; i++)
            {
                m_Skills[i].InitSkill(m_Animator, this, m_CharacterMovementBase);

                //�����ǰ���ܲ�����ʹ��
                if (!m_Skills[i].GetSkillIsDone())
                {
                    //����
                    m_Skills[i].ResetSkill();
                }
            }
        }

        public CombatSkillBase GetAnDoneSkill()
        {
            for (int i = 0; i < m_Skills.Count; i++)
            {
                if (m_Skills[i].GetSkillIsDone()) return m_Skills[i];
                else continue;
            }

            return null;
        }

        public CombatSkillBase GetSkillUseName(string name)
        {
            for (int i = 0; i < m_Skills.Count; i++)
            {
                if (m_Skills[i].GetSkillName().Equals(name)) return m_Skills[i];
                else continue;
            }

            return null;
        }

        public CombatSkillBase GetSkillUseID(int id)
        {
            for (int i = 0; i < m_Skills.Count; i++)
            {
                if (m_Skills[i].GetSkillID() == id) return m_Skills[i];
                else continue;
            }

            return null;
        }

        public float GetCurrentTargetDistance() => Vector3.Distance(m_CurrentTarget.position, transform.root.position);

        public Vector3 GetDirectionForTarget() => (m_CurrentTarget.position - transform.root.position).normalized;
    }
}