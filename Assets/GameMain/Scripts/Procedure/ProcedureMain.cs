// ---------------------------------------------------------------
// �ļ����ƣ�ProcedureMain.cs
// �� �� �ߣ���־ΰ
// ����ʱ�䣺2023/02/10
// ����������
// ---------------------------------------------------------------
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace Grapple
{
    public class ProcedureMain : ProcedureBase
    {
        public override bool UseNativeDialog => throw new System.NotImplementedException();

        // ��Ϸ��ʼ��ʱִ�С�
        protected override void OnInit(ProcedureOwner procedureOwner)
        {
            base.OnInit(procedureOwner);
        }

        // ÿ�ν����������ʱִ�С�
        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
        }

        // ÿ����ѯִ�С�
        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
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