// ---------------------------------------------------------------
// 文件名称：CharacterStateBaseSO.cs
// 创 建 者：赵志伟
// 创建时间：2023/08/21
// 功能描述：
// ---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterStateBaseSO : ScriptableObject, IState
{
    protected Animator m_Animator;
    protected List<CharacterStateActionBaseSO> m_Actions;


    public void InitState(MainStateMachineController machineController)
    {
        m_Animator = machineController.GetComponentInChildren<Animator>();
        m_Actions = new List<CharacterStateActionBaseSO>();
    }

    public virtual void OnEnterState()
    {

    }

    public abstract void OnStateTick();


    public virtual void OnEndState()
    {

    }
}
