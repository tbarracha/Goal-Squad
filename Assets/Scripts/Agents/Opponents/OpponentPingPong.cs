
using UnityEngine;

public class OpponentPingPong : Opponent
{
    [SerializeField] LineRenderer runLine;
    [SerializeField] Vector3[] wayPoints;
    Vector3 targetWaypoint;
    int currentIndex = 0;

    public override void Initialize()
    {
        base.Initialize();
        Player.OnPlayerReachGoal.AddListener(() => ChangeState(AgentState.Idle));
    }

    public override void OnSpawn()
    {
        base.OnSpawn();

        wayPoints = new Vector3[2];
        wayPoints[0] = UtilsVector.SetVectorX(Position, Mathf.Clamp(settings.GetRandomPingPoingWaypointX, -5, 5));
        wayPoints[1] = UtilsVector.SetVectorX(Position, Mathf.Clamp(settings.GetRandomPingPoingWaypointX, -5, 5));

        float wpDist = Vector3.Distance(wayPoints[0], wayPoints[1]);
        while (wpDist < settings.MinWaypointDistance)
        {
            wayPoints[1] = UtilsVector.SetVectorX(Position, Mathf.Clamp(settings.GetRandomPingPoingWaypointX, -5, 5));
            wpDist = Vector3.Distance(wayPoints[0], wayPoints[1]);
        }

        Utilities.SetLinePointsWithHeightOffset(runLine, wayPoints, 0.01f);

        spawnedPosition = wayPoints.GetRandom();
        SetPosition(spawnedPosition);
    }

    public override void StartUpdate()
    {
        base.StartUpdate();
        ChangeState(AgentState.Running);
    }


    protected override void ToRunningState()
    {
        base.ToRunningState();

        currentIndex = Random.Range(0, wayPoints.Length);
        targetWaypoint = wayPoints[currentIndex];
        Accelerate();
    }

    protected override void UpdateRunningState()
    {
        UtilsVector.SmoothLookAt(graphics, DirectionTo(targetWaypoint), lookSpeed);
        selfTransform.position = Vector3.MoveTowards(Position, targetWaypoint, speed * Time.deltaTime);
        detectDistance = Vector3.Distance(targetWaypoint, Position);

        if (detectDistance <= .1f)
        {
            int curr = currentIndex;
            currentIndex = wayPoints.GetNextIndex(curr);
            targetWaypoint = wayPoints[currentIndex];
        }
    }
}