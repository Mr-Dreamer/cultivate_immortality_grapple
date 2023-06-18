// ---------------------------------------------------------------
// 文件名称：PlayerMovementController.cs
// 创 建 者：
// 创建时间：2023/03/30
// 功能描述：
// ---------------------------------------------------------------
using Grapple.Move;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Grapple.Move
{
    public class PlayerMovementController : CharacterMovementBase
    {
        //引用
        private Transform m_CharacterCamera;
        private TP_CameraController m_TpCameraController;

        [SerializeField, Header("相机锁定点")] private Transform m_StandCameraLook;
        [SerializeField] private Transform m_CrouchCameraLook;

        //Ref Value
        private float m_TargetRotation;
        private float m_RotationVelocity;//玩家当前角度（没多大作用，方法参数要求）

        //LerpTime
        [SerializeField, Header("旋转速度")] private float m_RotationLerpTime;
        [SerializeField] private float m_MoveDirctionSlerpTime;


        //Move Speed
        [SerializeField, Header("移动速度")] private float m_WalkSpeed;
        [SerializeField, Header("移动速度")] private float m_RunSpeed;
        [SerializeField, Header("移动速度")] private float m_CrouchMoveSpeed;


        [SerializeField, Header("角色胶囊控制(下蹲)")] private Vector3 m_CrouchCenter;
        [SerializeField] private Vector3 m_OriginCenter;
        [SerializeField] private Vector3 m_CameraLookPositionOnCrouch;
        [SerializeField] private Vector3 m_CameraLookPositionOrigin;
        [SerializeField] private float m_CrouchHeight;
        [SerializeField] private float m_OriginHeight;
        [SerializeField] private bool m_IsOnCrouch;
        [SerializeField] private Transform m_CrouchDetectionPosition;
        [SerializeField] private Transform m_CameraLook;
        [SerializeField] private LayerMask m_CrouchDetectionLayer;

        //animationID
        private int crouchID = Animator.StringToHash("Crouch");


        #region 内部函数

        protected override void Awake()
        {
            base.Awake();

            m_CharacterCamera = Camera.main.transform.root.transform;
            m_TpCameraController = m_CharacterCamera.GetComponent<TP_CameraController>();
        }

        protected override void Start()
        {
            base.Start();


            m_CameraLookPositionOrigin = m_CameraLook.position;
        }

        protected override void Update()
        {
            base.Update();

            PlayerMoveDirection();

        }

        private void LateUpdate()
        {
            CharacterCrouchControl();
            UpdateMotionAnimation();
            UpdateCrouchAnimation();
            UpdateRollAnimation();

        }

        #endregion



        #region 条件

        private bool CanMoveContro()
        {
            return m_IsOnGround && m_CharacterAnimator.CheckAnimationTag("Motion") || m_CharacterAnimator.CheckAnimationTag("CrouchMotion");
        }

        private bool CanCrouch()
        {
            if (m_CharacterAnimator.CheckAnimationTag("Crouch")) return false;
            if (m_CharacterAnimator.GetFloat(m_RunID) > .9f) return false;

            return true;
        }


        private bool CanRunControl()
        {
            if (Vector3.Dot(m_MovementDirection.normalized, transform.forward) < 0.75f) return false;
            if (!CanMoveContro()) return false;


            return true;
        }

        #endregion


        private void PlayerMoveDirection()
        {

            if (m_IsOnGround && m_CharacterInputSystem.PlaerMovement == Vector2.zero)
                m_MovementDirection = Vector3.zero;

            if (CanMoveContro())
            {
                if (m_CharacterInputSystem.PlaerMovement != Vector2.zero)
                {
                    //相机的目前的角度+移动的角度=目标角度，也就是玩家将要朝向的角度
                    m_TargetRotation = Mathf.Atan2(m_CharacterInputSystem.PlaerMovement.x, m_CharacterInputSystem.PlaerMovement.y) * Mathf.Rad2Deg + m_CharacterCamera.localEulerAngles.y;

                    //平滑阻尼角度，在rotationLerpTime时间内将玩家朝向targetRotation
                    transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, m_TargetRotation, ref m_RotationVelocity, m_RotationLerpTime);

                    var direction = Quaternion.Euler(0f, m_TargetRotation, 0f) * Vector3.forward;

                    direction = direction.normalized;

                    m_MovementDirection = Vector3.Slerp(m_MovementDirection, ResetMoveDirectionOnSlop(direction),
                        m_MoveDirctionSlerpTime * Time.deltaTime);

                }
            }
            else
            {
                m_MovementDirection = Vector3.zero;
            }

            m_CharacterController.Move((m_CharacterCurrentMoveSpeed * Time.deltaTime)
                * m_MovementDirection.normalized + Time.deltaTime
                * new Vector3(0.0f, m_VerticalSpeed, 0.0f));


        }


        private void UpdateMotionAnimation()
        {

            if (CanRunControl())
            {
                m_CharacterAnimator.SetFloat(m_MovementID, m_CharacterInputSystem.PlaerMovement.sqrMagnitude * ((m_CharacterInputSystem.PlayerRun && !m_IsOnCrouch) ? 2f : 1f), 0.1f, Time.deltaTime);

                m_CharacterCurrentMoveSpeed = (m_CharacterInputSystem.PlayerRun && !m_IsOnCrouch) ? m_RunSpeed : m_WalkSpeed;
            }
            else
            {
                m_CharacterAnimator.SetFloat(m_MovementID, 0f, 0.05f, Time.deltaTime);
                m_CharacterCurrentMoveSpeed = 0f;
            }

            m_CharacterAnimator.SetFloat(m_RunID, (m_CharacterInputSystem.PlayerRun && !m_IsOnCrouch) ? 1f : 0f);
        }

        private void UpdateCrouchAnimation()
        {
            if (m_IsOnCrouch)
            {
                m_CharacterCurrentMoveSpeed = m_CrouchMoveSpeed;
            }

        }

        private void UpdateRollAnimation()
        {

        }

        private void CharacterCrouchControl()
        {
            if (!CanCrouch()) return;

            if (m_CharacterInputSystem.PlayerCrouch)
            {

                if (m_IsOnCrouch)
                {
                    if (!DetectionHeadHasObject())
                    {
                        m_IsOnCrouch = false;
                        m_CharacterAnimator.SetFloat(crouchID, 0f);
                        SetCrouchColliderHeight(m_OriginHeight, m_OriginCenter);
                        m_TpCameraController.SetLookPlayerTarget(m_StandCameraLook);
                    }

                }
                else
                {
                    m_IsOnCrouch = true;
                    m_CharacterAnimator.SetFloat(crouchID, 1f);
                    SetCrouchColliderHeight(m_CrouchHeight, m_CrouchCenter);
                    m_TpCameraController.SetLookPlayerTarget(m_CrouchCameraLook);
                }
            }
        }


        private void SetCrouchColliderHeight(float height, Vector3 center)
        {
            m_CharacterController.center = center;
            m_CharacterController.height = height;

        }


        private bool DetectionHeadHasObject()
        {
            Collider[] hasObjects = new Collider[1];

            int objectCount = Physics.OverlapSphereNonAlloc(m_CrouchDetectionPosition.position, 0.5f, hasObjects, m_CrouchDetectionLayer);

            if (objectCount > 0)
            {
                return true;
            }

            return false;
        }
    }
}