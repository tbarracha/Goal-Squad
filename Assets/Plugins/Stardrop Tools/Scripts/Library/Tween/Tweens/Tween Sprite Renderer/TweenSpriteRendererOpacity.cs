
using UnityEngine;
using UnityEngine.UI;

namespace StardropTools.Tween
{
    public class TweenSpriteRendererOpacity : TweenColorOpacity
    {
        public SpriteRenderer renderer;

        protected override void SetEssentials()
        {
            //tweenID = renderer.GetHashCode();
            tweenType = TweenType.SpriteRendererOpacity;
        }

        public TweenSpriteRendererOpacity(SpriteRenderer renderer, float start, float end)
            : base (renderer.color, start, end)
        {
            this.renderer = renderer;
            this.start = start;
            this.end = end;

            SetEssentials();
        }

        public TweenSpriteRendererOpacity(SpriteRenderer renderer, float end)
            : base (renderer.color, end)
        {
            this.renderer = renderer;
            start = renderer.color.a;
            this.end = end;

            SetEssentials();
        }

        protected override void TweenUpdate(float percent)
        {
            base.TweenUpdate(percent);

            if (renderer == null)
                ChangeState(TweenState.Canceled);

            renderer.color = color;
        }
    }
}