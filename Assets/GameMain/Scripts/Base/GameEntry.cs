using System.Collections;
using System.Collections.Generic;
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