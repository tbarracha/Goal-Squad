
using UnityEngine;
using StardropTools;
using StardropTools.Audio;
using StardropTools.PlayerPreferences;

public class AudioManager : BaseManagerSingleton<AudioManager>
{
    [SerializeField] PlayerPrefBool playerPref_CanPlay;

    [Header("Audio Sources")]
    [SerializeField] AudioSource sourceAmbiance;
    [Tooltip("0-pass, 1-whistle, 2-alert, 3-goal, 4-win/lose")]
    [SerializeField] AudioSource[] sources;

    [Header("Audio Lists")]
    [SerializeField] AudioListSO audioList_Pass;
    [SerializeField] AudioListSO audioList_Whistle;
    [SerializeField] AudioListSO audioList_Alert;
    [SerializeField] AudioListSO audioList_Goal;
    [Space]
    [SerializeField] AudioListSO audioList_Win;
    [SerializeField] AudioListSO audioList_Lose;
    [Space]
    [SerializeField] AudioListSO audioList_Music;


    bool CanPlay => playerPref_CanPlay.Bool;

    public static readonly EventHandler<bool> OnAudioToggled = new EventHandler<bool>();


    public override void Initialize()
    {
        base.Initialize();
        OnAudioToggled.AddListener(AudioToggled);

        PlayAmbience();
    }

    protected override void EventFlow()
    {
        Ball.OnBallGoalReached.AddListener(PlayGoal);
        Ball.OnBallStollen.AddListener(PlayPass);

        GameManager.OnWin.AddListener(PlayWin);
        GameManager.OnLose.AddListener(PlayLose);
        GameManager.OnPlayEnd.AddListener(PlayWhistleEnd);

        LevelManager.OnChangedLevelState.AddListener(LevelStateChanged);
    }

    void LevelStateChanged(LevelState levelState)
    {
        switch (levelState)
        {
            case LevelState.Move:
                PlayWhistleStart();
                break;

            case LevelState.Kick:
                PlayWhistleKick();
                break;
        }
    }

    void AudioToggled(bool value)
    {
        playerPref_CanPlay.SetBool(value, true);
        if (value == false)
            StopAmbience();
        else
            PlayAmbience();
    }

    public void PlayAudio(AudioSource source, AudioClip clip, float pitch = 1)
    {
        if (CanPlay == false)
            return;

        source.Stop();
        source.clip = clip;
        source.Play();
    }

    public void PlayOneShot(AudioSource source, AudioClip clip, float pitch = 1)
    {
        if (CanPlay == false)
            return;

        source.PlayOneShot(clip, pitch);
    }

    public void PlayPass() => PlayOneShot(sources[0], audioList_Pass.RandomClip, audioList_Pass.RandomPitch);

    public void PlayWhistleStart() => PlayOneShot(sources[1], audioList_Whistle.GetClipAtIndex(0), audioList_Whistle.RandomPitch);
    public void PlayWhistleKick() => PlayOneShot(sources[1], audioList_Whistle.GetClipAtIndex(1), audioList_Whistle.RandomPitch);
    public void PlayWhistleEnd() => PlayOneShot(sources[1], audioList_Whistle.GetClipAtIndex(2), audioList_Whistle.RandomPitch);

    public void PlayAlert() => PlayOneShot(sources[2], audioList_Alert.RandomClip, audioList_Alert.RandomPitch);

    public void PlayGoal() => PlayOneShot(sources[3], audioList_Goal.RandomClip, audioList_Goal.RandomPitch);

    public void PlayWin() => PlayOneShot(sources[4], audioList_Win.RandomClip, audioList_Win.RandomPitch);
    public void PlayLose() => PlayOneShot(sources[4], audioList_Lose.RandomClip, audioList_Lose.RandomPitch);

    public void PlayAmbience() => PlayAudio(sourceAmbiance, audioList_Music.GetClipAtIndex(0));
    public void StopAmbience() => sourceAmbiance.Stop();
}
