// ---------------------------------------------------------------
// 文件名称：SwordPerson.cs
// 创 建 者：赵志伟
// 创建时间：2023/02/13
// 功能描述：巨剑和武士刀人物控制相关逻辑(old)
// ---------------------------------------------------------------
using UnityEngine;
using UnityEngine.InputSystem;

public class SwordPersonController : MonoBehaviour
{
    /// <summary>
    /// 向前行走速度
    /// </summary>
    public float ForwardSpeed = 2.0f;

    /// <summary>
    /// 向后行走速度
    /// </summary>
    public float BackwardSpeed = 1.5f;

    /// <summary>
    /// 向前奔跑速度
    /// </summary>
    public float ForwardRunSpeed = 4.58f;

    /// <summary>
    /// 前后的斜45度行走的速度
    /// </summary>
    public float LRSpeed = 0.747f;

    /// <summary>
    /// 按键后的最终速度
    /// </summary>
    private float m_TargetSpeed;

    /// <summary>
    /// 按键后的斜45度行走的最终速度
    /// </summary>
    private float m_TargetLRSpeed;

    /// <summary>
    /// 按键后的当前速度
    /// </summary>
    private float m_CurrentSpeed;

    /// <summary>
    /// 按键后的斜45度方向的当前速度
    /// </summary>
    private float m_CurrentLRSpeed;

    private Animator m_Animator;
    private Rigidbody m_Rigidbody;

    /// <summary>
    /// 是否奔跑状态
    /// </summary>
    private bool m_IsRunning = false;

    private void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnAnimatorMove()
    {
        Move();
    }

    /// <summary>
    /// 巨剑人物移动
    /// </summary>
    private void Move()
    {
        m_CurrentSpeed = Mathf.Lerp(m_TargetSpeed, m_CurrentSpeed, 0.9f);
        m_CurrentLRSpeed = Mathf.Lerp(m_TargetLRSpeed, m_CurrentLRSpeed, 0.9f);
        m_Animator.SetFloat("Vertical Speed", m_CurrentSpeed);
        m_Animator.SetFloat("Horizontal Speed", m_CurrentLRSpeed);
        Vector3 vector3 = new Vector3(m_Animator.velocity.x, m_Rigidbody.velocity.y, m_Animator.velocity.z);
        m_Rigidbody.velocity = vector3;
    }

    /// <summary>
    /// 检测移动输入
    /// </summary>
    /// <param name="callbackContext"></param>
    public void GetPlayerMoveInput(InputAction.CallbackContext callbackContext)
    {
        Vector2 movement = callbackContext.ReadValue<Vector2>();
        if (m_IsRunning)
        {
            Debug.Log("Run");
            m_TargetSpeed = movement.y > 0 ? ForwardRunSpeed * movement.y : BackwardSpeed * movement.y;
        }
        else
        {
            Debug.Log("Wallk");
            m_TargetSpeed = movement.y > 0 ? ForwardSpeed * movement.y : BackwardSpeed * movement.y;
        }

        m_TargetLRSpeed = movement.x > 0 ? ForwardSpeed * movement.x : BackwardSpeed * movement.x;

        //m_PlayerInput = callbackContext.ReadValue<Vector2>();
        //Debug.Log(m_PlayerInput);
    }

    public void GetPlayerRunInput(InputAction.CallbackContext callbackContext)
    {
        m_IsRunning = callbackContext.ReadValue<float>() > 0;
        Debug.Log(m_IsRunning);
    }
}
