// ---------------------------------------------------------------
// 文件名称：CharacterMovementBase.cs
// 创 建 者：赵志伟
// 创建时间：2023/03/19
// 功能描述：角色基类(所有角色→玩家/敌人)
// ---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Grapple.Move
{
    public abstract class CharacterMovementBase : MonoBehaviour
    {
        protected Animator m_CharacterAnimator;
        protected CharacterController m_CharacterController;
        protected CharacterInputSystem m_CharacterInputSystem;

        protected Vector3 m_MovementDirection;
        protected Vector3 m_VerticalDirection;

        [SerializeField, Header("移动速度")]
        protected float m_CharacterGravity;
        [SerializeField]
        protected float m_CharacterCurrentMoveSpeed;
        protected float m_CharacterFallTime = 0.15f;
        protected float m_CharacterFallOutDeltaTime;
        protected float m_VerticalSpeed;
        protected float m_MaxVerticalSpeed = 53f;

        [SerializeField, Header("地面检测")] protected float m_GroundDetectionRang;
        [SerializeField] protected float m_GroundDetectionOffset;
        [SerializeField] protected float m_SlopRayExtent;
        [SerializeField] protected LayerMask m_WhatIsGround;
        [SerializeField, Tooltip("角色动画移动时检测障碍物的层级")] protected LayerMask m_WhatIsObs;
        [SerializeField] protected bool m_IsOnGround;

        protected int m_AnimationMoveID = Animator.StringToHash("AnimationMove");
        protected int m_MovementID = Animator.StringToHash("Movement");
        protected int m_HorizontalID = Animator.StringToHash("Horizontal");
        protected int m_VerticalID = Animator.StringToHash("Vertical");
        protected int m_RunID = Animator.StringToHash("Run");

        protected virtual void Awake()
        {
            m_CharacterAnimator = GetComponentInChildren<Animator>();
            m_CharacterController = GetComponent<CharacterController>();
            m_CharacterInputSystem = GetComponent<CharacterInputSystem>();
        }

        protected virtual void Start()
        {
            m_CharacterFallOutDeltaTime = m_CharacterFallTime;
        }

        protected virtual void Update()
        {

        }

        /// <summary>
        /// 角色重力
        /// </summary>
        private void CharacterGravity()
        {
            if (m_IsOnGround)
            {
                m_CharacterFallOutDeltaTime = m_CharacterFallTime;
                if (m_VerticalSpeed < 0.0f)
                {
                    m_VerticalSpeed = -2;
                }
            }
            else
            {
                if (m_CharacterFallOutDeltaTime >= 0.0f)
                {
                    m_CharacterFallOutDeltaTime -= Time.deltaTime;
                    m_CharacterFallOutDeltaTime = Mathf.Clamp(m_CharacterFallOutDeltaTime, 0, m_CharacterFallTime);
                }
            }

            if (m_VerticalSpeed < m_MaxVerticalSpeed)
            {
                m_VerticalSpeed += m_CharacterGravity * Time.deltaTime;
            }
        }

        /// <summary>
        /// 地面检测
        /// </summary>
        private void CheckOnGround()
        {
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - m_GroundDetectionOffset, transform.position.z);
            m_IsOnGround = Physics.CheckSphere(spherePosition, m_GroundDetectionRang, m_WhatIsGround, QueryTriggerInteraction.Ignore);
        }

        private void OnDrawGizmosSelected()
        {

            if (m_IsOnGround)
                Gizmos.color = Color.green;
            else
                Gizmos.color = Color.red;

            Vector3 position = Vector3.zero;

            position.Set(transform.position.x, transform.position.y - m_GroundDetectionOffset,
                transform.position.z);

            Gizmos.DrawWireSphere(position, m_GroundDetectionRang);
        }

        /// <summary>
        /// 坡度检测
        /// </summary>
        /// <param name="dir">当前移动方向</param>
        /// <returns></returns>
        protected Vector3 ResetMoveDirectionOnSlop(Vector3 dir)
        {
            if (Physics.Raycast(transform.position, -Vector3.up, out var hit, m_SlopRayExtent))
            {
                float newAnle = Vector3.Dot(Vector3.up, hit.normal);
                if (newAnle != 0 && m_VerticalSpeed <= 0)
                {
                    return Vector3.ProjectOnPlane(dir, hit.normal);
                }
            }
            return dir;
        }
    }
}
