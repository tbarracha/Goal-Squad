
using UnityEngine;

namespace StardropTools.Tween
{
    /// <summary>
    /// For a tween to start, it MUST always end with .Initialize();
    /// </summary>
    public abstract class Tween
    {
        protected int tweenID;
        protected TweenType tweenType;
        [Space]
        protected EaseType easeType;
        protected LoopType loopType;
        [Space]
        protected float duration;
        protected float delay;
        protected float percent;
        [Space]
        protected int loopCount;
        protected bool ignoreTimeScale;
        [Space]
        protected AnimationCurve easeCurve;

        protected TweenState tweenState;
        protected float runtime;
        protected bool isValid;

        public bool isInManagerList;

        #region Parameters

        public int TweenID => tweenID;
        public TweenType TweenType => tweenType;

        public float Duration => duration;
        public float Delay => delay;
        public float TotalDuration => duration + delay;

        #endregion // Params


        #region Events

        public readonly EventHandler OnTweenStart = new EventHandler();
        public readonly EventHandler OnTweenComplete = new EventHandler();
        public readonly EventHandler OnTweenTick = new EventHandler();
        public readonly EventHandler OnTweenPaused = new EventHandler();
        public readonly EventHandler OnTweenCanceled = new EventHandler();

        public readonly EventHandler OnDelayStart = new EventHandler();
        public readonly EventHandler OnDelayComplete = new EventHandler();

        public readonly EventHandler<float> OnTweenPercent = new EventHandler<float>();

        #endregion // Events

        #region Get Tween Type

        // Values
        public TweenInt asInt => this as TweenInt;
        public TweenFloat asFloat => this as TweenFloat;
        public TweenVector2 asVector2 => this as TweenVector2;
        public TweenVector3 asVector3 => this as TweenVector3;
        public TweenVector4 asVector4 => this as TweenVector4;
        public TweenQuaternion asQuaternion => this as TweenQuaternion;

        // Shake Values
        public TweenShakeInt asShakeInt => this as TweenShakeInt;
        public TweenShakeFloat asShakeFloat => this as TweenShakeFloat;
        public TweenShakeVector2 asShakeVector2 => this as TweenShakeVector2;
        public TweenShakeVector3 asShakeVector3 => this as TweenShakeVector3;
        public TweenShakeVector4 asShakeVector4 => this as TweenShakeVector4;

        #endregion // Get tween type


        #region Setters

        public Tween SetID(int tweenID)
        {
            this.tweenID = tweenID;
            return this;
        }

        public Tween SetID(GameObject uniqueObject)
        {
            tweenID = uniqueObject.GetInstanceID();
            return this;
        }

        public Tween SetType(TweenType tweenType)
        {
            this.tweenType = tweenType;
            return this;
        }

        public Tween SetIdTag(int tweenID, TweenType tweenType)
        {
            this.tweenID = tweenID;
            this.tweenType = tweenType;
            return this;
        }


        public Tween SetEaseType(EaseType easeType)
        {
            this.easeType = easeType;
            return this;
        }

        public Tween SetLoopType(LoopType loopType)
        {
            this.loopType = loopType;
            return this;
        }        

        public Tween SetEaseAndLoop(EaseType easeType, LoopType loopType)
        {
            this.easeType = easeType;
            this.loopType = loopType;
            return this;
        }

        public Tween SetAnimationCurve(AnimationCurve easeCurve)
        {
            this.easeCurve = easeCurve;
            return this;
        }


        public Tween SetDuration(float duration)
        {
            this.duration = duration;
            return this;
        }

        public Tween SetDelay(float delay)
        {
            this.delay = delay;
            return this;
        }

        public Tween SetDurationAndDelay(float duration, float delay)
        {
            this.duration = duration;
            this.delay = delay;
            return this;
        }

        public Tween SetIgnoreTimeScale(bool ignoreTimeScale)
        {
            this.ignoreTimeScale = ignoreTimeScale;
            return this;
        }

        public Tween SetLoopCount(int loopCount)
        {
            this.loopCount = loopCount;
            return this;
        }

        #endregion // Setters

        public Tween Initialize()
        {
            ResetRuntime();

            if (delay > 0)
                ChangeState(TweenState.Waiting);
            else
                ChangeState(TweenState.Running);

            isValid = TweenManager.Instance.ProcessTween(this);

            if (isValid == true)
                OnTweenStart?.Invoke();

            return this;
        }


        public void UpdateTweenState()
        {
            switch (tweenState)
            {
                case TweenState.Waiting:
                    Waiting();
                    break;

                case TweenState.Running:
                    Running();
                    break;

                case TweenState.Complete:
                    Complete();
                    break;

                case TweenState.Paused:
                    Pause();
                    break;

                case TweenState.Canceled:
                    Stop();
                    break;
            }
        }


        public void ChangeState(TweenState nextState)
        {
            // check if not the same
            if (tweenState == nextState)
                return;

            // to delay
            if (nextState == TweenState.Waiting)
                OnDelayStart?.Invoke();

            // from delay to running
            if (tweenState == TweenState.Waiting && nextState == TweenState.Running)
                OnDelayComplete?.Invoke();

            // to complete
            if (nextState == TweenState.Complete)
                OnTweenComplete?.Invoke();
            
            // to pause
            if (nextState == TweenState.Paused)
                OnTweenPaused?.Invoke();

            // to cancel
            if (nextState == TweenState.Canceled)
                OnTweenCanceled?.Invoke();

            tweenState = nextState;
            ResetRuntime();
        }

        protected virtual void Waiting()
        {
            if (ignoreTimeScale)
                runtime += Time.unscaledDeltaTime;
            else
                runtime += Time.deltaTime;

            if (runtime >= delay)
                ChangeState(TweenState.Running);
        }

        protected virtual void Running()
        {
            if (ignoreTimeScale)
                runtime += Time.unscaledDeltaTime;

            else
                runtime += Time.deltaTime;

            percent = Mathf.Min(runtime / duration, 1);

            TweenUpdate(percent);
            OnTweenPercent?.Invoke(percent);

            if (percent >= 1)
                ChangeState(TweenState.Complete);

            OnTweenTick?.Invoke();
        }

        protected virtual void Complete()
        {
            if (loopType == LoopType.None)
                RemoveFromManagerList();

            else if (loopType == LoopType.Loop)
                Loop();

            else if (loopType == LoopType.PingPong)
                PingPong();
        }

        public virtual void Pause() { }

        public virtual void Stop()
            => RemoveFromManagerList();

        protected float Ease(float percent)
        {
            if (easeType != EaseType.AnimationCurve)
                return TweenEase.Ease(easeType, percent);

            else
            {
                if (easeCurve.keys.Exists(2) == false)
                {
                    Debug.Log("Tween Animation Curve needs more keys!");
                    return TweenEase.Ease(EaseType.Linear, percent);
                }

                return easeCurve.Evaluate(percent);
            }
        }

        protected abstract void SetEssentials();
        protected abstract void TweenUpdate(float percent);
        protected abstract void Loop();
        protected abstract void PingPong();

        protected void RemoveFromManagerList()
        {
            OnTweenStart.ClearAllListeners();
            OnTweenTick.ClearAllListeners();
            OnTweenComplete.ClearAllListeners();
            OnTweenPaused.ClearAllListeners();
            OnTweenCanceled.ClearAllListeners();
            OnDelayStart.ClearAllListeners();
            OnDelayComplete.ClearAllListeners();

            TweenManager.Instance.RemoveTween(this);
        }

        protected void ResetRuntime() => runtime = 0;
    }
}