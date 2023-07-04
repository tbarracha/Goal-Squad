
using UnityEngine;

/// <summary>
/// Opponent that dashes in a single line when ball is at a close enough distance
/// </summary>
public class OpponentLineRunner : Opponent
{
    Vector3 targetPosition, runDirection;

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

        Vector3 selectedPlayerPos = Player.Instance.SelectedMember.Position;

        if (selectedPlayerPos.x == 0)
            targetPosition = selectedPlayerPos + Vector3.forward * settings.MidPrediction;

        else
        {
            float distance = DistanceTo(selectedPlayerPos);
            
            if (distance <= settings.SidePredictionDistance)
                targetPosition = selectedPlayerPos + Vector3.forward * settings.SidePrediction;
            else
                targetPosition = selectedPlayerPos + Vector3.forward * settings.FarsidePrediction;
        }

        runDirection = DirectionTo(targetPosition).normalized;
    }


    protected override void UpdateAlertState()
    {
        CalculatePlayerDistance();

        if (detectDistance < ballDetectDistance)
            ChangeState(AgentState.Running);
    }

    protected override void UpdateRunningState()
    {
        UtilsVector.SmoothLookAt(graphics, runDirection, lookSpeed);
        selfTransform.Translate(runDirection * speed * Time.deltaTime);

        if (timeInstate > settings.ActionTime || Mathf.Abs(PosX) >= maxSideEdgeX)
            ChangeState(AgentState.Idle);
    }
}