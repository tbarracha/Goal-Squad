
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenSpriteOpacityComponent : TweenSpriteComponent
    {
        [Range(0, 1)] public float startOpacity;
        [Range(0, 1)] public float endOpacity;

        public override Tween StartTween()
        {
            if (hasStart)
                tween = new TweenSpriteRendererOpacity(target, startOpacity, endOpacity);
            else
                tween = new TweenSpriteRendererOpacity(target, endOpacity);

            SetTweenEssentials();
            tween.SetID(target.GetHashCode()).Initialize();
            StartSequence();

            return tween;
        }

        [NaughtyAttributes.Button("Get Start Opacity")]
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
    }
}