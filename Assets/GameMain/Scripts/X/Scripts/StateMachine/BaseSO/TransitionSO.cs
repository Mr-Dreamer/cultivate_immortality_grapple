using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Transition", menuName = "StateMachine/Transition/New Transition")]
public class TransitionSO : ScriptableObject
{
    private Dictionary<StateActionSO, List<ConditionSO>> _transition = new Dictionary<StateActionSO, List<ConditionSO>>();

    [SerializeField] private List<TransitionState> currentTransition = new List<TransitionState>();

    private StateMachineSystem stateMachineSystem;

    public void Init(StateMachineSystem stateMachine) 
    {      
        stateMachineSystem = stateMachine;
        AddTransition(stateMachine);
        
    }

    


    public void TryGetEnableCondition() 
    {
        if (_transition.Count != 0) 
        {
            if (_transition.ContainsKey(stateMachineSystem.currentState))
            {
                foreach (var item in _transition[stateMachineSystem.currentState])
                {
                    if (item.ConditionSetUp())
                    {
                        Transition(item);
                    }
                }
            }
        }
    }

    public void Transition(ConditionSO condition) 
    {
        stateMachineSystem.currentState?.OnExit();
        stateMachineSystem.currentState = GetNextState(condition);
        stateMachineSystem.currentState?.OnEnter(this.stateMachineSystem);

    }

    public StateActionSO GetNextState(ConditionSO condition) 
    {
        if (currentTransition.Count != 0) 
        {
            foreach (var item in currentTransition)
            {
                //if (item.condition == condition && stateMachineSystem.currentState == item.fromState)
                //{
                //    return item.toState;
                //}
                if (stateMachineSystem.currentState == item.fromState && item.condition.Contains(condition))
                {
                    return item.toState;
                }
            }           
        }
        return null;
    }

    public void AddTransition(StateMachineSystem stateMachine) 
    {
        if (currentTransition.Count != 0) 
        {
            foreach (var item in currentTransition)
            {
                if (!_transition.ContainsKey(item.fromState))
                {
                    _transition.Add(item.fromState, new List<ConditionSO>());
                    //_transition[item.fromState].Add(item.condition);
                    foreach (var conditions in item.condition)
                    {
                        conditions.Init(stateMachine);
                        _transition[item.fromState].Add(conditions);
                    }
                }
                else
                {
                    //if (!_transition[item.fromState].Contains(item.condition))
                    //{
                    //    _transition[item.fromState].Add(item.condition);
                    //}
                    foreach (var newCondition in item.condition)
                    {
                        if (!_transition[item.fromState].Contains(newCondition))
                        {
                            newCondition.Init(stateMachine);
                            _transition[item.fromState].Add(newCondition);

                        }
                        else 
                        {
                            continue;
                        }
                    }
                }
            }
        }
    }

    [System.Serializable]
    private class TransitionState
    {
        public StateActionSO fromState;

        public StateActionSO toState;

        public List<ConditionSO> condition;


        public TransitionState(StateActionSO fromState, StateActionSO toState, List<ConditionSO> condition)
        {
            this.fromState = fromState;
            this.toState = toState;
            this.condition = condition;
            
        }      

    }
}



