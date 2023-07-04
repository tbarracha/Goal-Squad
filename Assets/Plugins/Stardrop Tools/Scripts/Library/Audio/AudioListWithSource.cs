

namespace StardropTools.Audio
{
    /// <summary>
    /// Class that contains an Audio Source and an Scriptable Object Audio List.
    /// <para>Play clip or One Shot Clip at the Audio Source</para>
    /// <para>Play Random clip from list, or select clip index</para>
    /// </summary>
    [System.Serializable]
    public class AudioListWithSource
    {
        [UnityEngine.SerializeField] UnityEngine.AudioSource source;
        [NaughtyAttributes.Expandable] [UnityEngine.SerializeField] AudioListSO clipList;

        public UnityEngine.AudioSource Source { get => source; }
        public AudioListSO AudioDB { get => clipList; }
        public UnityEngine.AudioClip RandomClip { get => clipList.GetRandomClip(); }
        public float RandomPitch { get => clipList.GetRandomPitch(); }

        public UnityEngine.AudioClip GetClipAtIndex(int clipIndex) => clipList.GetClipAtIndex(clipIndex);

        #region Play Audio

        /// <summary>
        /// Plays all audios as One Shot Clips
        /// </summary>
        public void PlayAll(bool randomPitch = false)
        {
            for (int i = 0; i < clipList.ClipCount; i++)
                PlayClipOneShotAtIndex(i, randomPitch);
        }


        /// <summary>
        /// Selects a random clip from list, attatches to AudioClip and plays it
        /// </summary>
        public void PlayRandom(bool randomPitch = false)
        {
            if (randomPitch)
                source.pitch = RandomPitch;

            StopPlaying();
            source.clip = RandomClip;
            source.Play();
        }

        /// <summary>
        /// Selects a random clip from list and Plays One Shot Audio Clip
        /// </summary>
        public void PlayRandomOneShot(bool randomPitch = false)
        {
            if (randomPitch)
                source.pitch = RandomPitch;

            source.PlayOneShot(RandomClip);
        }


        /// <summary>
        /// Selects a clip at list index, attatches to AudioClip and plays it
        /// </summary>
        public void PlayClipAtIndex(int clipIndex, bool randomPitch = false)
        {
            StopPlaying();
            source.clip = clipList.GetClipAtIndex(clipIndex);

            if (randomPitch)
                source.pitch = RandomPitch;

            source.Play();
        }

        /// <summary>
        /// Selects a clip from list at index and Plays One Shot Audio Clip
        /// </summary>
        public void PlayClipOneShotAtIndex(int clipIndex, bool randomPitch)
        {
            if (randomPitch)
                source.pitch = RandomPitch;

            source.PlayOneShot(clipList.GetClipAtIndex(clipIndex));
        }

        public void StopPlaying()
        {
            if (source.isPlaying)
                source.Stop();
        }
        #endregion // Play audio

        public void SetVolume(float value) => source.volume = value;
        public void SetPitch(float value) => source.pitch = value;
    }
}