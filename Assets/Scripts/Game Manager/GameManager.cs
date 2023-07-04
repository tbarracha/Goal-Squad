
using UnityEngine;
using StardropTools;

public class GameManager : BaseGameManager
{
    bool hasWon = false;

    public bool HasWon => hasWon;


    protected override void Awake()
    {
        base.Awake();
        ChangeBaseGameState(BaseGameState.MainMenu);
    }

    protected override void EventFlow()
    {
        base.EventFlow();
        Ball.OnBallGoalReached.AddListener(Win);
        Ball.OnBallStollen.AddListener(Lose);
        OnMainMenu.AddListener(ResetComponent);
    }

    protected override void ResetComponent()
    {
        base.ResetComponent();
        hasWon = false;
    }

    protected override void Win()
    {
        ChangeBaseGameState(BaseGameState.PlayResults);
        hasWon = true;
        OnWin?.Invoke();
    }

    protected override void Lose()
    {
        ChangeBaseGameState(BaseGameState.PlayResults);
        hasWon = false;
        OnLose?.Invoke();
    }
}
