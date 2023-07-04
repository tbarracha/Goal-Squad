
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenPosition : TweenVector3
    {
        public Transform target;

        protected override void SetEssentials()
        {
            //tweenID = target.GetHashCode();
            tweenType = TweenType.Position;
        }

        public TweenPosition(Transform target, Vector3 start, Vector3 end)
        {
            this.target = target;
            this.start = start;
            this.end = end;

            SetEssentials();
        }

        public TweenPosition(Transform target, Vector3 end)
        {
            this.target = target;
            start = target.position;
            this.end = end;

            SetEssentials();
        }

        protected override void TweenUpdate(float percent)
        {
            base.TweenUpdate(percent);
            target.position = lerped;
        }
    }


    // Local Position
    public class TweenLocalPosition : TweenVector3
    {
        public Transform target;

        public TweenLocalPosition(Transform target, Vector3 start, Vector3 end)
        {
            this.target = target;
            this.start = start;
            this.end = end;

            tweenType = TweenType.Position;
        }

        public TweenLocalPosition(Transform target, Vector3 end)
        {
            this.target = target;

            start = target.localPosition;
            this.end = end;

            tweenType = TweenType.LocalPosition;
        }


        protected override void TweenUpdate(float percent)
        {
            base.TweenUpdate(percent);

            if (target == null)
                ChangeState(TweenState.Canceled);

            target.localPosition = lerped;
        }
    }
}