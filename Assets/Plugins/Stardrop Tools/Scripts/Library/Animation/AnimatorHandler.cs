
using UnityEngine;

namespace StardropTools
{
    /// <summary>
    /// Play, CrossFade or Trigger through Animator States on Layer Zero
    /// </summary>
    public class AnimatorHandler : MonoBehaviour
    {
        [Header("Simple Animation")]
        [SerializeField] protected Animator animator;
        [SerializeField] protected AnimationEventListener animEventDetector;
        [SerializeField] protected AnimState[] animStates;
        [SerializeField] int currentAnimID;
        [Min(0)] [SerializeField] protected float overralCrossFadeTime = 0;
        [Space]
        [SerializeField] bool debug;
        [Space]
        //[NaughtyAttributes.BoxGroup("Create States Transitions")]
        //[NaughtyAttributes.BoxGroup("Delete State Transisitions")]
        [NaughtyAttributes.Foldout("State Transition")]
        [SerializeField] bool createAnyStateTransitions;
        [NaughtyAttributes.Foldout("State Transition")]
        [SerializeField] bool clearAnyStateTransitions;
        [NaughtyAttributes.Foldout("State Transition")]
        [SerializeField] bool clearTriggerParameters;

        protected float animDuration;
        protected Timer animationLifetime;

        public int StateCount { get => animStates.Length; }
        public AnimState CurrentState { get => animStates[currentAnimID]; }
        public int CurrentAnimID { get => currentAnimID; }

        public readonly EventHandler OnAnimStart = new EventHandler();
        public readonly EventHandler OnAnimComplete = new EventHandler();

        public readonly EventHandler<int> OnAnimStartID = new EventHandler<int>();
        public readonly EventHandler<int> OnAnimCompleteID = new EventHandler<int>();

        public readonly EventHandler<AnimState> OnAnimStateStart = new EventHandler<AnimState>();
        public readonly EventHandler<AnimState> OnAnimStateComplete = new EventHandler<AnimState>();

        public EventHandler<int> OnAnimEventINT { get => animEventDetector.OnAnimEventINT; }
        public EventHandler<string> OnAnimEventSTRING { get => animEventDetector.OnAnimEventSTRING; }

        public void SyncAnimatorWithAnimationID(AnimatorHandlerTransitionType transitionType, bool disableOnFinish = false)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).shortNameHash != CurrentState.StateHash)
            {
                if (debug)
                    Debug.LogFormat("DIFFERENT Anim hash! - current: {0}, target: {1}", animator.GetCurrentAnimatorStateInfo(0).shortNameHash, CurrentState.StateHash);

                switch (transitionType)
                {
                    case AnimatorHandlerTransitionType.Play:
                        animator.Play(CurrentState.StateHash, CurrentState.Layer);
                        break;

                    case AnimatorHandlerTransitionType.CrossFade:
                        animator.CrossFade(CurrentState.StateHash, CurrentState.Layer);
                        break;

                    case AnimatorHandlerTransitionType.Trigger:
                        animator.SetTrigger(CurrentState.StateName);
                        break;
                }

                AnimationLifetime(CurrentState.LengthInSeconds, !disableOnFinish);
            }

            else
            {
                if (debug)
                    Debug.Log("SAME Anim hash!");
            }
        }

        /// <summary>
        /// Play target animation ID. Not Smooth!
        /// </summary>
        public void PlayAnimation(int animationID, bool disableOnFinish = false)
        {
            if (animationID == currentAnimID)
                return;

            if (animationID < 0 || animationID > animStates.Length)
            {
                Debug.LogFormat("Animation ID: {0}, does not exist", animationID);
                return;
            }


            var targetState = animStates[animationID];

            if (animator.enabled == false)
                animator.enabled = true;

            //animator.Play(targetState.StateName, targetState.Layer);
            animator.Play(targetState.StateHash, targetState.Layer);

            AnimationLifetime(targetState.LengthInSeconds, !disableOnFinish);
            currentAnimID = animationID;

            OnAnimStartID?.Invoke(currentAnimID);
            OnAnimStateStart?.Invoke(CurrentState);

            if (debug)
                Debug.Log(targetState.StateName);
        }

        /// <summary>
        /// Smoothly Crossfade from Current Animation, to Target Animation ID
        /// </summary>
        public void CrossFadeAnimation(int animationID, bool disableOnFinish = false)
        {
            if (animationID == currentAnimID)
                return;

            if (animationID < 0 || animationID > animStates.Length)
            {
                Debug.LogFormat("Animation ID: {0}, does not exist", animationID);
                return;
            }


            var targetState = animStates[animationID];

            if (animator.enabled == false)
                animator.enabled = true;

            //animator.CrossFade(targetState.StateName, targetState.crossfade);
            animator.CrossFade(targetState.StateHash, targetState.crossfade);

            AnimationLifetime(targetState.LengthInSeconds, !disableOnFinish);
            currentAnimID = animationID;

            OnAnimStart?.Invoke();
            OnAnimStartID?.Invoke(currentAnimID);
            OnAnimStateStart?.Invoke(CurrentState);

            if (debug)
                Debug.Log(targetState.StateName);
        }


        /// <summary>
        /// Set target animation id trigger parameter as true
        /// & smoothly crossfade to target animation
        /// </summary>
        public void TriggerAnimation(int animationID, float resetTriggerDelay = .01f, bool disableOnFinish = false)
        {
            if (animationID == currentAnimID)
                return;

            if (animationID < 0 || animationID > animStates.Length)
            {
                Debug.LogFormat("Animation ID: {0}, does not exist", animationID);
                return;
            }

            var targetState = animStates[animationID];

            // must be name instead of hash because parameter index does not always
            // match state index since we may want to make array changes in the inspector
            animator.SetTrigger(targetState.StateName);
            AnimationLifetime(targetState.LengthInSeconds, !disableOnFinish);

            currentAnimID = animationID;
            Invoke(nameof(ResetTrigger), resetTriggerDelay);

            if (debug)
                Debug.Log(targetState.StateName);
        }

        public void SetCurrentAnimationTime(float animTime)
        {
            AnimState targetState = animStates[currentAnimID];
            AnimatorStateInfo currentAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
            float targetTime = animTime * currentAnimatorStateInfo.length;
            animator.Play(targetState.StateHash, 0, targetTime);
        }

        public void ResetTrigger()
            => animator.ResetTrigger(currentAnimID);

        protected void AnimationLifetime(float time, bool disableOnFinish)
        {
            if (isActiveAndEnabled == false)
                return;

            animationLifetime = new Timer(time).Initialize();
            animationLifetime.OnTimerComplete.AddListener(() => AnimationLifetimeComplete(disableOnFinish));
        }

        protected void AnimationLifetimeComplete(bool disableOnFinish)
        {
            animator.enabled = disableOnFinish;

            OnAnimComplete?.Invoke();
            OnAnimCompleteID?.Invoke(currentAnimID);
            OnAnimStateComplete?.Invoke(CurrentState);
        }

        public void ChangeRuntimeAnimatorController(RuntimeAnimatorController runtimeAnimator)
        {
            animator.runtimeAnimatorController = runtimeAnimator;
        }

