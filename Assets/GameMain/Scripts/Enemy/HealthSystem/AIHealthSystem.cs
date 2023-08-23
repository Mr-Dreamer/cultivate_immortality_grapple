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
        [SerializeField] private int m_MaxParryCount;
        [SerializeField] private int m_CounterattackParryCount;//当格挡次数大于设置的值 触发反击技能

        [SerializeField] private int m_MaxHitCount;
        [SerializeField] private int m_HitCount;//如果受伤次数超过最大受伤次数 触发脱身技能

        private void Start()
        {
            m_HitCount = 0;
        }

        private void LateUpdate()
        {
            OnHitLockTarget(); 
        }

        public override void TakeDamager(float damagar, string hitAnimationName, Transform attacker)
        {
            SetAttacker(attacker);

            if (m_MaxParryCount > 0 && !OnInvincibleState())
            {
                //如果反击格挡次数等于2
                if (m_CounterattackParryCount == 2)
                {
                    //触发反击技能
                    m_Animator.Play("CounterAttack", 0, 0f);
                    m_CounterattackParryCount = 0;
                    GameAssetsManager.Instance.PlaySound(m_AudioSource, SoundAssetsType.Parry);
                }
                else
                {
                    OnParry(hitAnimationName);
                }
                m_MaxParryCount--;
            }
            else
            {
                if (m_HitCount == m_MaxHitCount && !m_Animator.CheckAnimationTag("Flick_0"))
                {
                    //触发脱身技能
                    m_Animator.Play("Roll_B", 0, 0f);

                    m_HitCount = 0;
                    m_MaxHitCount += Random.Range(1, 4);
                }
                else
                {
                    if (!OnInvincibleState())
                    {
                        m_Animator.Play(hitAnimationName, 0, 0f);
                        GameAssetsManager.Instance.PlaySound(m_AudioSource, SoundAssetsType.Hit);
                        m_HitCount++;
                    }
                }
            }
        }

        /// <summary>
        /// 处于处决状态无敌不受到伤害
        /// </summary>
        private bool OnInvincibleState()
        {
            if (m_Animator.CheckAnimationTag("CounterAttack")) return true;

            return false;
        }

        private void OnHitLockTarget()
        {
            if (m_Animator.CheckAnimationTag("Hit"))
            {
                transform.rotation = transform.LockOnTarget(m_CurrentAttacker, transform, 50);
            }
        }

        private void OnParry(string hitName)
        {
            switch (hitName)
            {
                default:
                    m_Animator.Play(hitName, 0, 0f);
                    GameAssetsManager.Instance.PlaySound(m_AudioSource, SoundAssetsType.Hit);
                    break;
                case "Hit_D_Up":
                    m_Animator.Play("ParryF", 0, 0f);
                    GameAssetsManager.Instance.PlaySound(m_AudioSource, SoundAssetsType.Parry);
                    m_CounterattackParryCount++;
                    break;
                case "Hit_H_Left":
                    m_Animator.Play("ParryR", 0, 0f);
                    GameAssetsManager.Instance.PlaySound(m_AudioSource, SoundAssetsType.Parry);
                    m_CounterattackParryCount++;
                    break;
                case "Hit_H_Right":
                    m_Animator.Play("ParryL", 0, 0f);
                    GameAssetsManager.Instance.PlaySound(m_AudioSource, SoundAssetsType.Parry);
                    m_CounterattackParryCount++;
                    break;
                case "Hit_Up_Left":
                    m_Animator.Play("ParryR", 0, 0f);
                    GameAssetsManager.Instance.PlaySound(m_AudioSource, SoundAssetsType.Parry);
                    m_CounterattackParryCount++;
                    break;
                case "Hit_Up_Right":
                    m_Animator.Play("ParryL", 0, 0f);
                    GameAssetsManager.Instance.PlaySound(m_AudioSource, SoundAssetsType.Parry);
                    m_CounterattackParryCount++;
                    break;
            }
        }
    }
}