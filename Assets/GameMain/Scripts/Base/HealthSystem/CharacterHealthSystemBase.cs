// ---------------------------------------------------------------
// 文件名称：CharacterHealthSystemBase.cs
// 创 建 者：赵志伟
// 创建时间：2023/07/06
// 功能描述：角色生命基类
// ---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using Grapple;
using Grapple.Combat;
using Grapple.Move;
using UnityEngine;

public class CharacterHealthSystemBase : MonoBehaviour, IDamagar
{
    protected Animator m_Animator;
    protected CharacterMovementBase m_Movement;
    protected CharacterCombatSystemBase m_CombatSystem;
    protected AudioSource m_AudioSource;

    /// <summary>
    /// 当前攻击者
    /// </summary>
    protected Transform m_CurrentAttacker;

    protected int m_AnimationMove = Animator.StringToHash("m_AnimationMove");

    /// <summary>
    /// 受伤移动距离倍率
    /// </summary>
    public float m_HitmAnimationMoveMult;


    protected virtual void Awake()
    {
        m_Animator = GetComponentInChildren<Animator>();
        m_Movement = GetComponent<CharacterMovementBase>();
        m_CombatSystem = GetComponentInChildren<CharacterCombatSystemBase>();
        m_AudioSource = m_Movement.GetComponentInChildren<AudioSource>();
    }


    protected virtual void Update()
    {
        HitAnimaitonMove();
    }


    /// <summary>
    /// 设置攻击者
    /// </summary>
    /// <param name="attacker">攻击者</param>
    public virtual void SetAttacker(Transform attacker)
    {
        if (m_CurrentAttacker != attacker || m_CurrentAttacker == null)
            m_CurrentAttacker = attacker;
    }

    /// <summary>
    /// 受伤动画位移
    /// </summary>
    protected virtual void HitAnimaitonMove()
    {
        if (!m_Animator.CheckAnimationTag("Hit")) return;
        m_Movement.CharacterMoveInterface(transform.forward, m_Animator.GetFloat(m_AnimationMove) * m_HitmAnimationMoveMult, true);
    }

    #region 接口

    public virtual void TakeDamager(float damager)
    {
    }

    public virtual void TakeDamager(string hitAnimationName)
    {
        m_Animator.Play(hitAnimationName, 0, 0f);
        GameAssetsManager.Instance.PlaySound(m_AudioSource, SoundAssetsType.Hit);
    }

    public virtual void TakeDamager(float damager, string hitAnimationName)
    {
    }

    public virtual void TakeDamager(float damagar, string hitAnimationName, Transform attacker)
    {
        SetAttacker(attacker);
    }

    #endregion

    /// <summary>
    /// 弹刀动画
    /// </summary>
    /// <param name="animationName"></param>
    public void FlickWeapon(string animationName)
    {
        m_Animator.Play(animationName, 0, 0f);
    }
}
