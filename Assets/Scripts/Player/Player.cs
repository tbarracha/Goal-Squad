
using StardropTools;
using StardropTools.Tween;
using System.Collections;
using UnityEngine;

public class Player : BaseObjectManagerSingleton<Player>
{
    [NaughtyAttributes.Expandable][SerializeField] PlayerSettingsSO settings;
    [SerializeField] TeamMember[] members;
    [SerializeField] TeamMember selectedMember;
    [SerializeField] TeamMateIndicator indicator;

    [Header("Movement")]
    [SerializeField] float speed = 0;
    Vector3 startPosition, endPosition, goalPosition;

    [Header("Kick")]
    [SerializeField] LineRenderer kickLine;
    [SerializeField] Vector3[] kickPoints;
    Vector3[] smoothKickPoints;
    Vector3 midKickPoint;
    bool hasKicked;

#if UNITY_EDITOR
    [Header("Debug")]
    [SerializeField] float memberDistance = 3;
    [SerializeField] float colliderRadius = 3;
#endif

    LevelState currentLevelState = LevelState.Idle;
    Coroutine accelCR, deaccelCR;
    float startDistance;

    public TeamMember SelectedMember => selectedMember;
    public float Speed => speed;


    public static readonly EventHandler             OnPlayerReachGoal       = new EventHandler();
    public static readonly EventHandler<TeamMember> OnTeamMemberSelected    = new EventHandler<TeamMember>();
    public static readonly EventHandler<Vector3[]>  OnKickPathSelected      = new EventHandler<Vector3[]>();
    public static readonly EventHandler<float>      OnPlayerMoveProgress    = new EventHandler<float>();

    
    protected override void EventFlow()
    {
        base.EventFlow();

        GameManager.OnPlayStart.AddListener(StartUpdate);
        GameManager.OnPlayEnd.AddListener(StopUpdate);
        GameManager.OnRestart.AddListener(ResetComponent);

        LevelManager.OnChangedLevelState.AddListener(OnLevelStateChanged);
        LevelManager.OnPlayerPositionsGenerated.AddListener(PlayerPositionsGenerated);
        OnTeamMemberSelected.AddListener(TeamMemberSelected);

        GameManager.OnWin.AddListener(()    => SetTeamMembersState(AgentState.Victory));
        GameManager.OnLose.AddListener(()   => SetTeamMembersState(AgentState.Defeat));
    }

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();

        if (members.Length != 3)
            members = GetComponentsInChildren<TeamMember>();
        
        if (members.Exists() && members.Length == 3)
        {
            Vector3[] points = UtilsVector.CreatePointCircleHorizontal(Transform.position, members.Length, memberDistance);
            for (int i = 0; i < points.Length; i++)
            {
                members[i].SetPosition(points[i]);
                members[i].SetColliderRadius(colliderRadius);
            }

            members[0].PosX = 0;
        }
    }
