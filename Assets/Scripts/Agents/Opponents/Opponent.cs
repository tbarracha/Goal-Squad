
using StardropTools;
using StardropTools.Pool;
using System.Collections;
using UnityEngine;

public abstract class Opponent : Agent, IPoolable
{
    [NaughtyAttributes.Expandable]
    [SerializeField] protected OpponentSettingsSO       settings;
    [SerializeField] protected SphereColliderDetector   ballCollider;
    [SerializeField] protected VisualParticleEffect     alertEffect;
    [SerializeField] protected float                    detectDistance;

    protected Vector3       spawnedPosition;
    protected Quaternion    spawnedRotation;
    protected float         speed;

    protected Coroutine accelCR, deaccelCR;

    protected float moveSpeed           => settings.MoveSpeed;
    protected float lookSpeed           => settings.LookSpeed;
    protected float maxSideEdgeX        => settings.MaxSideEdge;
    protected float ballDetectDistance  => settings.BallDetectDistance;

    protected float accelTime           => settings.AccelTime;
    protected float deaccelTime         => settings.DeaccelTime;


    protected EventHandler OnAccelComplete = new EventHandler();
    protected EventHandler OnDeaccelComplete = new EventHandler();


    #region Poolable
    PoolItem poolItem;

    public void SetPoolItem(PoolItem poolItem) => this.poolItem = poolItem;

    public void Despawn() => poolItem.Despawn();

    public virtual void OnSpawn()
    {
        StopUpdate();

        spawnedPosition = Position;
        spawnedRotation = Rotation;
    }

    public virtual void OnDespawn()
    {
        StopUpdate();
        SetPosition(Vector3.zero);
        alertEffect.Stop();
        alertEffect.SetActive(false);
    }

    #endregion // Poolable


    public override void Initialize()
    {
        base.Initialize();
        ballCollider.OnColliderEnter.AddListener(BallTouched);

        //GameManager.OnPlayStart.AddListener(StartUpdate);
        //GameManager.OnPlayEnd.AddListener(StopUpdate);
        
        GameManager.OnWin.AddListener(OnPlayerWin);
        GameManager.OnLose.AddListener(OnPlayerLose);

        //GameManager.OnRestart.AddListener(ResetComponent);
    }

    public override void StartUpdate()
    {
        if (isActiveAndEnabled == false) return;
        timeInstate = 0;
        base.StartUpdate();
    }

    public override void StopUpdate()
    {
        base.StopUpdate();
        //ChangeState(AgentState.Idle);
    }

    public override void HandleUpdate()
    {
        if (isActiveAndEnabled == false)
            StopUpdate();

        base.HandleUpdate();
        ballCollider.SearchForColliders();
    }

    protected void BallTouched(Collider ballCollider)
    {
        Ball.OnBallTouched?.Invoke(Position);
    }

    protected override void ResetComponent()
    {
        CancelInvoke();
        StopAllCoroutines();

        ChangeState(AgentState.Idle);
        SetPosition(spawnedPosition);
        SetRotation(spawnedRotation);
    }

    public override void ResetComponentPublic() => ResetComponent();

    protected virtual void OnPlayerWin()
    {
        StopUpdate();
        ChangeState(AgentState.Defeat);
    }

    protected virtual void OnPlayerLose()
    {
        StopUpdate();
        ChangeState(AgentState.Victory);
    }

    protected void PlayAlert()
    {
        AudioManager.Instance.PlayAlert();
        alertEffect.Play();
    }

    protected void Accelerate()
    {
        if (!isActiveAndEnabled) return;

        if (accelCR != null)
            StopCoroutine(accelCR);
        accelCR = StartCoroutine(AccelCR());
    }

    protected void Deaccelerate()
    {
        if (!isActiveAndEnabled) return;

        if (deaccelCR != null)
            StopCoroutine(deaccelCR);
        deaccelCR = StartCoroutine(DeaccelCR());
    }

    IEnumerator AccelCR()
    {
        float t = 0;

        while (t < accelTime)
        {
            speed = Mathf.Lerp(0, moveSpeed, t / accelTime);
            t += Time.deltaTime;
            yield return null;
        }

        speed = moveSpeed;
        OnAccelComplete?.Invoke();
    }

    IEnumerator DeaccelCR()
    {
        float t = 0;
        float startSpeed = speed;

        while (t < deaccelTime)
        {
            speed = Mathf.Lerp(startSpeed, 0, t / deaccelTime);
            t += Time.deltaTime;
            yield return null;
        }

        speed = 0;
        OnDeaccelComplete?.Invoke();
    }

    protected void CalculatePlayerDistance() => detectDistance = PosZ - Player.Instance.PosZ;
}