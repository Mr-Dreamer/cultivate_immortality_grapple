// ---------------------------------------------------------------
// 文件名称：TP_CameraController.cs
// 创 建 者：赵志伟
// 创建时间：2023/06/16
// 功能描述：
// ---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_CameraController : MonoBehaviour
{
    private CharacterInputSystem m_PlayerInput;

    [SerializeField] private Transform m_LookAttarGet;
    private Transform m_PlayerCamera;


    [Range(0.1f, 1.0f), SerializeField, Header("鼠标灵敏度")] public float m_MouseInputSpeed;
    [Range(0.1f, 0.5f), SerializeField, Header("相机旋转平滑度")] public float m_RotationSmoothTime = 0.12f;

    [SerializeField, Header("相机对于玩家")] private float m_DistancePlayerOffset;
    [SerializeField, Header("相机对于玩家")] private Vector3 m_OffsetPlayer;
    [SerializeField] private Vector2 m_ClmpCameraRang = new Vector2(-85f, 70f);
    [SerializeField] private float m_LookAtPlayerLerpTime;

    [SerializeField, Header("锁敌")] private bool m_IsLockOn;
    [SerializeField] private Transform m_CurrentTarget;


    [SerializeField, Header("相机碰撞")] private Vector2 mm_CameraDistanceMinMax = new Vector2(0.01f, 3f);
    [SerializeField] private float m_ColliderMotionLerpTime;

    private Vector3 m_RotationSmoothVelocity;
    private Vector3 m_CurrentRotation;
    private Vector3 m_CamDirection;
    private float m_CameraDistance;
    private float m_Yaw;
    private float m_Pitch;

    public LayerMask CollisionLayer;



    private void Awake()
    {
        m_PlayerCamera = Camera.main.transform;
        m_PlayerInput = m_LookAttarGet.transform.root.GetComponent<CharacterInputSystem>();
    }

    private void Start()
    {
        m_CamDirection = transform.localPosition.normalized;

        m_CameraDistance = mm_CameraDistanceMinMax.y;
    }

    private void Update()
    {
        //UpdateCursor();
        GetCameraControllerInput();
    }



    private void LateUpdate()
    {
        ControllerCamera();
        CheckCameraOcclusionAndCollision(m_PlayerCamera);
        //CameraLockOnTarget();
    }


    private void ControllerCamera()
    {
        if (!m_IsLockOn)
        {
            m_CurrentRotation = Vector3.SmoothDamp(m_CurrentRotation, new Vector3(m_Pitch, m_Yaw), ref m_RotationSmoothVelocity, m_RotationSmoothTime);
            transform.eulerAngles = m_CurrentRotation;
        }

        Vector3 fanlePos = m_LookAttarGet.position - transform.forward * m_DistancePlayerOffset;

        transform.position = Vector3.Lerp(transform.position, fanlePos, m_LookAtPlayerLerpTime * Time.deltaTime);
    }

    private void GetCameraControllerInput()
    {
        if (m_IsLockOn) return;

        //m_Yaw += m_PlayerInput..x * m_MouseInputSpeed;
        //m_Pitch -= m_PlayerInput.cameraLook.y * m_MouseInputSpeed;
        m_Pitch = Mathf.Clamp(m_Pitch, m_ClmpCameraRang.x, m_ClmpCameraRang.y);
    }



    private void CheckCameraOcclusionAndCollision(Transform camera)
    {
        Vector3 desiredCamPosition = transform.TransformPoint(m_CamDirection * 3f);

        if (Physics.Linecast(transform.position, desiredCamPosition, out var hit, CollisionLayer))
        {
            m_CameraDistance = Mathf.Clamp(hit.distance * .9f, mm_CameraDistanceMinMax.x, mm_CameraDistanceMinMax.y);

        }
        else
        {
            m_CameraDistance = mm_CameraDistanceMinMax.y;

        }
        camera.transform.localPosition = Vector3.Lerp(camera.transform.localPosition, m_CamDirection * (m_CameraDistance - 0.1f), m_ColliderMotionLerpTime * Time.deltaTime);

    }



    private void UpdateCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }



    private void CameraLockOnTarget()
    {
        if (!m_IsLockOn) return;

        Vector3 directionOfTarget = ((m_CurrentTarget.position + m_CurrentTarget.transform.up * .7f) - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionOfTarget.normalized);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10f * Time.deltaTime);
    }


    public void SetLookPlayerTarget(Transform target)
    {
        m_LookAttarGet = target;
    }
}
