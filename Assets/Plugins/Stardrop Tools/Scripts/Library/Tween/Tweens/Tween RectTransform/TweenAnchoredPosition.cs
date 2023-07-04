
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenAnchoredPosition : TweenVector2
    {
        public RectTransform target;

        protected override void SetEssentials()
        {
            //tweenID = target.GetHashCode();
            tweenType = TweenType.ImageColor;
        }

        public TweenAnchoredPosition(RectTransform target, Vector2 start, Vector2 end)
        {
            this.target = target;
            this.start = start;
            this.end = end;

            SetEssentials();
        }

        public TweenAnchoredPosition(RectTransform target, Vector2 end)
        {
            this.target = target;
            start = target.anchoredPosition;
            this.end = end;

            SetEssentials();
        }

        protected override void TweenUpdate(float percent)
        {
            base.TweenUpdate(percent);

            if (target == null)
                ChangeState(TweenState.Canceled);

            target.anchoredPosition = lerped;
        }
    }
}