// ---------------------------------------------------------------
// 文件名称：ToCombatCondition.cs
// 创 建 者：赵志伟
// 创建时间：2023/07/07
// 功能描述：
// ---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using Grapple;
using UnityEngine;

[CreateAssetMenu(fileName = "ToCombatCondition", menuName = "StateMachine/Condition/ToCombatCondition")]
 public class ToCombatCondition : ConditionSO
{
    public override bool ConditionSetUp()
    {
        return (m_AICombatSystem.GetCurrentTarget() == null ? false : true);
    }
}
