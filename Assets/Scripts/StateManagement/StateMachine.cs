using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class StateMachine
    {
        object currentStateKey;
        IState currentState = new EmptyState();
        Dictionary<object, IState> states = new Dictionary<object, IState>();

        public object GetCurrentKey => currentStateKey;
        public IState _currentState => currentState;
        public Dictionary<object, IState> _states => states;
        public int _stateCount => states.Count;

        public IState GetState(object id)
        {
            if (states.ContainsKey(id))
            {
                return states[id];
            }
            
            return null;
        }

        public void Update()
        {
            if (currentState != null)
            {
                currentState.ExecuteFrame();
            }
        }

        public void UpdateFixed()
        {
            if (currentState != null)
            {
                currentState.ExecuteFrameFixed();
            }
        }

        public void UpdateLate()
        {
            if (currentState != null)
            {
                currentState.ExecuteFrameLate();
            }
        }

        public void AddState(object id, IState state)
        {
            if (!states.ContainsKey(id))
            {
                states.Add(id, state);
            }
        }

        public void ChangeState(object id, params object[] args)
        {
            if (currentState != null)
            {
                currentState.Exit();

                if (states.ContainsKey(id))
                {
                    currentStateKey = id;
                    currentState = states[id];
                    currentState.Enter(args);
                }
            }
            else Debug.LogError("State machine does not contain the state " + id);
        }

        public void ClearStates()
        {
            states.Clear();
        }

        public void RemoveState(object id)
        {
            if (states.ContainsKey(id))
            {
                states.Remove(id);
            }
        }
    }
}