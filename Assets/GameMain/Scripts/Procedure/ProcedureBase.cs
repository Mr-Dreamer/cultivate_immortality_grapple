using System.Collections;
// ---------------------------------------------------------------
// �ļ����ƣ�ProcedureBase.cs
// �� �� �ߣ���־ΰ
// ����ʱ�䣺2023/02/11
// �����������������������
// ---------------------------------------------------------------
using System.Collections.Generic;
using UnityEngine;

namespace Grapple
{
    public abstract class ProcedureBase : GameFramework.Procedure.ProcedureBase
    {
        // ��ȡ�����Ƿ�ʹ��ԭ���Ի���
        // ��һЩ��������̣�����Ϸ�߼��Ի�����Դ�������ǰ�����̣��У����Կ��ǵ���ԭ���Ի��������Ϣ��ʾ��Ϊ
        public abstract bool UseNativeDialog
        {
            get;
        }
    }
}
