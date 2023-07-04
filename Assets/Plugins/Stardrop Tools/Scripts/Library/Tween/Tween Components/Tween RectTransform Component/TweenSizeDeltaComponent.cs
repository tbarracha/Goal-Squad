
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenSizeDeltaComponent : TweenRectTransformComponent
    {
        public Vector2 startSizeDelta;
        public Vector2 endSizeDelta;

        public override Tween StartTween()
        {
            if (hasStart)
                tween = new TweenSizeDelta(target, startSizeDelta, endSizeDelta);
            else
                tween = new TweenSizeDelta(target, endSizeDelta);

            SetTweenEssentials();
            tween.SetID(target.GetHashCode()).Initialize();
            StartSequence();

            return tween;
        }

        [NaughtyAttributes.Button("Get Start Size Delta")]
        private void GetStart()
        {
            startSizeDelta = target.rect.size;
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