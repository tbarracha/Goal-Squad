
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenSizeDelta : TweenVector2
    {
        public RectTransform target;

        protected override void SetEssentials()
        {
            //tweenID = target.GetHashCode();
            tweenType = TweenType.SizeDelta;
        }

        public TweenSizeDelta(RectTransform target, Vector2 start, Vector2 end)
        {
            this.target = target;
            this.start = start;
            this.end = end;

            SetEssentials();
        }

        public TweenSizeDelta(RectTransform target, Vector2 end)
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