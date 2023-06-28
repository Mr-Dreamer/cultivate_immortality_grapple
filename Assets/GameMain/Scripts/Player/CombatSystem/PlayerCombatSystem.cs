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
        //Speed
        [SerializeField, Header("攻击移动速度倍率"), Range(.1f, 10f)]
        private float m_AttackMoveMult;

        //检测
        [SerializeField, Header("检测敌人")] private Transform m_DetectionCenter;
        [SerializeField] private float m_DetectionRang;

        //缓存
        private Collider[] m_DetectionedTarget = new Collider[1];

        private void Update()
        {
            PlayerAttackAction();
            DetectionTarget();
            ActionMotion();
        }

        /// <summary>
        /// 获取玩家输入，是否播放攻击动画
        /// </summary>
        private void PlayerAttackAction()
        {
            if (m_CharacterInputSystem.PlayerLAtk)
            {
                m_Animator.SetTrigger(m_LAtkID);

            }
        }

        /// <summary>
        /// 处理攻击时的位移
        /// </summary>
        private void ActionMotion()
        {
            if (m_Animator.CheckAnimationTag("Attack"))
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
            if (m_Animator.CheckAnimationTag("Attack"))
            {
                if (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.75f)
                {
                    return true;
                }
            }
            return false;
        }


        private void DetectionTarget()
        {
            int targetCount = Physics.OverlapSphereNonAlloc(m_DetectionCenter.position, m_DetectionRang, m_DetectionedTarget,
                m_EnemyLayer);

            //TODO后续功能补充
        }

        #endregion
    }
}
