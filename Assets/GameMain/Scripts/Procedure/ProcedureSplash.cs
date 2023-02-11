// ---------------------------------------------------------------
// 文件名称：ProcedureSplash.cs
// 创 建 者：赵志伟
// 创建时间：2023/02/11
// 功能描述：资源加载模式流程的选择
// ---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.Resource;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace Grapple
{
    public class ProcedureSplash : ProcedureBase
    {
        private int a;
        public override bool UseNativeDialog { get => true; }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            // TODO: 这里可以播放一个 Splash 动画
            // ...

            if (GameEntry.Base.EditorResourceMode)
            {
                Log.Info("编辑器资源加载模式");
                ChangeState<ProcedurePreload>(procedureOwner);
            }
            else if (GameEntry.Resource.ResourceMode == ResourceMode.Package)
            {
                Log.Info("单机资源加载模式");
            }
            else
            {
                Log.Info("可更新资源加载模式");
            }
        }
    }
}
