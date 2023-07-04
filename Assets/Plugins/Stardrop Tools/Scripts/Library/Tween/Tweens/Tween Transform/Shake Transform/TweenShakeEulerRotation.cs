
using UnityEngine;

namespace StardropTools.Tween
{
    /// <summary>
    /// Set Intensity before all else
    /// </summary>
    public class TweenShakeEulerRotation : TweenShakeVector3
    {
        public Transform target;

        protected override void SetEssentials()
        {
            //tweenID = target.GetHashCode();
            tweenType = TweenType.ShakeEulerRotation;
        }

        public TweenShakeEulerRotation(Transform target, Vector3 start, Vector3 end)
        {
            this.target = target;
            this.start = start;
            this.end = end;

            SetEssentials();
        }

        public TweenShakeEulerRotation(Transform target, Vector3 end)
        {
            this.target = target;
            start = target.eulerAngles;
            this.end = end;

            SetEssentials();
        }

        protected override void TweenUpdate(float percent)
        {
            base.TweenUpdate(percent);
            target.eulerAngles = lerped;
        }
    }


    // Local Rotation
    /// <summary>
    /// Set Intensity before all else
    /// </summary>
    public class TweenShakeLocalEulerRotation : TweenShakeVector3
    {
        public Transform target;

        protected override void SetEssentials()
        {
            tweenID = target.GetInstanceID();
            tweenType = TweenType.ShakeLocalEulerRotation;
        }

        public TweenShakeLocalEulerRotation(Transform target, Vector3 start, Vector3 end)
        {
            this.target = target;
            this.start = start;
            this.end = end;

            SetEssentials();
        }

        public TweenShakeLocalEulerRotation(Transform target, Vector3 end)
        {
            this.target = target;

            start = target.localEulerAngles;
            this.end = end;

            SetEssentials();
        }


        protected override void TweenUpdate(float percent)
        {
            base.TweenUpdate(percent);

            if (target == null)
                ChangeState(TweenState.Canceled);

            target.localEulerAngles = lerped;
        }
    }
}