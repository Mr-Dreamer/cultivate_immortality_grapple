using System.Collections;
// ---------------------------------------------------------------
// 文件名称：ProcedureBase.cs
// 创 建 者：赵志伟
// 创建时间：2023/02/11
// 功能描述：所有流程类基类
// ---------------------------------------------------------------
using System.Collections.Generic;
using UnityEngine;

namespace Grapple
{
    public abstract class ProcedureBase : GameFramework.Procedure.ProcedureBase
    {
        // 获取流程是否使用原生对话框
        // 在一些特殊的流程（如游戏逻辑对话框资源更新完成前的流程）中，可以考虑调用原生对话框进行消息提示行为
        public abstract bool UseNativeDialog
        {
            get;
        }
    }
}
