using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class StateMachine
    {
        public StateID CurrentStateKey { get; private set; }
        public IState CurrentState { get; private set; } = new EmptyState();
        public Dictionary<StateID, IState> States { get; private set; } = 
            new Dictionary<StateID, IState>();

        public IState GetState(StateID id)
        {
            if (States.ContainsKey(id))
            {
                return States[id];
            }
            
            return null;
        }

        public bool InState(params StateID[] ids)
        {
            foreach (var id in ids)
                if (CurrentStateKey == id)
                    return true;

            return false;
        }

        public void Update()
        {
            CurrentState?.ExecuteFrame();
        }

        public void UpdateFixed()
        {
            CurrentState?.ExecuteFrameFixed();
        }

        public void UpdateLate()
        {
            CurrentState?.ExecuteFrameLate();
        }

        public void AddState(StateID id, IState state)
        {
            if (!States.ContainsKey(id))
            {
                States.Add(id, state);
            }
        }

        public void ChangeState(StateID id, params object[] args)
        {
            if (CurrentState != null)
            {
                CurrentState.Exit();

                if (States.ContainsKey(id))
                {
                    CurrentStateKey = id;
                    CurrentState = States[id];
                    CurrentState.Enter(args);
                }
                else Debug.LogError("State machine does not contain the state " + id);
            }
        }

        public void ClearStates()
        {
            States.Clear();
        }

        public void RemoveState(StateID id)
        {
            if (States.ContainsKey(id))
            {
                States.Remove(id);
            }
        }
    }
}