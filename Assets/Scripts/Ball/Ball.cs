
using StardropTools;
using StardropTools.Tween;
using System.Collections;
using UnityEngine;

public class Ball : BaseObjectManagerSingleton<Ball>
{
    [NaughtyAttributes.Expandable][SerializeField] BallSettingsSO settings;
    [SerializeField] BallState ballState;
    [SerializeField] VelocityCalculator velocityCalculator;
    [SerializeField] new Rigidbody rigidbody;
    [SerializeField] Transform graphic;
    [SerializeField] Transform shadow;

    [Header("Tweens")]
    [SerializeField] TweenPositionComponent tweenComponentPass;

    LevelState currentLevelState = LevelState.Idle;
    Coroutine kickPathCR;
    Vector3 shadowPosition, startPosition;

    Vector3 Velocity => velocityCalculator.Velocity;
    public BallState CurrentBallState => ballState;


    public static readonly EventHandler OnBallGoalReached       = new EventHandler();
    public static readonly EventHandler OnBallStollen           = new EventHandler();
    public static readonly EventHandler<Vector3> OnBallTouched  = new EventHandler<Vector3>();


    public override void Initialize()
    {
        base.Initialize();
        tweenComponentPass.duration = settings.PassDuration;
        velocityCalculator.SetTarget(selfTransform);
    }

    protected override void EventFlow()
    {
        base.EventFlow();

        GameManager.OnPlayStart.AddListener(StartUpdate);
        //GameManager.OnPlayEnd.AddListener(StopUpdate);

        LevelManager.OnPlayerPositionsGenerated.AddListener(LevelPositionsGenerated);
        LevelManager.OnChangedLevelState.AddListener(OnLevelStateChanged);

        Player.OnTeamMemberSelected.AddListener(PassTo);
        Player.OnKickPathSelected.AddListener(KickTo);

        GameManager.OnRestart.AddListener(ResetComponent);

        OnBallTouched.AddListener(StealBall);
    }

    protected override void ResetComponent()
    {
        CancelInvoke();
        StopAllCoroutines();

        ChangeBallState(BallState.Idle);
        rigidbody.isKinematic = true;
        SetPosition(startPosition);
        ResetRotation();
        graphic.localRotation = Quaternion.identity;
    }

    void ChangeBallState(BallState targetState)
    {
        if (targetState == ballState) return;
        ballState = targetState;
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();
        velocityCalculator.CalculateVelocity();

        //if (currentLevelState != LevelState.Idle || currentLevelState != LevelState.Finish)
            graphic.Rotate(graphic.right, settings.RotationSpeed * Velocity.magnitude * Time.deltaTime);

        shadowPosition = graphic.position;
        shadowPosition.y = 0.01f;
        shadow.position = shadowPosition;
        shadow.rotation = Quaternion.Euler(90, 0, 0);
    }

    void LevelPositionsGenerated(Vector3 startPos, Vector3 endPos, Vector3 goalPos)
    {
        ChangeBallState(BallState.Idle);
        rigidbody.isKinematic = true;
        SetPosition(startPos);
        ResetRotation();
        graphic.localRotation = Quaternion.identity;

        startPosition = startPos;
    }

    void OnLevelStateChanged(LevelState newState)
    {
        currentLevelState = newState;
    }

    void PassTo(TeamMember member)
    {
        if (currentLevelState != LevelState.Move)
            return;

        AudioManager.Instance.PlayPass();
        ClearParent();

        tweenComponentPass.startPos = Position;
        tweenComponentPass.endPos = member.BallPointPass.position;

        ChangeBallState(BallState.Passing);
        var tween = tweenComponentPass.StartTween();
        tween.OnTweenComplete.AddListener(() => PassComplete(member));
    }

    void PassComplete(TeamMember member)
    {
        SetParent(member.Transform);
        ChangeBallState(BallState.Rotating);
    }


    void KickTo(Vector3[] path)
    {
        if (kickPathCR != null)
            StopCoroutine(kickPathCR);

        AudioManager.Instance.PlayPass();
        ChangeBallState(BallState.Kicking);
        kickPathCR = StartCoroutine(KickPathCR(path));
    }

    void StealBall(Vector3 stealerPosition)
    {
        Vector3 dir = DirectionFrom(stealerPosition);
        AddForce(dir);
        ChangeBallState(BallState.Stolen);
        OnBallStollen?.Invoke();
    }

    IEnumerator KickPathCR(Vector3[] path)
    {
        for (int i = 0; i < path.Length; i++)
        {
            while (i < path.Length && Vector3.Distance(Position, path[i]) > .1f)
            {
                Position = Vector3.MoveTowards(Position, path[i], settings.KickSpeed * Time.deltaTime);
                LookAt(DirectionTo(path[i]));
                yield return null;
            }
        }

        AddForce(Velocity * settings.KickForce);
        OnBallGoalReached?.Invoke();
    }

    void AddForce(Vector3 forceDirection)
    {
        rigidbody.isKinematic = false;
        rigidbody.AddForce(forceDirection, ForceMode.Impulse);
    }
}