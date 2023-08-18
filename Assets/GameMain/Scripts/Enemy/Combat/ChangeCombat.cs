// ---------------------------------------------------------------
// 文件名称：ChangeCombat.cs
// 创 建 者：赵志伟
// 创建时间：2023/08/18
// 功能描述：AI变招
// ---------------------------------------------------------------
using UnityEngine;
using Grapple;

public class ChangeCombat : StateMachineBehaviour
{
    private AICombatSystem m_AICombatSystem;

    [SerializeField] private float m_DetectionTime;

    [SerializeField] private bool m_CanChangeCombat;
    [SerializeField] private bool m_AllowReleaseChangeCombat;
    [SerializeField] private string m_ChangeCombatName;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (m_AICombatSystem == null)
        {
            m_AICombatSystem = animator.GetComponent<AICombatSystem>();
        }

        m_CanChangeCombat = true;
        m_AllowReleaseChangeCombat = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_CanChangeCombat = false;
        m_AllowReleaseChangeCombat = false;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CheckChangeCombatTime(animator);
        ChangeCombatAction(animator);
    }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}

    private void CheckChangeCombatTime(Animator animator)
    {
        if (m_AICombatSystem == null) return;
        if (m_AICombatSystem.GetCurrentTarget() == null) return;

        //�����ǰ����״̬ʱ��С��ָ��ʱ�� ������� ����������
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < m_DetectionTime)
        {
            m_CanChangeCombat = true;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > m_DetectionTime)
        {
            m_CanChangeCombat = false;
        }
    }

    private void ChangeCombatAction(Animator animator)
    {
        if (m_AICombatSystem == null) return;
        if (m_AICombatSystem.GetCurrentTarget() == null) return;

        if (m_CanChangeCombat)
        {
            if (m_AICombatSystem.GetCurrentTargetDistance() < 2.5f)
            {
                //allowReleaseChangeCombat = true;
                animator.CrossFade(m_ChangeCombatName, 0f, 0, 0f);
            }
        }

        if (!m_CanChangeCombat && m_AllowReleaseChangeCombat)
        {
            //animator.CrossFade(changeCombatName, 0f, 0, 0f);
        }
    }
}

