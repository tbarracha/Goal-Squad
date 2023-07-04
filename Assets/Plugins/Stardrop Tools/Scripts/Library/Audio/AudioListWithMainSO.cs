
using UnityEngine;

namespace StardropTools.Audio
{
    /// <summary>
    /// WIP, Main audio clip, accompanied by a list of audios to cycle or randomize through, with different pitch
    /// </summary>
    [CreateAssetMenu(menuName = "Stardrop / Audio / Audio List with Main")]
    public class AudioListWithMainSO : ScriptableObject
    {
        [SerializeField] AudioClip mainClip;
        [SerializeField] System.Collections.Generic.List<AudioClip> clips;
        [SerializeField] float minPitch = 1;
        [SerializeField] float maxPitch = 1.2f;

        public int ClipCount => clips.Count;

        public float MinPitch { get => minPitch; }
        public float MaxPitch { get => maxPitch; }

        public float RandomPitch { get => GetRandomPitch(); }
        public AudioClip RandomClip { get => GetRandomClip(); }

        public void AddClip(AudioClip clip)
        {
            if (clips == null)
                clips = new System.Collections.Generic.List<AudioClip>();

            if (clips.Contains(clip) == false)
                clips.Add(clip);
        }

        public void RemoveClip(AudioClip clip)
        {
            if (clips.Contains(clip))
                clips.Remove(clip);
        }

        public void RemoveClip(int clipID)
        {
            if (clips[clipID] != null)
                clips.Remove(clips[clipID]);
        }


        public void SetClips(AudioClip[] clips)
        {
            if (this.clips == null)
                this.clips = new System.Collections.Generic.List<AudioClip>();

            for (int i = 0; i < clips.Length; i++)
                if (this.clips.Contains(clips[i]) == false)
                    this.clips.Add(clips[i]);
        }


        public AudioClip GetRandomClip() => clips[Random.Range(0, clips.Count)];

        public AudioClip GetClipAtIndex(int index) => clips[index];

        public float GetRandomPitch() => Random.Range(minPitch, maxPitch);


        [NaughtyAttributes.Button("Clear Clips")]
        public void ClearClips()
        {
            clips = new System.Collections.Generic.List<AudioClip>();
        }
    }
}