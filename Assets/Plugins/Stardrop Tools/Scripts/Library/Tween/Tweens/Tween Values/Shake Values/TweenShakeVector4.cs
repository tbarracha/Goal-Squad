
using UnityEngine;

namespace StardropTools.Tween
{
    /// <summary>
    /// Set Intensity before all else
    /// </summary>
    public class TweenShakeVector4 : TweenVector4
    {
        protected Vector4 intensity;

        protected override void SetEssentials()
        {
            tweenType = TweenType.ShakeVector4;
        }

        public TweenShakeVector4()
        {
            SetEssentials();
        }

        public TweenShakeVector4 SetIntensity(Vector4 intensity)
        {
            this.intensity = intensity;
            return this;
        }

        public TweenShakeVector4 SetIntensity(float intensityMultiplier)
        {
            intensity = Vector4.one * intensityMultiplier;
            return this;
        }

        protected override void TweenUpdate(float percent)
        {
            if (percent == 0)
                lerped = start;

            percent = 1 - percent;

            Vector4 amount = intensity * Ease(percent);
            amount.x = Random.Range(-amount.x, amount.x);
            amount.y = Random.Range(-amount.y, amount.y);
            amount.z = Random.Range(-amount.z, amount.z);
            amount.w = Random.Range(-amount.w, amount.w);

            lerped = amount + start;
        }
    }
}