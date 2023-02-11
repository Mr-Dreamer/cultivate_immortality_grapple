using GameFramework.Procedure;
using System.Collections;
using System.Collections.Generic;
// ---------------------------------------------------------------
// 文件名称：ProcedureLaunch.cs
// 创 建 者：赵志伟
// 创建时间：2023/02/10
// 功能描述：入口流程(运行游戏后的第一个流程)
// ---------------------------------------------------------------
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace Grapple
{
    public class ProcedureLaunch : ProcedureBase
    {
        public override bool UseNativeDialog => throw new System.NotImplementedException();

        // 游戏初始化时执行
        protected override void OnInit(ProcedureOwner procedureOwner)
        {
            base.OnInit(procedureOwner);
            Log.Info("#####zzw##OnInit");
        }

        // 每次进入这个流程时执行。
        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            Log.Info("#####zzw##OnEnter");
        }

        // 每次轮询执行。
        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            Log.Info("#####zzw");
            ChangeState<ProcedureMain>(procedureOwner); // 切换流程到 ProcedureMain
        }

        // 每次离开这个流程时执行。
        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
        }

        // 游戏退出时执行。
        protected override void OnDestroy(ProcedureOwner procedureOwner)
        {
            base.OnDestroy(procedureOwner);
        }
    }
}