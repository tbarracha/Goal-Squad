
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.Audio
{
    /// <summary>
    /// 2 Lists of audios to cycle or randomize through, with different pitch
    /// </summary>
    [CreateAssetMenu(menuName = "Stardrop / Audio / Advanced Audio List")]
    public class AdvancedAudioListSO : ScriptableObject
    {
        [SerializeField] List<AudioClip> mainClips;
        [SerializeField] List<AudioClip> secondaryClips;
        [SerializeField] float[] mainPitchRange;
        [SerializeField] float[] secondaryPitchRange;
        [Space]
        [SerializeField] bool clearAllClips;
        [SerializeField] bool clearMainClips;
        [SerializeField] bool clearSecondaryClips;

        public AudioClip RandomClipMain => GetRandomClipMain();
        public AudioClip RandomClipSecondary => GetRandomClipSecondary();

        public int ClipCountMain => mainClips.Count;
        public int ClipCountSecondary => secondaryClips.Count;
        public int TotalClipCount => ClipCountMain + ClipCountSecondary;

        public float MinPitchMain { get => mainPitchRange[0]; }
        public float MaxPitchMain { get => mainPitchRange[1]; }

        public float MinPitchSecondary { get => secondaryPitchRange[0]; }
        public float MaxPitchSecondary { get => secondaryPitchRange[1]; }

        public float RandomPitchMain { get => GetRandomPitchMain(); }

        public void AddClip(AudioClip clip)
        {
            if (mainClips == null)
                mainClips = new List<AudioClip>();

            if (mainClips.Contains(clip) == false)
                mainClips.Add(clip);
        }

        public void RemoveClip(AudioClip clip)
        {
            if (mainClips.Contains(clip))
                mainClips.Remove(clip);
        }

        public void RemoveClip(int clipID)
        {
            if (mainClips[clipID] != null)
                mainClips.Remove(mainClips[clipID]);
        }

        public void SetClips(AudioClip[] mainClips, AudioClip[] secondaryClips)
        {
            SetMainClips(mainClips);
            SetSecondaryClips(secondaryClips);
        }

        public void SetMainClips(AudioClip[] clips)
        {
            if (mainClips == null)
                mainClips = new List<AudioClip>();

            for (int i = 0; i < clips.Length; i++)
                if (mainClips.Contains(clips[i]) == false)
                    mainClips.Add(clips[i]);
        }

        public void SetSecondaryClips(AudioClip[] clips)
        {
            if (secondaryClips == null)
                secondaryClips = new List<AudioClip>();

            for (int i = 0; i < clips.Length; i++)
                if (secondaryClips.Contains(clips[i]) == false)
                    secondaryClips.Add(clips[i]);
        }

        public AudioClip GetRandomClipMain() => mainClips.GetRandom();
        public AudioClip GetRandomClipSecondary() => mainClips.GetRandom();


        /// <summary>
        /// Returns a clip array with 2 audio clips taken at random from the audio lists: 1 main, 1 secondary
        /// </summary>
        public AudioClip[] GetRandomClips()
        {
            AudioClip[] clips = new AudioClip[2];
            clips[0] = mainClips.GetRandom();
            clips[1] = secondaryClips.GetRandom();

            return clips;
        }


        /// <summary>
        /// Returns a clip array with 1 main and 1 secondary audio clips from a set index
        /// </summary>
        public AudioClip[] GetClipsAtIndex(int mainClipIndex, int secondaryClipIndex)
        {
            AudioClip[] clips = new AudioClip[2];
            clips[0] = mainClips[mainClipIndex];
            clips[1] = secondaryClips[secondaryClipIndex];

            return clips;
        }

        /// <summary>
        /// Returns a clip array with 1 Main, and a set amount random secondary audio clips
        /// <para>if SecondaryClipCount == 0, then will return all secondary clips</para>
        /// </summary>
        public AudioClip[] GetMainAndRandomSecondary(int mainClipIndex, int secondaryClipAmount = 0)
        {
            List<AudioClip> clips = new List<AudioClip>();

            if (secondaryClipAmount > 0)
                clips = secondaryClips.GetRandomNonRepeat(secondaryClipAmount);
            else
                clips = secondaryClips;

            clips.Add(mainClips[mainClipIndex]);

            return clips.ToArray();
        }

        public AudioClip GetClipAtIndexMain(int index) => mainClips[index];
        public AudioClip GetClipAtIndexSecondary(int index) => secondaryClips[index];

        public float GetRandomPitchMain() => Random.Range(MinPitchMain, MaxPitchMain);
        public float GetRandomPitchSecondary() => Random.Range(MinPitchSecondary, MaxPitchSecondary);


        public void ClearClips()
        {
            ClearClipsMain();
            ClearClipsSecondary();
            clearAllClips = false;
        }

        public void ClearClipsMain()
        {
            mainClips = new List<AudioClip>();
            clearMainClips = false;
        }

        public void ClearClipsSecondary()
        {
            secondaryClips = new List<AudioClip>();
            clearSecondaryClips = false;
        }

        protected virtual void OnValidate()
        {
            if (clearAllClips)
                ClearClips();

            if (clearMainClips)
                ClearClipsMain();

            if (clearSecondaryClips)
                ClearClipsSecondary();
        }
    }
}