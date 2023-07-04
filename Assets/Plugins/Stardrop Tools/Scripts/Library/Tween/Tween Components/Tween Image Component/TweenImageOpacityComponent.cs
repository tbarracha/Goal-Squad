
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenImageOpacityComponent : TweenImageComponent
    {
        [Range(0, 1)] public float startOpacity;
        [Range(0, 1)] public float endOpacity;

        public override Tween StartTween()
        {
            if (hasStart)
                tween = new TweenImageOpacity(target, startOpacity, endOpacity);
            else
                tween = new TweenImageOpacity(target, endOpacity);

            SetTweenEssentials();
            tween.SetID(target.GetHashCode()).Initialize();
            StartSequence();

            return tween;
        }

        [NaughtyAttributes.Button("Get Start Opaicty")]
        private void GetStart()
        {
            startOpacity = target.color.a;
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