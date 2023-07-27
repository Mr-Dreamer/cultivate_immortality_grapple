using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineSystem : MonoBehaviour
{
    public NB_Transition Transition;
    public StateActionSO CurrentState;

    private void Awake()
    {
        Transition?.InitTransition(this);
        CurrentState?.OnEnter();
    }

    private void Update()
    {
        StateMachineTick();
    }

    private void StateMachineTick()
    {
        Transition?.TryGetApplyCondition();
        CurrentState?.OnUpdate();
    }
}
