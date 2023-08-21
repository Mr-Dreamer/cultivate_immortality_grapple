// ---------------------------------------------------------------
// 文件名称：CombatSkillBase.cs
// 创 建 者：赵志伟
// 创建时间：2023/08/17
// 功能描述：攻击技能基类
// ---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using Grapple;
using Grapple.Move;
using UnityEngine;

public abstract class CombatSkillBase : ScriptableObject
{
    [SerializeField] protected string m_SkillName;
    [SerializeField] protected int m_SkillID;
    [SerializeField] protected float m_SkillCDTime;
    [SerializeField] protected float m_SkillUseDistance;
    [SerializeField] protected bool m_SkillIsDone;

    protected Animator m_Animator;
    protected AICombatSystem m_AICombatSystem;
    protected CharacterMovementBase m_CharacterMovementBase;

    //AnimationID
    protected int horizontalID = Animator.StringToHash("Horizontal");
    protected int verticalID = Animator.StringToHash("Vertical");
    protected int runID = Animator.StringToHash("Run");

    /// <summary>
    /// 执行某个技能
    /// </summary>
    public abstract void InvokeSkill();

    protected void UseSkill()
    {
        m_Animator.Play(m_SkillName, 0, 0f);
        m_SkillIsDone = false;
        ResetSkill();
    }

    public void ResetSkill()
    {
        GameObjectPoolSystem.Instance.TakeGameObject("Timer").GetComponent<Timer>().CreateTime(m_SkillCDTime, () => m_SkillIsDone = true, false);
    }

    public void InitSkill(Animator _animator, AICombatSystem _combat, CharacterMovementBase _movement)
    {
        this.m_Animator = _animator;
        this.m_AICombatSystem = _combat;
        this.m_CharacterMovementBase = _movement;
    }

    public string GetSkillName() => m_SkillName;

    public int GetSkillID() => m_SkillID;

    public bool GetSkillIsDone() => m_SkillIsDone;

    public void SetSkillIsDone(bool done) => m_SkillIsDone = done;
}
