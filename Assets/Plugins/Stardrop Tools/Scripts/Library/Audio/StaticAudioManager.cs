
using UnityEngine;

namespace StardropTools.Audio
{
    public static class StaticAudioManager
    {
        public static void PlayOneShotAtSource(AudioSource source, AudioClip clip, float pitch = 1)
        {
            source.pitch = pitch;
            source.PlayOneShot(clip);
        }

        public static void PlayRandomOneShotAtSource(AudioSource source, AudioClip[] clips, float pitch = 1)
        {
            source.pitch = pitch;
            source.PlayOneShot(clips[Random.Range(0, clips.Length)]);
        }


        public static void PlayFromAudioGroup(AudioSource source, AudioListSO group, int index, float pitch = 1)
            => PlayOneShotAtSource(source, group.GetClipAtIndex(index), pitch);

        public static void PlayRandomFromAudioGroup(AudioSource source, AudioListSO group, float pitch = 1)
            => PlayOneShotAtSource(source, group.GetRandomClip(), pitch);


        public static void PlayFromAudioGroupWithSource(AudioListWithSource audioSourceDB, int clipIndex, float pitch = 1)
            => PlayOneShotAtSource(audioSourceDB.Source, audioSourceDB.GetClipAtIndex(clipIndex), pitch);

        public static void PlayRandomFromAudioGroupWithSource(AudioListWithSource audioSourceDB, float pitch)
            => PlayOneShotAtSource(audioSourceDB.Source, audioSourceDB.RandomClip, pitch);


        public static void PlaySourceClip(AudioSourceWithClip sourceClip)
        {
            sourceClip.Play();
        }

        public static void PlaySourceClipOneShot(AudioSourceWithClip sourceClip)
        {
            sourceClip.Source.PlayOneShot(sourceClip.Clip);
        }
    }
}