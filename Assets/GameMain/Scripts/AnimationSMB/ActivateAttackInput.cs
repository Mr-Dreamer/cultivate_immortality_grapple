using System.Collections;
using System.Collections.Generic;
using Grapple.Combat;
using UnityEngine;

public enum DetectionAttack
{
    Attack,
    Parry,
    Roll,
    Hit,
    Dead
}

public class ActivateAttackInput : StateMachineBehaviour
{
    [SerializeField] private DetectionAttack detectionAttack;

    private PlayerCombatSystem combatSystem;

    [SerializeField] private float maxAllowAttackTime;
    private float currentAllowAttackTime;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //��ȡ��ҹ���ϵͳ�ű�
        if (combatSystem == null) combatSystem = animator.GetComponent<PlayerCombatSystem>();

        currentAllowAttackTime = maxAllowAttackTime;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        //�����ǰ���������빥���ź��ټ�ʱ ��ʱ��ﵽ �������빥���ź�
        if (!combatSystem.GetAllowAttackInput())
        {
            if (currentAllowAttackTime > 0)
            {
                currentAllowAttackTime -= Time.deltaTime;

                if (currentAllowAttackTime <= 0)
                {
                    combatSystem.SetAllowAttackInput(true);
                }
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
