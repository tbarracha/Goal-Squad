
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenVector3 : Tween
    {
        protected Vector3 start;
        protected Vector3 end;
        protected Vector3 lerped;

        public readonly EventHandler<Vector3> OnTweenVector3 = new EventHandler<Vector3>();

        protected override void SetEssentials()
        {
            tweenType = TweenType.Vector3;
        }

        public TweenVector3()
        {
            SetEssentials();
        }

        public TweenVector3 SetStart(Vector3 start)
        {
            this.start = start;
            return this;
        }

        public TweenVector3 SetEnd(Vector3 end)
        {
            this.end = end;
            return this;
        }

        public TweenVector3 SetStartEnd(Vector3 start, Vector3 end)
        {
            this.start = start;
            this.end = end;
            return this;
        }

        protected override void TweenUpdate(float percent)
        {
            lerped = Vector3.LerpUnclamped(start, end, Ease(percent));
            OnTweenVector3?.Invoke(lerped);
        }

        protected override void Complete()
        {
            base.Complete();
            OnTweenVector3?.Invoke(lerped);
        }

        protected override void Loop()
        {
            ResetRuntime();
            ChangeState(TweenState.Running);
        }

        protected override void PingPong()
        {
            Vector3 temp = start;
            start = end;
            end = temp;

            ResetRuntime();
            ChangeState(TweenState.Running);
        }
    }
}