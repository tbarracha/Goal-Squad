
using UnityEngine;

namespace StardropTools.Tween
{
    /// <summary>
    /// Set Intensity before all else
    /// </summary>
    public class TweenShakeLocalScale : TweenShakeVector3
    {
        public Transform target;

        protected override void SetEssentials()
        {
            //tweenID = target.GetHashCode();
            tweenType = TweenType.ShakeLocalScale;
        }

        public TweenShakeLocalScale(Transform target, Vector3 start, Vector3 end)
        {
            this.target = target;
            this.start = start;
            this.end = end;

            SetEssentials();
        }

        public TweenShakeLocalScale(Transform target, Vector3 end)
        {
            this.target = target;
            start = target.localScale;
            this.end = end;

            SetEssentials();
        }

        protected override void TweenUpdate(float percent)
        {
            base.TweenUpdate(percent);
            target.localScale = lerped;
        }

        protected override void Complete()
        {
            base.Complete();

            if (target == null)
                ChangeState(TweenState.Canceled);

            target.localScale = end;
        }
    }
}