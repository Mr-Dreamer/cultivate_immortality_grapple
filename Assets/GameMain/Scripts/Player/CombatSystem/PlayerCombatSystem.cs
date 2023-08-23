// ---------------------------------------------------------------
// 文件名称：PlayerCombatSystem.cs
// 创 建 者：赵志伟
// 创建时间：2023/06/26
// 功能描述：玩家攻击类
// ---------------------------------------------------------------
using UnityEngine;
using System.Collections;

namespace Grapple.Combat
{
    public class PlayerCombatSystem : CharacterCombatSystemBase
    {
        private PlayerHealthSystem m_HealthSystem;

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

        //允许攻击输入
        [SerializeField] private bool allowAttackInput;

        private void Update()
        {
            PlayerAttackAction();
            DetectionTarget();
            ActionMotion();
            UpdateCurrentTarget();
            PlayerParryInput();
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
            //当玩家处于Motion状态(idle)也允许玩家输入攻击信号
            if (!allowAttackInput)
            {
                if (m_Animator.CheckCurrentTagAnimationTimeIsExceed("Motion", 0.01f) && !m_Animator.IsInTransition(0))
                {
                    SetAllowAttackInput(true);
                }
            }

            //如果玩按下鼠标左键
            if (m_CharacterInputSystem.PlayerLAtk && allowAttackInput)
            {
                if (m_HealthSystem.GetCanExecute())
                {
                    //播放处决动画
                    m_Animator.Play("Execute_0", 0, 0f);

                    Time.timeScale = 1f;
                }
                else
                {
                    //触发默认攻击动画
                    m_Animator.SetTrigger(m_LAtkID);

                    SetAllowAttackInput(false);
                }
            }

            //如果玩家一直按住鼠标右键
            if (m_CharacterInputSystem.PlayerRAtk)
            {
                //并且按下左键
                if (m_CharacterInputSystem.PlayerLAtk)
                {
                    //触发大剑攻击动画
                    m_Animator.SetTrigger(m_LAtkID);

                    SetAllowAttackInput(false);
                }
            }

            m_Animator.SetBool(m_sWeapon, m_CharacterInputSystem.PlayerRAtk);
        }

        private void PlayerParryInput()
        {
            if (CanInputParry())
            {
                m_Animator.SetBool(m_DefenID, m_CharacterInputSystem.PlayerDefen);
            }
            else
            {
                m_Animator.SetBool(m_DefenID, false);
            }
        }

        private bool CanInputParry()
        {
            if (m_Animator.CheckAnimationTag("Motion")) return true;
            if (m_Animator.CheckAnimationTag("Parry")) return true;
            if (m_Animator.CheckCurrentTagAnimationTimeIsExceed("Hit", 0.07f)) return true;

            return false;
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

        /// <summary>
        /// 获取当前是否允许玩家攻击输入
        /// </summary>
        /// <returns></returns>
        public bool GetAllowAttackInput() => allowAttackInput;

        /// <summary>
        /// 设置是否允许玩家输入攻击信号 
        /// </summary>
        /// <param name="allow"></param>
        public void SetAllowAttackInput(bool allow) => allowAttackInput = allow;
    }
}
