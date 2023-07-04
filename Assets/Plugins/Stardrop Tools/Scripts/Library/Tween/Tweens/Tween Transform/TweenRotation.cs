
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenRotation : TweenQuaternion
    {
        public Transform target;

        protected override void SetEssentials()
        {
            //tweenID = target.GetHashCode();
            tweenType = TweenType.Rotation;
        }

        public TweenRotation(Transform target, Quaternion start, Quaternion end)
        {
            this.target = target;
            this.start = start;
            this.end = end;

            SetEssentials();
        }

        public TweenRotation(Transform target, Quaternion end)
        {
            this.target = target;
            start = target.rotation;
            this.end = end;

            SetEssentials();
        }

        protected override void TweenUpdate(float percent)
        {
            base.TweenUpdate(percent);
            target.rotation = lerped;
        }
    }


    // Local Rotation
    public class TweenLocalRotation : TweenQuaternion
    {
        public Transform target;

        public TweenLocalRotation(Transform target, Quaternion start, Quaternion end)
        {
            this.target = target;
            this.start = start;
            this.end = end;

            tweenID = target.GetInstanceID();
            tweenType = TweenType.LocalRotation;
        }

        public TweenLocalRotation(Transform target, Quaternion end)
        {
            this.target = target;
            start = target.localRotation;
            this.end = end;

            tweenID = target.GetInstanceID();
            tweenType = TweenType.LocalRotation;
        }

        protected override void TweenUpdate(float percent)
        {
            base.TweenUpdate(percent);

            if (target == null)
                ChangeState(TweenState.Canceled);

            target.localRotation = lerped;
        }
    }
}