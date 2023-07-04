
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenAnchoredPositionComponent : TweenRectTransformComponent
    {
        public Vector2 startPos;
        public Vector2 endPos;

        public override Tween StartTween()
        {
            if (hasStart)
                tween = new TweenAnchoredPosition(target, startPos, endPos);
            else
                tween = new TweenAnchoredPosition(target, endPos);

            SetTweenEssentials();
            tween.SetID(target.GetHashCode()).Initialize();
            StartSequence();

            return tween;
        }

        [NaughtyAttributes.Button("Get Start Anchored Position")]
        private void GetStart()
        {
            startPos = target.anchoredPosition;
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