#if UNITY_EDITOR

        /// <summary>
        /// 
        /// 1) Get Animator Controller Reference
        /// 2) Get Animator Controller States
        /// 3) Get Animation Clips from Animator
        /// 4) Check if States.Length == AnimClips.Length
        /// 5) Loop through states
        /// 5.1) Create AnimState based on state & animClip info
        /// 
        /// </summary>
        [NaughtyAttributes.Button("Create Anim States")]
        protected void GenerateAnimStates()
        {
            if (animator == null)
            {
                Debug.Log("Animator not found!");
                return;
            }

            // 1 & 2) Get Animator Controller States
            // 3) Get Animation Clips from Animator
            UnityEditor.Animations.ChildAnimatorState[] controllerStates = AnimUtilities.GetAnimatorStates(animator, 0);
            AnimationClip[]                             animClips =        AnimUtilities.GetAnimationClips(animator);

            // 4) Check if States.Length == AnimClips.Length
            if (controllerStates.Length != animClips.Length)
            {
                Debug.Log("States.Lenth != Animation Clips.Length");
                return;
            }

            var animStateList = new System.Collections.Generic.List<AnimState>();

            // 5) Loop through states
            // 5.1) Create AnimState based on state & animClip info
            for (int i = 0; i < controllerStates.Length; i++)
            {
                UnityEditor.Animations.AnimatorState controllerState = controllerStates[i].state;
                AnimState newState = new AnimState(controllerState.name, controllerState.nameHash, 0, .15f, animClips[i].length);

                animStateList.Add(newState);
                Debug.Log("State: " + controllerStates[i].state.name);
            }

            animStates = animStateList.ToArray();
        }


        // Create Any State triggerable Transitions with
        // parameter conditions that have state names

        // 1) create trigger parameters based on Animator States
        // 2) create transitions with parameter names
        void CreateAnyStateTriggerTransition()
        {
            // 1) create trigger parameters based on Animator States
            AnimUtilities.CreateTriggerParametersBasedOnStates(animator, 0, true);

            // 2) create transitions with parameter names
            for (int i = 0; i < animStates.Length; i++)
            {
                string stateName = animStates[i].StateName;
                AnimUtilities.CreateAnyStateTriggerTransition(animator, 0, stateName, stateName);
            }
        }

        void ClearAnyStateTransitions()
            => AnimUtilities.ClearAnyStateTransitions(animator, 0);

        void ClearTriggerParameters()
        {
            var parameters = animator.parameters;
            var listParamToRemove = new System.Collections.Generic.List<AnimatorControllerParameter>();

            // 1) loop through all params
            // 2) check if param is a trigger && name is equal to a reference state
            // 3) remove if true
            for (int i = 0; i < parameters.Length; i++)
            {
                var p = parameters[i];

                if (p.type == AnimatorControllerParameterType.Trigger)
                    for (int j = 0; j < animStates.Length; j++)
                    {
                        if (p.name == animStates[j].StateName)
                        {
                            listParamToRemove.Add(p);
                            break;
                        }
                    }
            }

            AnimUtilities.RemoveAnimatorParameters(animator, listParamToRemove.ToArray());
        }

        protected virtual void OnValidate()
        {
            if (animator == null)
                animator = GetComponent<Animator>();

            if (createAnyStateTransitions)
            {
                CreateAnyStateTriggerTransition();
                createAnyStateTransitions = false;
            }

            if (clearAnyStateTransitions)
            {
                ClearAnyStateTransitions();
                clearAnyStateTransitions = false;
            }

            if (clearTriggerParameters)
            {
                ClearTriggerParameters();
                clearTriggerParameters = false;
            }

            if (overralCrossFadeTime > 0)
                foreach (AnimState animeState in animStates)
                    animeState.crossfade = overralCrossFadeTime;

            // Refresh state name with index at the start
            if (animStates.Exists())
                for (int i = 0; i < animStates.Length; i++)
                    animStates[i].SetIndexStateName(i);
        }
#endif
        [NaughtyAttributes.Button("Get Animator")]
        void GetAnimator()
        {
            if (animator == null)
            {
                animator = GetComponent<Animator>();
                if (animator == null)
                    animator = GetComponentInChildren<Animator>();
            }

            animEventDetector = animator.GetComponent<AnimationEventListener>();
        }
    
    }
}

