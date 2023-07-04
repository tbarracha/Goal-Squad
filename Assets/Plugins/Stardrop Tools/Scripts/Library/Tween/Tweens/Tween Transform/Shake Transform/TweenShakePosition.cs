
using UnityEngine;

namespace StardropTools.Tween
{
    /// <summary>
    /// Set Intensity before all else
    /// </summary>
    public class TweenShakePosition : TweenShakeVector3
    {
        public Transform target;

        public TweenShakePosition(Transform target, Vector3 start, Vector3 end)
        {
            this.target = target;
            this.start = start;
            this.end = end;

            tweenID = target.GetInstanceID();
            tweenType = TweenType.ShakePosition;
        }

        public TweenShakePosition(Transform target, Vector3 end)
        {
            this.target = target;
            start = target.position;
            this.end = end;

            tweenID = target.GetInstanceID();
            tweenType = TweenType.ShakePosition;
        }

        protected override void TweenUpdate(float percent)
        {
            base.TweenUpdate(percent);
            target.position = lerped;
        }
    }


    // Local Position
    /// <summary>
    /// Set Intensity before all else
    /// </summary>
    public class TweenShakeLocalPosition : TweenShakeVector3
    {
        public Transform target;

        protected override void SetEssentials()
        {
            //tweenID = target.GetInstanceID();
            tweenType = TweenType.ShakeLocalPosition;
        }

        public TweenShakeLocalPosition(Transform target, Vector3 start, Vector3 end)
        {
            this.target = target;
            this.start = start;
            this.end = end;

            SetEssentials();
        }

        public TweenShakeLocalPosition(Transform target, Vector3 end)
        {
            this.target = target;

            start = target.localPosition;
            this.end = end;

            SetEssentials();
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