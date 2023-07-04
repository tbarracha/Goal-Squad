
using UnityEngine;

public class OpponentFollower : Opponent
{
    Vector3 lookDirection = Vector3.zero;

    public override void Initialize()
    {
        base.Initialize();
        OnDeaccelComplete.AddListener(SetIdle);
    }

    public override void StartUpdate()
    {
        base.StartUpdate();
        ChangeState(AgentState.Alert);
    }


    protected override void ToAlertState() => PlayRandomIdle();

    protected override void ToRunningState()
    {
        base.ToRunningState();

        Accelerate();
        PlayAlert();

        Invoke(nameof(Deaccelerate), settings.ActionTime);
    }


    protected override void UpdateAlertState()
    {
        CalculatePlayerDistance();

        if (detectDistance < ballDetectDistance)
            ChangeState(AgentState.Running);
    }

    protected override void UpdateRunningState()
    {
        lookDirection = DirectionTo(Player.Instance.SelectedMember.Position);
        UtilsVector.SmoothLookAt(graphics, lookDirection, lookSpeed);
        selfTransform.Translate(graphics.forward * speed * Time.deltaTime);
    }
}