
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenShakeScaleComponent : TweenShakeTransformComponent
    {
        public override Tween StartTween()
        {
            tween = new TweenShakeLocalScale(target, target.localScale);

            SetTweenEssentials();
            tween.SetID(target.GetHashCode()).Initialize();
            StartSequence();

            return tween;
        }

        [NaughtyAttributes.Button("Start Tween")]
        private void TweenStart()
        {
            if (Application.isPlaying)
                StartTween();
        }


        protected override void OnValidate()
        {
            base.OnValidate();
        }
    }
}