#endif


    public override void StartUpdate()
    {
        base.StartUpdate();
        Accelerate();
        OnTeamMemberSelected?.Invoke(members.GetRandom());
    }

    protected override void ResetComponent()
    {
        base.ResetComponent();

        StopAllCoroutines();
        SetPosition(startPosition);
        SetTeamMembersState(AgentState.Idle);

        selectedMember = null;
        hasKicked = false;
        indicator.Hide();
        
        SetLineActive(false);
        SetLineColor(Color.white);
        OnPlayerMoveProgress?.Invoke(0.0f);
    }


    public override void HandleUpdate()
    {
        base.HandleUpdate();

        if (currentLevelState == LevelState.Move)
            HandleMovement();

        else if (currentLevelState == LevelState.Kick)
            HandleKick();
    }


    void HandleMovement()
    {
        selfTransform.position = Vector3.MoveTowards(selfTransform.position, endPosition, speed * Time.deltaTime);

        float progress = Mathf.Clamp(1 - Vector3.Distance(Position, endPosition) / startDistance, 0, 1);
        //print("Progress: " + progress);
        OnPlayerMoveProgress?.Invoke(progress);

        if (selfTransform.position == endPosition)
            OnPlayerReachGoal?.Invoke();
    }


    void HandleKick()
    {
        if (Ball.Instance.CurrentBallState != BallState.Rotating)
            return;

        if (hasKicked == false && Input.GetMouseButton(0))
        {
            midKickPoint.x = Mathf.Clamp(SingleInputManager.Instance.Horizontal * 1.5f * settings.KickMaxEdge, -settings.KickMaxEdge, settings.KickMaxEdge);
            midKickPoint.y = .5f;
            kickPoints[1] = midKickPoint;
            smoothKickPoints = UtilsVector.GenerateBezierCurve(kickPoints, settings.KickSmoothness);
            Utilities.SetLinePoints(kickLine, smoothKickPoints);
        }

        if (hasKicked == false && Input.GetMouseButtonUp(0))
        {
            TweenLineOpacity();
            indicator.Hide();
            hasKicked = true;
            SetSelectedMemberState(AgentState.Kicking);
            OnKickPathSelected?.Invoke(smoothKickPoints);
        }
    }


    void OnLevelStateChanged(LevelState newState)
    {
        currentLevelState = newState;

        if (newState != LevelState.Kick)
            SetLineActive(false);

        if (newState == LevelState.Move)
            SetTeamMembersState(AgentState.Running);

        else if (newState == LevelState.Kick)
        {
            SetTeamMembersState(AgentState.Idle);
            SetLineActive(true);

            midKickPoint = UtilsVector.GetMidPoint(Ball.Instance.Position, goalPosition);
            midKickPoint.y = .5f;
            //kickPoints = new Vector3[] { Ball.Instance.Position, midKickPoint, goalPosition };
            kickPoints = new Vector3[] { selectedMember.BallPointPass.position, midKickPoint, goalPosition };
            smoothKickPoints = UtilsVector.GenerateBezierCurve(kickPoints, settings.KickSmoothness);
            Utilities.SetLinePoints(kickLine, smoothKickPoints);
        }
    }


    void PlayerPositionsGenerated(Vector3 startPosition, Vector3 endPosition, Vector3 goalPosition)
    {
        this.startPosition = startPosition;
        this.endPosition = endPosition;
        this.goalPosition = goalPosition;

        startDistance = Vector3.Distance(startPosition, endPosition);
        ResetComponent();
    }

    void TeamMemberSelected(TeamMember selected)
    {
        if (currentLevelState != LevelState.Move)
            return;

        if (selectedMember != null && selectedMember.name == selected.name)
            return;

        selectedMember = selected;
        indicator.IndicateMate(selected.Transform);
    }

    void SetTeamMembersState(AgentState targetState)
    {
        for (int i = 0; i < members.Length; i++)
            members[i].ChangeMemberState(targetState);
    }

    void SetSelectedMemberState(AgentState agentState) => selectedMember.ChangeMemberState(agentState);

    void Accelerate()
    {
        if (accelCR != null)
            StopCoroutine(accelCR);

        accelCR = StartCoroutine(AccelCR());
    }

    void Deaccelerate()
    {
        if (deaccelCR != null)
            StopCoroutine(deaccelCR);

        deaccelCR = StartCoroutine(DeaccelCR());
    }

    IEnumerator AccelCR()
    {
        float t = 0;

        while (t < settings.AccelTime)
        {
            speed = Mathf.Lerp(speed, settings.MoveSpeed, t / settings.AccelTime);

            t += Time.deltaTime;
            yield return null;
        }

        speed = settings.MoveSpeed;
    }

    IEnumerator DeaccelCR()
    {
        float t = 0;

        while (t < settings.DeaccelTime)
        {
            speed = Mathf.Lerp(speed, 0, t / settings.DeaccelTime);

            t += Time.deltaTime;
            yield return null;
        }

        speed = 0;
    }

    void SetLineActive(bool isActive)
    {
        if (kickLine.enabled != isActive)
            kickLine.enabled = isActive;
    }

    void TweenLineOpacity()
    {
        Color color = kickLine.startColor;
        TweenColorOpacity tweenOpacity = new TweenColorOpacity(color, .2f)
            .SetDuration(.2f)
            .SetEaseType(EaseType.EaseOutSine)
            .Initialize() as TweenColorOpacity;

        tweenOpacity.OnTweenColorOpacity.AddListener(SetLineColor);
    }

    void SetLineColor(Color color) => Utilities.SetLineColor(kickLine, color);
}