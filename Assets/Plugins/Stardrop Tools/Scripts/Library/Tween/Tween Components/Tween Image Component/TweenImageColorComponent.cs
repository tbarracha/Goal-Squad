
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenImageColorComponent : TweenImageComponent
    {
        public Color startColor;
        public Color endColor;

        public override Tween StartTween()
        {
            if (hasStart)
                tween = new TweenImageColor(target, startColor, endColor);
            else
                tween = new TweenImageColor(target, endColor);

            SetTweenEssentials();
            tween.SetID(target.GetHashCode()).Initialize();
            StartSequence();

            return tween;
        }

        [NaughtyAttributes.Button("Get Start Image Color")]
        private void GetStart()
        {
            startColor = target.color;
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