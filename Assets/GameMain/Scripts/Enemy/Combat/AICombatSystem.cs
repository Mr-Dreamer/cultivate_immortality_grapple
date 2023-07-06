// ---------------------------------------------------------------
// 文件名称：AICombatSystem.cs
// 创 建 者：赵志伟
// 创建时间：2023/07/05
// 功能描述：AI攻击类
// ---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grapple.Combat;

namespace Grapple
{
    public class AICombatSystem : CharacterCombatSystemBase
    {
        [SerializeField, Header("检测中心点")]
        private Transform m_DedectionCenter;
        [SerializeField, Header("检测半径")]
        private float m_DetectionRang;
        [SerializeField, Header("敌人层")]
        private LayerMask m_WhatIsEnemy;
        [SerializeField, Header("障碍物层")]
        private LayerMask m_WhatIsBos;
        [SerializeField, Header("当前目标")]
        private Transform m_CurrentTarget;

        Collider[] m_ColliderTarget = new Collider[1];

        private void Update()
        {
            AIView();
        }

        /// <summary>
        /// AI检测敌对玩家
        /// </summary>
        private void AIView()
        {
            int targetCount = Physics.OverlapSphereNonAlloc(m_DedectionCenter.position, m_AttackDetectionRang, m_ColliderTarget, m_WhatIsEnemy);
            if (targetCount > 0)
            {
                if (!Physics.Raycast((transform.position + transform.root.up * 0.5f), (m_ColliderTarget[0].transform.position - transform.root.position).normalized, out var hit, m_WhatIsBos))
                {
                    if (Vector3.Dot((m_ColliderTarget[0].transform.position - transform.position).normalized, transform.root.forward) > 0.25f)
                    {
                        m_CurrentTarget = m_ColliderTarget[0].transform;
                    }
                }
            }
        }

        //private void OnDrawGizmos()
        //{
        //    Gizmos.DrawWireSphere(m_DedectionCenter.position, m_DetectionRang);
        //}
    }
}