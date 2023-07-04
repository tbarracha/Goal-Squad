#if UNITY_EDITOR

using UnityEngine;
using UnityEditor.Animations;

public static class AnimUtilities
{
    public static AnimatorController GetAnimatorController(Animator animator)
        => animator.runtimeAnimatorController as AnimatorController;


    public static AnimatorStateMachine GetAnimatorStateMachine(Animator animator, int layer)
        => GetAnimatorController(animator).layers[layer].stateMachine;
    public static AnimatorStateMachine GetAnimatorStateMachine(AnimatorController controller, int layer)
        => controller.layers[layer].stateMachine;


    public static ChildAnimatorState[] GetAnimatorStates(Animator animator, int layer)
        => GetAnimatorStates(GetAnimatorController(animator), layer);

    public static ChildAnimatorState[] GetAnimatorStates(AnimatorController controller, int layer)
    {
        // Get controller states
        AnimatorStateMachine animatorStateMachine = controller.layers[layer].stateMachine;
        ChildAnimatorState[] states = animatorStateMachine.states;

        return states;
    }


    public static AnimationClip[] GetAnimationClips(Animator animator)
            => animator.runtimeAnimatorController.animationClips;


    public static AnimatorControllerParameter[] GetAnimatorParametersOfType(Animator animator, AnimatorControllerParameterType parameterType)
    {
        var allParams = animator.parameters;
        System.Collections.Generic.List<AnimatorControllerParameter> paramList = new System.Collections.Generic.List<AnimatorControllerParameter>();

        for (int i = 0; i < allParams.Length; i++)
            if (allParams[i].type == parameterType)
                paramList.Add(allParams[i]);

        return paramList.ToArray();
    }


    public static AnimatorControllerParameter[] CreateTriggerParametersBasedOnStates(Animator animator, int layer, bool clearExistingParams = false)
        => CreateTriggerParametersBasedOnStates(GetAnimatorController(animator), layer, clearExistingParams);
    public static AnimatorControllerParameter[] CreateTriggerParametersBasedOnStates(AnimatorController controller, int layer, bool clearExistingParams = false)
    {
        var states = GetAnimatorStates(controller, layer);
        if (states.Length == 0)
        {
            Debug.Log("Controller has no states in layer: " + layer);
            return null;
        }

        if (clearExistingParams)
            controller.parameters = null;

        string paramName = "";
        for (int i = 0; i < states.Length; i++)
        {
            paramName = states[i].state.name;

            if (CheckIfParamExists(controller, paramName))
                continue;
            
            controller.AddParameter(paramName, AnimatorControllerParameterType.Trigger);
        }

        return controller.parameters;
    }


    public static bool CheckIfParamExists(Animator animator, string name)
    {
        bool value = false;

        for (int i = 0; i < animator.parameters.Length; i++)
            if (animator.GetParameter(i).name == name)
            {
                value = true;
                break;
            }

        return value;
    }

    public static bool CheckIfParamExists(AnimatorController controller, string name)
    {
        bool value = false;

        for (int i = 0; i < controller.parameters.Length; i++)
            if (controller.parameters[i].name == name)
            {
                value = true;
                break;
            }

        return value;
    }


    public static void RemoveAnimatorParameters(Animator animator, AnimatorControllerParameter[] parameters)
        => RemoveAnimatorParameters(GetAnimatorController(animator), parameters);
    public static void RemoveAnimatorParameters(AnimatorController controller, AnimatorControllerParameter[] parameters)
    {
        for (int i = 0; i < parameters.Length; i++)
            controller.RemoveParameter(parameters[i]);
    }


    public static void CreateAnyStateTriggerTransition(Animator animator, int layer, string stateName, string transitionParameterName)
        => CreateAnyStateTriggerTransition(GetAnimatorController(animator), layer, stateName, transitionParameterName);

    public static AnimatorStateTransition CreateAnyStateTriggerTransition(AnimatorController controller, int layer, string stateName, string transitionParameterName)
    {
        var fsm = GetAnimatorStateMachine(controller, layer);
        var states = GetAnimatorStates(controller, layer);
        AnimatorState state = null;

        // find state to transition to
        for (int i = 0; i < states.Length; i++)
            if (states[i].state.name == stateName)
            {
                state = states[i].state;
                break;
            }

        if (CheckIfParamExists(controller, transitionParameterName) == false)
            controller.AddParameter(transitionParameterName, AnimatorControllerParameterType.Trigger);

        AnimatorStateTransition transition = fsm.AddAnyStateTransition(state);
        transition.AddCondition(AnimatorConditionMode.If, 1, transitionParameterName);

        return transition;
    }


    public static void ClearAnyStateTransitions(Animator animator, int layer)
        => ClearAnyStateTransitions(GetAnimatorController(animator), layer);
    public static void ClearAnyStateTransitions(AnimatorController controller, int layer)
    {
        var fsm = GetAnimatorStateMachine(controller, layer);
        var transitions = fsm.anyStateTransitions;

        for (int i = 0; i < transitions.Length; i++)
            fsm.RemoveAnyStateTransition(transitions[i]);
    }
}
#endif