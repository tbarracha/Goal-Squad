
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenLocalScale : TweenVector3
    {
        public Transform target;

        protected override void SetEssentials()
        {
            //tweenID = target.GetHashCode();
            tweenType = TweenType.LocalScale;
        }

        public TweenLocalScale(Transform target, Vector3 start, Vector3 end)
        {
            this.target = target;
            this.start = start;
            this.end = end;

            target.localScale = start;

            SetEssentials();
        }

        public TweenLocalScale(Transform target, Vector3 end)
        {
            this.target = target;
            start = target.localScale;
            this.end = end;

            SetEssentials();
        }

        protected override void TweenUpdate(float percent)
        {
            base.TweenUpdate(percent);

            if (target == null)
                ChangeState(TweenState.Canceled);

            target.localScale = lerped;
        }
    }
}