
using UnityEngine;

namespace StardropTools.Tween
{
    /// <summary>
    /// Set Intensity before all else
    /// </summary>
    public class TweenShakeInt : TweenInt
    {
        protected int intensity;

        protected override void SetEssentials()
        {
            tweenType = TweenType.ShakeInt;
        }

        public TweenShakeInt()
        {
            SetEssentials();
        }

        public TweenShakeInt SetIntensity(int intensity)
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
            lerped = (int)(start + Random.Range(-amount, amount));
        }
    }
}