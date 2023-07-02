// ---------------------------------------------------------------
// 文件名称：AnimationResetTrigger.cs
// 创 建 者：赵志伟
// 创建时间：2023/07/02
// 功能描述：用于重置动画变量状态，防止从A到B后，状态变量未重置会影响到B
// ---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationResetTrigger : StateMachineBehaviour
{
    public string[] Trigger;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (var item in Trigger)
        {
            animator.ResetTrigger(item);
        }
    }
}
