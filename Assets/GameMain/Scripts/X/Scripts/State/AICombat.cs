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
        private int m_RandomHorizontal;

        public override void OnEnter(StateMachineSystem stateMachineSystem)
        {
        }

        public override void OnUpdate()
        {
        }

        public override void OnExit()
        {
        }

        private void NoCombatMove()
        {
            if (m_Animator.CheckAnimationTag("Motion"))
            {
                if (m_AICombatSystem.GetCurrentTargetDistance < 2.5f + 0.1f)
                {

                }
            }
        }
    }

}