
using UnityEngine;
using StardropTools;
using StardropTools.Tween;

public class CameraManager : BaseManager
{
    [SerializeField] Player player;
    [SerializeField] Transform pivot;
    [SerializeField] Transform startPoint;
    [SerializeField] TweenPositionComponent tweenPosition;
    [SerializeField] BoxColliderDetector boxColliderDetector;
    [SerializeField] float speed = 6;
    [SerializeField] float resetTime = 1;
    [SerializeField] float victoryBackOffset = 7;

    LevelState levelState;

    public override void Initialize()
    {
        base.Initialize();
        tweenPosition.endPos = startPoint.position;
    }

    protected override void EventFlow()
    {
        GameManager.OnPlayStart.AddListener(StartUpdate);
        GameManager.OnRestart.AddListener(ResetComponent);
        GameManager.OnMainMenu.AddListener(ResetComponent);

        LevelManager.OnChangedLevelState.AddListener(LevelStateChanged);
        boxColliderDetector.OnColliderEnter.AddListener(OnColliderDetected);
    }

    void LevelStateChanged(LevelState state) => levelState = state;

    protected override void ResetComponent()
    {
        base.ResetComponent();
        StopUpdate();

        if (pivot.position != Vector3.zero)
            tweenPosition.StartTween();
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();

        if (levelState != LevelState.Finish)
            pivot.position = Vector3.Lerp(pivot.position, player.Position, speed * Time.deltaTime);
        
        else
            pivot.position = Vector3.Lerp(pivot.position, player.Position + Vector3.back * victoryBackOffset, speed * Time.deltaTime);
    }

    void OnColliderDetected(Collider collider)
    {

    }
}
