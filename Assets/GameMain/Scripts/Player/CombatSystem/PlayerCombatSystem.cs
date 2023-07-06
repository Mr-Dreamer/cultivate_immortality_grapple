// ---------------------------------------------------------------
// 文件名称：PlayerCombatSystem.cs
// 创 建 者：
// 创建时间：2023/06/26
// 功能描述：玩家攻击类
// ---------------------------------------------------------------
using UnityEngine;
using System.Collections;

namespace Grapple.Combat
{
    public class PlayerCombatSystem : CharacterCombatSystemBase
    {
        /// <summary>
        /// 当前攻击目标
        /// </summary>
        [SerializeField] private Transform m_CurrentTarget; 
        //Speed
        [SerializeField, Header("攻击移动速度倍率"), Range(.1f, 10f)]
        private float m_AttackMoveMult;

        //检测
        [SerializeField, Header("检测敌人")] private Transform m_DetectionCenter;
        [SerializeField] private float m_DetectionRang;

        /// <summary>
        /// 缓存检测到的目标
        /// </summary>
        private Collider[] m_DetectionedTarget = new Collider[1];

        private void Update()
        {
            PlayerAttackAction();
            DetectionTarget();
            ActionMotion();
            UpdateCurrentTarget();
        }

        private void LateUpdate()
        {
            OnAttackActionLockON();
        }

        /// <summary>
        /// 获取玩家输入，是否播放攻击动画
        /// </summary>
        private void PlayerAttackAction()
        {
            if (m_CharacterInputSystem.PlayerRAtk)
            {
                if (m_CharacterInputSystem.PlayerLAtk)
                {
                    m_Animator.SetTrigger(m_LAtkID);

                }
            }
            else
            {
                if (m_CharacterInputSystem.PlayerLAtk)
                {
                    m_Animator.SetTrigger(m_LAtkID);

                }
            }

            m_Animator.SetBool(m_sWeapon, m_CharacterInputSystem.PlayerRAtk);
        }

        private void OnAttackActionLockON()
        {
            if (CanAttackLockOn())
            {
                if (m_Animator.CheckAnimationTag("Attack") || m_Animator.CheckAnimationTag("GSAttack"))
                {
                    transform.root.rotation = transform.LockOnTarget(m_CurrentTarget.transform, transform.root.transform, 50f);
                }
            }
        }

        /// <summary>
        /// 处理攻击时的位移
        /// </summary>
        private void ActionMotion()
        {
            if (m_Animator.CheckAnimationTag("Attack") || m_Animator.CheckAnimationTag("GSAttack"))
            {
                m_CharacterMovementBase.CharacterMoveInterface(transform.forward, m_Animator.GetFloat(m_AnimationMoveID) * m_AttackMoveMult, true);
            }
        }

        #region 动作检测

        /// <summary>
        /// 攻击状态是否允许自动锁定敌人
        /// </summary>
        /// <returns></returns>
        private bool CanAttackLockOn()
        {
            if (m_CurrentTarget == null)
            {
                return false;
            }
            if (m_Animator.CheckAnimationTag("Attack") || m_Animator.CheckAnimationTag("GSAttack"))
            {
                if (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.75f) //攻击动画还未播放完毕
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 检测目标
        /// </summary>
        private void DetectionTarget()
        {
            int targetCount = Physics.OverlapSphereNonAlloc(m_DetectionCenter.position, m_DetectionRang, m_DetectionedTarget,
                m_EnemyLayer);
            if (targetCount > 0)
            {
                SetCurrentTarget(m_DetectionedTarget[0].transform );
            }
        }

        private void SetCurrentTarget(Transform target)
        {
            if (m_CurrentTarget == null || m_CurrentTarget != target)
            {
                m_CurrentTarget = target;
            }
        }

        private void UpdateCurrentTarget()
        {
            if (m_Animator.CheckAnimationTag("Motion"))
            {
                if (m_CharacterInputSystem.PlaerMovement.sqrMagnitude > 0)
                {
                    m_CurrentTarget = null;
                }
            }
        }

        #endregion
    }
}
