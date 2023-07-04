
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.FiniteStateMachine
{
    /// <summary>
    /// Class responsible for managing states
    /// </summary>
    public class FiniteStateMachine : BaseComponent
    {
#if UNITY_EDITOR
        [Header("State Maker")]
        [SerializeField] protected string[] stateNames;
        [SerializeField] protected bool createStates;
        [Tooltip("Log State events to the Unity Console so we can just Copy/Paste to any script")]
        [SerializeField] protected bool logStateEventsToConsole;
#endif
        
        [Header("State Machine")]
        [SerializeField] protected int startIndex = 0;
        [SerializeField] protected BaseState currentState;
        [SerializeField] protected BaseState previousState;
        [SerializeField] protected float timeInCurrentState;
        [Space]
        [SerializeField] protected List<BaseState> states;
        [SerializeField] protected bool debug;

        public int CurrentStateID => currentState.GetStateID();
        public BaseState GetState(int stateID) => states[stateID];
        public int StateCount => states.Count;


        public override void Initialize()
        {
            base.Initialize();

            currentState = new NullState();
            previousState = new NullState();

            for (int i = 0; i < states.Count; i++)
                states[i].Initialize(this, i);

            ChangeState(startIndex);
        }


        public override void HandleUpdate()
        {
            base.HandleUpdate();
            UpdateStateMachine();
        }


        public void UpdateStateMachine()
        {
            currentState.UpdateState();
            currentState.HandleInput();
        }


        public void ChangeState(BaseStateComponent nextState)
            => ChangeState(nextState.StateID);

        public void ChangeState(int nextStateID)
        {
            if (currentState != null && nextStateID == currentState.GetStateID())
            {
                if (debug)
                    Debug.Log("State is already: " + currentState.GetStateID());

                return;
            }

            if (currentState != null)
            {
                currentState.ExitState();
                previousState = currentState;
            }

            currentState = states[nextStateID];
            currentState.EnterState();

            if (debug && previousState != null)
            {
                Debug.LogFormat("Changed stated from {0}, to {1}", previousState.GetStateID(), currentState.GetStateID());
                Debug.LogFormat("Changed stated from {0}, to {1}", previousState.StateName, currentState.StateName);
            }
        }

        public void SetStateIDs()
        {
            for (int i = 0; i < states.Count; i++)
                states[i].SetID(i);
        }


        /// <summary>
        /// State will be ID'ed based on entry order
        /// </summary>
        public void AddState(BaseState newState)
        {
            if (states == null)
                states = new List<BaseState>();

            if (states.Contains(newState) == false)
            {
                states.Add(newState);
                newState.SetID(states.Count - 1);
            }
        }

        /// <summary>
        /// States will be ID'ed based on entry order
        /// </summary>
        public void AddStates(BaseState[] newStates)
        {
            for (int i = 0; i < newStates.Length; i++)
                AddState(newStates[i]);
        }


        public void RemoveState(BaseState state)
        {
            if (states.Contains(state) == true)
                states.Remove(state);

            SetStateIDs();
        }

        public void RemoveState(int stateID)
        {
            if (states[stateID] != null)
                RemoveState(states[stateID]);

            SetStateIDs();
        }

        public void ClearStates()
        {
            states = new List<BaseState>();
            currentState = null;
            previousState = null;
        }


#if UNITY_EDITOR
        public void CreateStates()
        {
            // No need to create states if we have created them in the inspector
            if (Application.isPlaying && stateNames.Length == StateCount)
                return;

            List<BaseState> baseStates = new List<BaseState>();
            for (int i = 0; i < stateNames.Length; i++)
                baseStates.Add(new BaseState(stateNames[i]));

            AddStates(baseStates.ToArray());
        }


        /// <summary>
        /// Log State events to the Unity Console so we can just Copy/Paste to any script. We can Immediately Paste since it goes to clipboard
        /// </summary>
        public void LogStateEvents()
        {
            string start = "public BaseEvent "; // OnIdle;

            string name = "";                   // public BaseEvent Idle
            string on = "On";                   // public BaseEvent OnIdle
            string enter = "Enter";             // public BaseEvent OnIdleEnter
            string update = "Update";           // public BaseEvent OnIdleUpdate
            string exit = "Exit";               // public BaseEvent OnIdleExit

            string openGetState = " => stateMachine.GetState(";
            string stateIndex = "";
            string closeGetState = ").";

            string mainLog = "";

            for (int i = 0; i < stateNames.Length; i++)
            {
                name = stateNames[i];
                stateIndex = i.ToString();

                string logEnter = start + on + name + enter + openGetState + stateIndex + closeGetState + on + enter + ";";
                string logUpdate = start + on + name + update + openGetState + stateIndex + closeGetState + on + update + ";";
                string logExit = start + on + name + exit + openGetState + stateIndex + closeGetState + on + exit + ";";

                string log = logEnter + "\n" + logUpdate + "\n" + logExit + "\n";
                mainLog += log + "\n";
            }

            Utilities.CopyStringToClipboard(mainLog);
            Debug.Log(mainLog);
        }

        private void OnValidate()
        {
            if (createStates)
            {
                CreateStates();
                createStates = false;
            }

            if (logStateEventsToConsole)
            {
                LogStateEvents();
                logStateEventsToConsole = false;
            }
        }
#endif
    }
}