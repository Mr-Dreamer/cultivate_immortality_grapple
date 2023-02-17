// ---------------------------------------------------------------
// 文件名称：SwordPerson.cs
// 创 建 者：赵志伟
// 创建时间：2023/02/13
// 功能描述：巨剑和武士刀人物控制相关逻辑
// ---------------------------------------------------------------
using UnityEngine;
using UnityEngine.InputSystem;

public class SwordPersonController : MonoBehaviour
{
    public float ForwardSpeed = 2.0f;
    public float BackwardSpeed = 1.5f;
    private Animator m_Animator;
    private float m_TargetSpeed;
    private float m_CurrentSpeed;
    private Rigidbody m_Rigidbody;

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
        m_Animator.SetFloat("Speed", m_CurrentSpeed);
        //Debug.Log(m_Rigidbody.velocity.y);
        Vector3 vector3 = new Vector3(m_Animator.velocity.x, m_Rigidbody.velocity.y, m_Animator.velocity.z);
        m_Rigidbody.velocity = vector3;
    }

    /// <summary>
    /// 检测移动输入
    /// </summary>
    /// <param name="callbackContext"></param>
    public void PlayerMove(InputAction.CallbackContext callbackContext)
    {
        Vector2 movement = callbackContext.ReadValue<Vector2>();
        m_TargetSpeed = movement.y > 0 ? ForwardSpeed * movement.y : BackwardSpeed * movement.y;
    }
}
