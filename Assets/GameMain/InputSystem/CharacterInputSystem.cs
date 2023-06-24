// ---------------------------------------------------------------
// 文件名称：CharacterInputSystem.cs
// 创 建 者：赵志伟
// 创建时间：2023/03/19
// 功能描述：获取输入值
// ---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInputSystem : MonoBehaviour
{
    private InputController m_InputController;

    public Vector2 PlaerMovement
    {
        get => m_InputController.PlayerInput.Movement.ReadValue<Vector2>();
    }

    public Vector2 CameraLook
    {
        get => m_InputController.PlayerInput.CameraLook.ReadValue<Vector2>();
    }

    public bool PlayerRun
    {
        get => m_InputController.PlayerInput.Run.phase == InputActionPhase.Performed;
    }
    public bool PlayerLAtk
    {
        get => m_InputController.PlayerInput.LAtk.triggered;
    }

    public bool PlayerRAtk
    {
        get => m_InputController.PlayerInput.RAtk.triggered;
    }
    public bool PlayerDefen
    {
        get => m_InputController.PlayerInput.Defen.phase == InputActionPhase.Performed;
    }

    public bool PlayerRoll
    {
        get => m_InputController.PlayerInput.Roll.triggered;
    }

    public bool PlayerCrouch
    {
        get => m_InputController.PlayerInput.Crouch.triggered;
    }

    //内部函数
    private void Awake()
    {
        if (m_InputController == null)
            m_InputController = new InputController();
    }

    private void OnEnable()
    {
        m_InputController.Enable();
    }

    private void OnDisable()
    {
        m_InputController.Disable();
    }
}
