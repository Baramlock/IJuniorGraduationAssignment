using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    [SerializeField] private List<Transition> _transitions;

    public void Enter()
    {
        if (enabled == false)
        {
            enabled = true;
            foreach (var transition in _transitions)
            {
                transition.enabled = true;
            }
        }
    }

    public void Exit()
    {
        foreach (var transition in _transitions)
        {
            transition.enabled = false;
        }

        enabled = false;
    }

    public State GetNextState()
    {
        return _transitions.FirstOrDefault(x => x.NeedTransit)?.TargetState;
    }
}