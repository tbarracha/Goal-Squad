
using UnityEngine;

namespace StardropTools.Tween
{
    public abstract class TweenComponent : MonoBehaviour
    {
        [Header("Tweens To Sequence")]
        [SerializeField] protected TweenComponentSequence sequence;

        [Header("Tween Properties")]
        public EaseType easeType;
        public LoopType loopType;
        [NaughtyAttributes.ShowIf("showCurve")]
        public AnimationCurve curve; // only show this if easeType is set to AnimationCurve
        public float delay;
        public float duration;
        public bool hasStart;

        protected Tween tween;
        protected bool showCurve;


        public EventHandler OnDelayCompleted => tween.OnDelayComplete;
        public EventHandler OnTweenCompleted => tween.OnTweenComplete;
        public EventHandler OnTweenPaused => tween.OnTweenPaused;
        public EventHandler OnTweenCanceled => tween.OnTweenCanceled;


        public abstract Tween StartTween();

        public void StopTween()
        {
            if (tween != null)
                tween.Stop();
        }

        public void PauseTween()
        {
            if (tween != null)
                tween.Pause();
        }

        protected virtual void SetTweenEssentials()
        {
            tween.SetEaseAndLoop(easeType, loopType)
                .SetDurationAndDelay(duration, delay);

            if (easeType == EaseType.AnimationCurve)
                tween.SetAnimationCurve(curve);
        }

        protected void StartSequence()
        {
            if (sequence != null)
                sequence.StartSequence();
        }

        protected virtual void OnValidate()
        {
            showCurve = easeType == EaseType.AnimationCurve ? true : false;
        }
    }
}