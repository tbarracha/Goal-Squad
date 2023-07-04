
using UnityEngine;

namespace StardropTools.Audio
{
    /// <summary>
    /// Class that contains an Audio Source and an Scriptable Object Audio List.
    /// <para>Play clip or One Shot Clip at the Audio Source</para>
    /// <para>Play Random clip from list, or select clip index</para>
    /// </summary>
    [System.Serializable]
    public class AdvancedAudioListWithSource
    {
        [SerializeField] AudioSource source;
        [NaughtyAttributes.Expandable] [SerializeField] AdvancedAudioListSO clipList;

        public AudioSource Source => source;
        public AdvancedAudioListSO AudioDB => clipList;

        public AudioClip RandomClipMain => clipList.RandomClipMain;
        public AudioClip RandomClipSecondary => clipList.RandomClipSecondary;

        public float RandomPitchMain => RandomPitchMain;
        public float RandomPitchSecondary => RandomPitchSecondary;
        public float RandomPitch
        {
            get
            {
                float[] randoms = new float[2];
                randoms[0] = RandomPitchMain;
                randoms[1] = RandomPitchSecondary;

                return randoms.GetRandom();
            }
        }


        public AudioClip GetClipAtIndex(int clipIndex) => clipList.GetClipAtIndexMain(clipIndex);

        public void SetVolume(float value) => source.volume = value;
        public void SetPitch(float value) => source.pitch = value;

        /// <summary>
        /// Plays all audios from both lists as One Shot Clips
        /// </summary>
        public void PlayAll(bool randomPitch = false)
        {
            for (int i = 0; i < clipList.ClipCountMain; i++)
                PlayClipOneShotAtIndex(0, i, randomPitch);

            for (int i = 0; i < clipList.ClipCountSecondary; i++)
                PlayClipOneShotAtIndex(0, i, randomPitch);
        }

        /// <summary>
        /// if secondaryClipCount == 0, then will play all secondary clips
        /// </summary>
        public void PlayMainWithRandomSecondaries(int mainClipIndex = 0, int secondaryClipCount = 0, bool randomPitch = false)
        {
            AudioClip[] clips = clipList.GetMainAndRandomSecondary(mainClipIndex, secondaryClipCount);

            for (int i = 0; i < clipList.ClipCountMain; i++)
            {
                source.pitch = RandomPitch;
                source.PlayOneShot(clips[i]);
            }
        }

        /// <summary>
        /// Plays 1 main and 1 secondary random audio clips
        /// </summary>
        public void PlayRandomOneShot(bool randomPitch = false)
        {
            CheckRandomPitch(0, randomPitch);
            CheckRandomPitch(1, randomPitch);

            source.PlayOneShot(RandomClipMain);
            source.PlayOneShot(RandomClipSecondary);
        }

        /// <summary>
        /// Selects a clip from list at index and Plays One Shot Audio Clip
        /// <para>ListIndex: 0 = main, 1 = secondary</para>
        /// </summary>
        public void PlayClipOneShotAtIndex(int listIndex, int clipIndex, bool randomPitch)
        {
            CheckRandomPitch(listIndex, randomPitch);

            if (listIndex == 0)
                source.PlayOneShot(clipList.GetClipAtIndexMain(clipIndex));
            else
                source.PlayOneShot(clipList.GetClipAtIndexSecondary(clipIndex));
        }

        /// <summary>
        /// 0 = main, 1 = secondary
        /// </summary>
        void CheckRandomPitch(int listIndex, bool value)
        {
            if (value)
            {
                if (listIndex == 0)
                    source.pitch = RandomPitchMain;
                else
                    source.pitch = RandomPitchSecondary;
            }
        }

        public void StopPlaying()
        {
            if (source.isPlaying)
                source.Stop();
        }
    }
}