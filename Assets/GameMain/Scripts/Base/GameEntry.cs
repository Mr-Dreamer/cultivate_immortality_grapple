// ---------------------------------------------------------------
// �ļ����ƣ�GameEntry.cs
// �� �� �ߣ���־ΰ
// ����ʱ�䣺2022/12/1
// ������������Ϸ���-�������(����)�ĳ�ʼ��
// ---------------------------------------------------------------
using UnityEngine;

namespace Grapple
{
    /// <summary>
    /// ��Ϸ���
    /// </summary>
    public partial class GameEntry : MonoBehaviour
    {
        private void Start()
        {
            //��ʼ���������
            InitBuiltinComponents();

            //��ʼ���Զ������
            InitCustomComponents();

            //��ʼ���Զ��������
            InitCustomDebuggers();
        }
    }
}