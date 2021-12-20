using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private State _startState;

    private State _currentState;

    private void Start()
    {
        Reset();
    }

    private void Update()
    {
        if (_currentState == null)
        {
            return;
        }

        var nexState = _currentState.GetNextState();
        if (nexState != null)
        {
            Transit(nexState);
        }
    }

    private void Reset()
    {
        _currentState = _startState;
        if (_currentState != null)
        {
            _currentState.Enter();
        }
    }

    private void Transit(State nextState)
    {
        if (_currentState != null)
        {
            _currentState.Exit();
        }

        _currentState = nextState;
        if (_currentState != null)
        {
            _currentState.Enter();
        }
    }
}