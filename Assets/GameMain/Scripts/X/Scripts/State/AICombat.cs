// ---------------------------------------------------------------
// 文件名称：AICombat.cs
// 创 建 者：赵志伟
// 创建时间：2023/07/06
// 功能描述：
// ---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AICombat", menuName = "StateMachine/State/AICombat")]
public class AICombat : StateActionSO
{
    public override void OnUpdate()
    {
        Debug.Log("#####zzw##combat");
    }
}
