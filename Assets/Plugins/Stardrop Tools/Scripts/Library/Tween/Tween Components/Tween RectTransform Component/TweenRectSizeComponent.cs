
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenRectSizeComponent : TweenRectTransformComponent
    {
        public Vector2 startRectSize;
        public Vector2 endRectSize;

        public override Tween StartTween()
        {
            if (hasStart)
                tween = new TweenRectSize(target, startRectSize, endRectSize);
            else
                tween = new TweenRectSize(target, endRectSize);

            SetTweenEssentials();
            tween.SetID(target.GetHashCode()).Initialize();
            StartSequence();

            return tween;
        }

        [NaughtyAttributes.Button("Get Start Rect Size")]
        private void GetStart()
        {
            startRectSize = target.rect.size;
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