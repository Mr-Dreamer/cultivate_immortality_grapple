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
        protected int m_Roll = Animator.StringToHash("Roll");

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
            CharacterGravity();
            CheckOnGround();
        }

        /// <summary>
        /// 角色重力
        /// </summary>
        private void CharacterGravity()
        {
            //如果角色处于地面
            if (m_IsOnGround)
            {
                //重置下落时间
                m_CharacterFallOutDeltaTime = m_CharacterFallTime;
                //在地面时阻止速度无限下降
                if (m_VerticalSpeed < 0.0f)
                {
                    m_VerticalSpeed = -2;
                }
            }
            else
            {
                //如果角色下落时间大于0 就证明处于下落状态
                if (m_CharacterFallOutDeltaTime >= 0.0f)
                {
                    //限制下落时间
                    m_CharacterFallOutDeltaTime -= Time.deltaTime;
                    //随着时间的推移减少
                    m_CharacterFallOutDeltaTime = Mathf.Clamp(m_CharacterFallOutDeltaTime, 0, m_CharacterFallTime);
                }
            }

            //如果当前角色Y轴速度小于最大Y轴速度
            if (m_VerticalSpeed < m_MaxVerticalSpeed)
            {
                //重力加速度
                m_VerticalSpeed += m_CharacterGravity * Time.deltaTime;
            }
        }

        /// <summary>
        /// 地面检测
        /// </summary>
        private void CheckOnGround()
        {
            //设置球体检测位置 在玩家当前的Y轴方向稍微向下偏移
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - m_GroundDetectionOffset, transform.position.z);
            //开始检测
            //m_IsOnGround = Physics.CheckSphere(spherePosition, m_GroundDetectionRang, m_WhatIsGround, QueryTriggerInteraction.Ignore);
            m_IsOnGround = true;
        }

        private void OnDrawGizmosSelected()
        {

            //if (m_IsOnGround)
            //    Gizmos.color = Color.green;
            //else
            //    Gizmos.color = Color.red;

            //Vector3 position = Vector3.zero;

            //position.Set(transform.position.x, transform.position.y - m_GroundDetectionOffset,
            //    transform.position.z);

            //Gizmos.DrawWireSphere(position, m_GroundDetectionRang);
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
                //计算角色上方与射线碰撞到的法向量点积
                float newAnle = Vector3.Dot(Vector3.up, hit.normal);
                //如果不等于0 并且角色当前Y轴速度小于等于0
                if (newAnle != 0 && m_VerticalSpeed <= 0)
                {
                    //返回一个平面投影向量
                    return Vector3.ProjectOnPlane(dir, hit.normal);
                }
            }
            return dir;
        }

        protected bool CanAnimationMotion(Vector3 dir)
        {
            return Physics.Raycast(transform.position + transform.up * .5f, dir.normalized * m_CharacterAnimator.GetFloat(m_AnimationMoveID), out var hit, 1f, m_WhatIsObs);
        }

        /// <summary>
        /// 移动接口,处理了坡度和重力
        /// </summary>
        /// <param name="moveDirection">移动方向</param>
        /// <param name="moveSpeed">移动速度</param>
        public virtual void CharacterMoveInterface(Vector3 moveDirection, float moveSpeed, bool useGravity)
        {
            //如果移动方向的前方没有障碍物
            if (!CanAnimationMotion(moveDirection))
            {
                //移动方向标准化
                m_MovementDirection = moveDirection.normalized;

                //对当前移动方向进行坡度检测
                m_MovementDirection = ResetMoveDirectionOnSlop(m_MovementDirection);

                //如果使用重力
                if (useGravity)
                {
                    //给垂直向量Y轴赋值
                    m_VerticalDirection.Set(0.0f, m_VerticalSpeed, 0.0f);
                }
                else
                {
                    //归零
                    m_VerticalDirection = Vector3.zero;
                }

                //移动
                m_CharacterController.Move((moveSpeed * Time.deltaTime)
                    * m_MovementDirection.normalized + Time.deltaTime
                    * m_VerticalDirection);
            }
        }
    }
}
