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
}
