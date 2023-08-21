// ---------------------------------------------------------------
// 文件名称：IState.cs
// 创 建 者：赵志伟
// 创建时间：2023/08/21
// 功能描述：
// ---------------------------------------------------------------
using System;
public interface IState
{
    void InitState(MainStateMachineController machineController);
    void OnEnterState();
    void OnStateTick();
    void OnEndState();
}
