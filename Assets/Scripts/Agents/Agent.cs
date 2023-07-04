using StardropTools;
using System.Collections;
using UnityEngine;

public class Agent : AnimatedObject
{
    [NaughtyAttributes.Expandable][SerializeField] protected AgentJumpSettingsSO jumpSettings;
    [SerializeField] protected Transform graphics;
    [Space]
    [SerializeField] protected AgentState state;

    protected LevelState levelState;
    protected float timeInstate;
    protected Coroutine jumpCR;

    public override void Initialize()
    {
        base.Initialize();
        LevelManager.OnChangedLevelState.AddListener(LevelStateChanged);
    }

    protected void LevelStateChanged(LevelState state) => levelState = state;



    public override void HandleUpdate()
    {
        base.HandleUpdate();
        HandleState();
    }

    public void SetIdle() => ChangeState(AgentState.Idle);

    protected virtual void ChangeState(AgentState targetState)
    {
        if (targetState == state)
            return;

        state = targetState;
        timeInstate = 0;

        switch (targetState)
        {
            case AgentState.Idle:
                ToIdleState();
                break;

            case AgentState.Alert:
                ToAlertState();
                break;

            case AgentState.Running:
                ToRunningState();
                break;

            case AgentState.Kicking:
                ToKickingState();
                break;

            case AgentState.Defending:
                ToDefeatState();
                break;

            case AgentState.Victory:
                ToVictoryState();
                break;

            case AgentState.Defeat:
                ToDefeatState();
                break;
        }
    }

    protected virtual void HandleState()
    {
        timeInstate += Time.deltaTime;

        switch (state)
        {
            case AgentState.Idle:
                UpdateIdleState();
                break;

            case AgentState.Alert:
                UpdateAlertState();
                break;

            case AgentState.Running:
                UpdateRunningState();
                break;

            case AgentState.Kicking:
                UpdateKickingState();
                break;

            case AgentState.Defending:
                UpdateDefendingState();
                break;

            case AgentState.Victory:
                UpdateVictoryState();
                break;

            case AgentState.Defeat:
                UpdateDefeatState();
                break;
        }
    }

    #region To Specific State
    protected virtual void ToIdleState() => PlayRandomIdle();

    protected virtual void ToAlertState() { }

    protected virtual void ToRunningState() => PlayRun();

    protected virtual void ToKickingState() => PlayRandomKick();

    protected virtual void ToDefendingState() => PlayDefend();

    protected virtual void ToVictoryState() => PlayRandomVictory();

    protected virtual void ToDefeatState() => PlayRandomDefeat();

    #endregion


    #region Update Specific State

    protected virtual void UpdateIdleState() { }

    protected virtual void UpdateAlertState() { }

    protected virtual void UpdateRunningState() { }

    protected virtual void UpdateKickingState() { }
                           
    protected virtual void UpdateDefendingState() { }
                           
    protected virtual void UpdateVictoryState() { }

    protected virtual void UpdateDefeatState() { }

    #endregion



    public void SetGraphicRotation(Quaternion rotation) => graphics.rotation = rotation;
    public void SetGraphicRotation(Vector3 eulerAngles) => graphics.eulerAngles = eulerAngles;


    protected void PlayRandomIdle() => animator.CrossFadeAnimation(Random.Range(0, 3));

    protected void PlayRun()
    {
        animator.CrossFadeAnimation(3);
        animator.SetCurrentAnimationTime(Random.Range(0f, 1f));
    }

    protected void PlayRandomKick() => animator.CrossFadeAnimation(Random.Range(6, 8));

    protected void PlayRandomVictory() => animator.CrossFadeAnimation(Random.Range(8, 14));

    protected void PlayRandomDefeat() => animator.CrossFadeAnimation(Random.Range(14, 17));

    protected void PlayDefend() => animator.CrossFadeAnimation(17);


    protected void Jump()
    {
        if (jumpCR != null)
            StopCoroutine(jumpCR);

        jumpCR = StartCoroutine(JumpCR());
    }

    IEnumerator JumpCR()
    {
        float t = 0;

        Vector3 startPos = Position;
        Vector3 targetPos = startPos + Vector3.up * jumpSettings.RandomJumpHeight;

        while (t < jumpSettings.JumpDuration)
        {
            SetPosition(Vector3.Lerp(startPos, targetPos, jumpSettings.JumpCurve.Evaluate(t / jumpSettings.JumpDuration)));

            t += Time.deltaTime;
            yield return null;
        }

        SetPosition(startPos);
    }
}