
using UnityEngine;
using UnityEngine.UI;
using StardropTools;
using StardropTools.UI;
using StardropTools.Tween;

public abstract class UIMenu : UIMenuBase
{
    [Header("Generic Buttons")]
    [SerializeField] protected Button[] playButtons; 
    [SerializeField] protected Button[] replayButtons;
    [SerializeField] protected Button[] nextLevelButtons;
    [Space]
    [SerializeField] TweenComponentManager tweenComponentManager;


    public override void Initialize()
    {
        base.Initialize();

        for (int i = 0; i < playButtons.Length; i++)
            playButtons[i].onClick.AddListener(() => GameManager.OnPlayRequest?.Invoke());

        for (int i = 0; i < replayButtons.Length; i++)
            replayButtons[i].onClick.AddListener(() => GameManager.OnRestartRequest?.Invoke());

        for (int i = 0; i < nextLevelButtons.Length; i++)
            nextLevelButtons[i].onClick.AddListener(() => GameManager.OnNextLevelRequest?.Invoke());
    }

    public override void Open()
    {
        base.Open();

        if (tweenComponentManager != null)
            tweenComponentManager.StartTweens();
    }

    public override void Close()
    {
        if (tweenComponentManager != null)
            tweenComponentManager.StopTweens();

        SetActive(false);
    }
}