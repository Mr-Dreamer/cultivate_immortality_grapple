// ---------------------------------------------------------------
// 文件名称：CharacterCombatSystemBase.cs
// 创 建 者：
// 创建时间：2023/06/26
// 功能描述：攻击基类
// ---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using Grapple.Move;
using UnityEngine;

namespace Grapple.Combat
{
    public class CharacterCombatSystemBase : MonoBehaviour
    {
        protected Animator m_Animator;
        protected CharacterInputSystem m_CharacterInputSystem;
        protected CharacterMovementBase m_CharacterMovementBase;
        [SerializeField]
        protected AudioSource m_AudioSource;


        //动画状态机中变量参数的哈希值
        protected int m_LAtkID = Animator.StringToHash("LAtk");
        protected int m_RAtkID = Animator.StringToHash("RAtk");
        protected int m_DefenID = Animator.StringToHash("Defen");
        protected int m_AnimationMoveID = Animator.StringToHash("AnimationMove"); 
        protected int m_sWeapon = Animator.StringToHash("SWeapon"); 

         //攻击检测
         [SerializeField, Header("攻击检测")] protected Transform m_AttackDetectionCenter;
        [SerializeField] protected float m_AttackDetectionRang;
        [SerializeField] protected LayerMask m_EnemyLayer;

        protected virtual void Awake()
        {
            m_Animator = GetComponent<Animator>();
            m_CharacterInputSystem = GetComponentInParent<CharacterInputSystem>();
            m_CharacterMovementBase = GetComponentInParent<CharacterMovementBase>();
            m_AudioSource = m_CharacterMovementBase.GetComponentInChildren<AudioSource>();
        }





        /// <summary>
        /// 攻击动画攻击检测事件
        /// </summary>
        /// <param name="hitName">传递受伤动画名</param>
        protected virtual void OnAnimationAttackEvent(string hitName)
        {
            Collider[] attackDetectionTargets = new Collider[4];

            int counts = Physics.OverlapSphereNonAlloc(m_AttackDetectionCenter.position, m_AttackDetectionRang,
                attackDetectionTargets, m_EnemyLayer);

            if (counts > 0)
            {
                for (int i = 0; i < counts; i++)
                {
                    if (attackDetectionTargets[i].TryGetComponent(out IDamagar damagar))
                    {
                        damagar.TakeDamager(0, hitName, transform.root.transform);

                    }
                }
            }
            PlayerWeaponSound();
        }

        private void PlayerWeaponSound()
        {
            if (m_Animator.CheckAnimationTag("Attack"))
            {
                GameAssetsManager.Instance.PlaySound(m_AudioSource, SoundAssetsType.SwordWave);
            }
            if (m_Animator.CheckAnimationTag("GSAttack"))
            {
                GameAssetsManager.Instance.PlaySound(m_AudioSource, SoundAssetsType.GSwordWave);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(m_AttackDetectionCenter.position, m_AttackDetectionRang);
        }
    }
}