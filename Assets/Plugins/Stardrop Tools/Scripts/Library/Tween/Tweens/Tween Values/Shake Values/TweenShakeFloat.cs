
using UnityEngine;

namespace StardropTools.Tween
{
    /// <summary>
    /// Set Intensity before all else
    /// </summary>
    public class TweenShakeFloat : TweenFloat
    {
        protected float intensity;

        protected override void SetEssentials()
        {
            tweenType = TweenType.ShakeFloat;
        }

        public TweenShakeFloat()
        {
            SetEssentials();
        }

        public TweenShakeFloat SetIntensity(float intensity)
        {
            this.intensity = intensity;
            return this;
        }

        protected override void TweenUpdate(float percent)
        {
            if (percent == 0)
                lerped = start;

            percent = 1 - percent;

            float amount = intensity * Ease(percent);
            lerped = start + Random.Range(-amount, amount);
        }
    }
}