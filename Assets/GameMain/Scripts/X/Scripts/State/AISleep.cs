// ---------------------------------------------------------------
// 文件名称：AISleep.cs
// 创 建 者：赵志伟
// 创建时间：2023/07/06
// 功能描述：
// ---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AISleep", menuName = "StateMachine/State/AISleep")]
public class AISleep : StateActionSO
{
    public override void OnUpdate()
    {
        Debug.Log("#####zzw##Sleep"); 
    }
}
