using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuPlay : UIMenu
{
    [SerializeField] UnityEngine.UI.Slider progressSlider;

    public override void Initialize()
    {
        base.Initialize();
        Player.OnPlayerMoveProgress.AddListener(SetProgress);
    }

    void SetProgress(float progress)
    {
        progressSlider.value = progress;
    }
}
