// ---------------------------------------------------------------
// 文件名称：AIHealthSystem.cs
// 创 建 者：赵志伟
// 创建时间：2023/07/06
// 功能描述：AI生命系统
// ---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grapple.Health
{
    public class AIHealthSystem : CharacterHealthSystemBase
    {
        private void LateUpdate()
        {
            OnHitLockTarget(); 
        }

        public override void TakeDamager(float damagar, string hitAnimationName, Transform attacker)
        {
            SetAttacker(attacker);
            m_Animator.Play(hitAnimationName, 0, 0f);
            GameAssetsManager.Instance.PlaySound(m_AudioSource, SoundAssetsType.Hit);
        }

        private void OnHitLockTarget()
        {
            if (m_Animator.CheckAnimationTag("Hit"))
            {
                transform.rotation = transform.LockOnTarget(m_CurrentAttacker, transform, 50);
            }
        }
    }
}