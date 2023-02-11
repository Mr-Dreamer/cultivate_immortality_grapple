using GameFramework.Procedure;
using System.Collections;
using System.Collections.Generic;
// ---------------------------------------------------------------
// �ļ����ƣ�ProcedureLaunch.cs
// �� �� �ߣ���־ΰ
// ����ʱ�䣺2023/02/10
// �����������������(������Ϸ��ĵ�һ������)
// ---------------------------------------------------------------
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace Grapple
{
    public class ProcedureLaunch : ProcedureBase
    {
        public override bool UseNativeDialog => throw new System.NotImplementedException();

        // ��Ϸ��ʼ��ʱִ��
        protected override void OnInit(ProcedureOwner procedureOwner)
        {
            base.OnInit(procedureOwner);
            Log.Info("#####zzw##OnInit");
        }

        // ÿ�ν����������ʱִ�С�
        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            Log.Info("#####zzw##OnEnter");
        }

        // ÿ����ѯִ�С�
        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            Log.Info("#####zzw");
            ChangeState<ProcedureMain>(procedureOwner); // �л����̵� ProcedureMain
        }

        // ÿ���뿪�������ʱִ�С�
        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
        }

        // ��Ϸ�˳�ʱִ�С�
        protected override void OnDestroy(ProcedureOwner procedureOwner)
        {
            base.OnDestroy(procedureOwner);
        }
    }
}