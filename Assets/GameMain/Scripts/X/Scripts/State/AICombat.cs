// ---------------------------------------------------------------
// 文件名称：AICombat.cs
// 创 建 者：赵志伟
// 创建时间：2023/07/06
// 功能描述：
// ---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grapple
{
    [CreateAssetMenu(fileName = "AICombat", menuName = "StateMachine/State/AICombat ")]
    public class AICombat : StateActionSO
    {
        public override void OnUpdate()
        {
            Debug.Log("#####zzw##combat");
        }

        ///// <summary>
        ///// 如果不能攻击就逃跑
        ///// </summary>
        //private void NoCombat()
        //{
        //    if (m_Animator.CheckAnimationTag("Motion"))
        //    {
        //        if (m_AICombatSystem.GetCurrentTargetDistance < 4.5f + 0.1f)
        //        {
        //            m_AIMovement.CharacterMoveInterface(-m_AIMovement.transform.forward, 1.5f, true);
        //            //m_Animator.SetFloat(m)
        //        }
        //    }
        //}
    }

}