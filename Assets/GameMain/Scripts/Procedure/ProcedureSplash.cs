// ---------------------------------------------------------------
// �ļ����ƣ�ProcedureSplash.cs
// �� �� �ߣ���־ΰ
// ����ʱ�䣺2023/02/11
// ������������Դ����ģʽ���̵�ѡ��
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

            // TODO: ������Բ���һ�� Splash ����
            // ...

            if (GameEntry.Base.EditorResourceMode)
            {
                Log.Info("�༭����Դ����ģʽ");
                ChangeState<ProcedurePreload>(procedureOwner);
            }
            else if (GameEntry.Resource.ResourceMode == ResourceMode.Package)
            {
                Log.Info("������Դ����ģʽ");
            }
            else
            {
                Log.Info("�ɸ�����Դ����ģʽ");
            }
        }
    }
}
