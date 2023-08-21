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
    protected Animator _animator;
    protected List<CharacterStateActionBaseSO> _actions;


    public void InitState(MainStateMachineController machineController)
    {
        _animator = machineController.GetComponentInChildren<Animator>();
        _actions = new List<CharacterStateActionBaseSO>();
    }

    public virtual void OnEnterState()
    {

    }

    public abstract void OnStateTick();


    public virtual void OnEndState()
    {

    }
}
