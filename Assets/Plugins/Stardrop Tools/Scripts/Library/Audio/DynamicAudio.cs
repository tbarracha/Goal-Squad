
using UnityEngine;

namespace StardropTools.Audio
{
    /// <summary>
    /// Components that allows for playing random audio clips with random pitch from a single audio source
    /// </summary>
    public class DynamicAudio : MonoBehaviour
    {
        [SerializeField] AudioListWithSource source;


        [NaughtyAttributes.Button("Play All")]
        public void PlayAll(bool randomPitch = false) => source.PlayAll(randomPitch);



        [NaughtyAttributes.Button("Play Random")]
        public void PlayRandom(bool randomPitch = false) => source.PlayRandom(randomPitch);

        public void PlayClipAtIndex(int clipIndex, bool randomPitch = false) => source.PlayClipAtIndex(clipIndex, randomPitch);



        [NaughtyAttributes.Button("Play Random One Shot")]
        public void PlayRandomOneShot(bool randomPitch = false) => source.PlayRandomOneShot(randomPitch);

        public void PlayClipAtIndexOneShot(int clipIndex, bool randomPitch = false) => source.PlayClipOneShotAtIndex(clipIndex, randomPitch);



#if UNITY_EDITOR
        [SerializeField] int clipIndex;

        [NaughtyAttributes.Button("Play Clip At Index")]
        public void PlayClipAtIndex(bool randomPitch = false) => source.PlayClipAtIndex(clipIndex, randomPitch);

        [NaughtyAttributes.Button("Play Clip At Index One Shot")]
        public void PlayClipAtIndexOneShot(bool randomPitch = false) => source.PlayClipOneShotAtIndex(clipIndex, randomPitch);
#endif
    }

}