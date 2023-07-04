
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenComponentManager : MonoBehaviour
    {
        [SerializeField] TweenComponent[] tweens;

        [NaughtyAttributes.Button("Get Tweens")]
        public void GetTweens()
        {
            tweens = Utilities.GetListComponentsInChildren<TweenComponent>(transform).ToArray();
        }
        
        [NaughtyAttributes.Button("Start Tweens")]
        public void StartTweens()
        {
            for (int i = 0; i < tweens.Length; i++)
                tweens[i].StartTween();
        }

        [NaughtyAttributes.Button("Stop Tweens")]
        public void StopTweens()
        {
            for (int i = 0; i < tweens.Length; i++)
                tweens[i].StopTween();
        }

        public void PauseTweens()
        {
            for (int i = 0; i < tweens.Length; i++)
                tweens[i].PauseTween();
        }
    }
}