
using UnityEngine;

public class OpponentDefender : Opponent
{
    Vector3 lookDirection = Vector3.back;
    float spawnedZ = 0f;

    public override void StartUpdate()
    {
        base.StartUpdate();
        ChangeState(AgentState.Alert);
    }

    public override void OnSpawn()
    {
        base.OnSpawn();

        spawnedZ = PosZ;
    }

    protected override void ToIdleState() => PlayDefend();


    protected override void ToRunningState()
    {
        base.ToRunningState();
        PlayAlert();
        Accelerate();
    }

    protected override void ToDefendingState() => PlayDefend();

    protected override void UpdateAlertState()
    {
        CalculatePlayerDistance();

        if (detectDistance < ballDetectDistance)
        {
            Invoke(nameof(SetIdle), settings.ActionTime);
            ChangeState(AgentState.Running);
        }
    }

    protected override void UpdateRunningState()
    {
        Vector3 ballPos = Ball.Instance.Position;
        if (ballPos.x > PosX)
            lookDirection = Vector3.right;

        else if (ballPos.x < PosX)
            lookDirection = Vector3.left;

        else if (ballPos.x == PosX)
            ChangeState(AgentState.Defending);

        Position = Vector3.MoveTowards(Position, UtilsVector.SetVectorZ(ballPos, spawnedZ), speed * Time.deltaTime);
        UtilsVector.SmoothLookAt(graphics, lookDirection, lookSpeed);
    }

    protected override void UpdateDefendingState()
    {
        Vector3 ballPos = Ball.Instance.Position;
        if (ballPos.x != PosX)
            ChangeState(AgentState.Running);

        UtilsVector.SmoothLookAt(graphics, Vector3.back, lookSpeed);
    }
}