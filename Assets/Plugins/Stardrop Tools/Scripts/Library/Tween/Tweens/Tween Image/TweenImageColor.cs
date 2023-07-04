
using UnityEngine;
using UnityEngine.UI;

namespace StardropTools.Tween
{
    public class TweenImageColor : TweenColor
    {
        public Image image;

        protected override void SetEssentials()
        {
            //tweenID = image.GetHashCode();
            tweenType = TweenType.ImageColor;
        }

        public TweenImageColor(Image image, Color start, Color end)
        {
            this.image = image;
            this.start = start;
            this.end = end;

            SetEssentials();
        }

        public TweenImageColor(Image image, Color end)
        {
            this.image = image;
            start = image.color;
            this.end = end;

            SetEssentials();
        }

        protected override void TweenUpdate(float percent)
        {
            base.TweenUpdate(percent);

            if (image == null)
                ChangeState(TweenState.Canceled);

            image.color = lerped;
        }
    }
}