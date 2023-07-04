
namespace StardropTools.Audio
{
    [System.Serializable]
    public class AudioSourceWithClip
    {
        [UnityEngine.SerializeField] UnityEngine.AudioSource source;
        [UnityEngine.SerializeField] UnityEngine.AudioClip clip;

        public UnityEngine.AudioSource Source { get => source; }
        public UnityEngine.AudioClip Clip { get => clip; }

        public void Play()
        {
            if (source.clip != clip)
                source.clip = clip;

            source.Play();
        }

        public void Stop() => source.Stop();

        public void SetClip(UnityEngine.AudioClip newClip, bool play = false)
        {
            if (newClip != clip)
                clip = newClip;

            source.clip = clip;

            if (play)
                Play();
        }

        public void SetVolume(float value) => source.volume = value;
        public void SetPitch(float value) => source.pitch = value;
    }
}