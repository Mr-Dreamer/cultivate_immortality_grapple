// ---------------------------------------------------------------
// 文件名称：AIMovement.cs
// 创 建 者：赵志伟
// 创建时间：2023/07/06
// 功能描述：AI移动
// ---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grapple.Move
{
    public class AIMovement : CharacterMovementBase
    {
        protected override void Update()
        {
            base.Update();

            UpdateGrvity();
        }

        /// <summary>
        /// 更新怪物垂直方向上的移动
        /// </summary>
        private void UpdateGrvity()
        {
            m_VerticalDirection.Set(0f, m_VerticalSpeed, 0f);
            m_CharacterController.Move(Time.deltaTime * m_VerticalDirection);
        }
    }
}