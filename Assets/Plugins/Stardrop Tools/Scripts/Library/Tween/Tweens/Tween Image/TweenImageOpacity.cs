
using UnityEngine;
using UnityEngine.UI;

namespace StardropTools.Tween
{
    public class TweenImageOpacity : TweenColorOpacity
    {
        public Image image;

        protected override void SetEssentials()
        {
            //tweenID = image.GetInstanceID();
            tweenType = TweenType.ImageOpacity;
        }

        public TweenImageOpacity(Image image, float start, float end)
            : base (image.color, start, end)
        {
            this.image = image;
            this.start = start;
            this.end = end;

            SetEssentials();
        }

        public TweenImageOpacity(Image image, float end)
            : base (image.color, end)
        {
            this.image = image;
            start = image.color.a;
            this.end = end;

            SetEssentials();
        }

        protected override void TweenUpdate(float percent)
        {
            base.TweenUpdate(percent);

            if (image == null)
                ChangeState(TweenState.Canceled);

            image.color = color;
        }
    }
}