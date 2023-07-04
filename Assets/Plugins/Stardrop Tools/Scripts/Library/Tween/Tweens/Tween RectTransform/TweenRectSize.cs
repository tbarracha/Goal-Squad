
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenRectSize : TweenVector2
    {
        public RectTransform target;
        Rect rect;

        protected override void SetEssentials()
        {
            //tweenID = target.GetHashCode();
            tweenType = TweenType.RectSize;
        }

        public TweenRectSize(RectTransform target, Vector2 start, Vector2 end)
        {
            this.target = target;
            this.start = start;
            this.end = end;

            SetEssentials();
        }

        public TweenRectSize(RectTransform target, Vector2 end)
        {
            this.target = target;
            start = target.sizeDelta;
            this.end = end;

            SetEssentials();
        }

        protected override void TweenUpdate(float percent)
        {
            base.TweenUpdate(percent);

            if (target == null)
                ChangeState(TweenState.Canceled);

            target.sizeDelta = lerped;
        }
    }
}