
using StardropTools.UI;
using UnityEngine;

public class UIMenuOptions : UIMenuTweenedBase
{
    [SerializeField] UIToggleButton toggleAudio;
    [SerializeField] UIToggleButton toggleVibration;

    public override void Initialize()
    {
        base.Initialize();
        Close();

        toggleAudio.OnToggleValue.AddListener(ToggleAudio);
    }

    public override void Close()
    {
        base.Close();
        SetActive(false);
    }

    void ToggleAudio(bool value) => AudioManager.OnAudioToggled?.Invoke(value);
}