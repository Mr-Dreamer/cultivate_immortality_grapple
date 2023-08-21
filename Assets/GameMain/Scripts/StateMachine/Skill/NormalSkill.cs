// ---------------------------------------------------------------
// 文件名称：NormalSkill.cs
// 创 建 者：赵志伟
// 创建时间：2023/08/19
// 功能描述：
// ---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NormalSkill", menuName = "Skill/NormalSkill")]
public class NormalSkill : CombatSkillBase
{
    public override void InvokeSkill()
    {
        if (m_Animator.CheckAnimationTag("Motion") && m_SkillIsDone)
        {
            //�����ܱ����� ����û���������ͷž���
            if (m_AICombatSystem.GetCurrentTargetDistance() > m_SkillUseDistance + 0.1f)
            {
                m_CharacterMovementBase.CharacterMoveInterface(m_AICombatSystem.GetDirectionForTarget(), 1.4f, true);

                m_Animator.SetFloat(verticalID, 1f, 0.25f, Time.deltaTime);
                m_Animator.SetFloat(horizontalID, 0f, 0.25f, Time.deltaTime);
                //animator.SetFloat(runID, 1f, 0.25f, Time.deltaTime);
            }
            else
            {
                UseSkill();
            }
        }
    }
}
