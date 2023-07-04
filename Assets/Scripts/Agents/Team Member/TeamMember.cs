using System;
using System.Collections;
using UnityEngine;

public class TeamMember : Agent
{
    [SerializeField] TeamMateSelector selector;
    [SerializeField] Transform ballPointPass;
    [SerializeField] Transform ballPointHold;

    public Transform BallPointPass => ballPointPass;
    public Transform BallPointHold => ballPointHold;


    internal void SetColliderRadius(float colliderRadius) => selector.SetRadius(colliderRadius);

    internal void ChangeMemberState(AgentState targetState) => ChangeState(targetState);

    protected override void ChangeState(AgentState targetState)
    {
        base.ChangeState(targetState);

        if (targetState == AgentState.Idle)
            PlayRandomIdle();

        else if (targetState == AgentState.Running)
            PlayRun();

        else if (targetState == AgentState.Kicking)
        {
            PlayRandomKick();
            Invoke(nameof(AfterKick), UnityEngine.Random.Range(2.25f, 2.5f));
        }

        else if (targetState == AgentState.Victory)
            PlayRandomVictory();

        else if (targetState == AgentState.Defeat)
            PlayRandomDefeat();
    }

    void AfterKick()
    {
        if (levelState == LevelState.Idle)
            SetIdle();

        else if (levelState == LevelState.Finish)
        {
            if ((GameManager.Instance as GameManager).HasWon == true)
                ChangeState(AgentState.Victory);
            else
                ChangeState(AgentState.Defeat);
        }
    }
}