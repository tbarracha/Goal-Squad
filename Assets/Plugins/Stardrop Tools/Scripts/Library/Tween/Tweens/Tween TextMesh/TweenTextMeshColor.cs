
using UnityEngine;
using TMPro;

namespace StardropTools.Tween
{
    public class TweenTextMeshColor : TweenColor
    {
        public TextMeshProUGUI textMesh;

        protected override void SetEssentials()
        {
            //tweenID = textMesh.GetHashCode();
            tweenType = TweenType.TextMeshColor;
        }

        public TweenTextMeshColor(TextMeshProUGUI textMesh, Color start, Color end)
        {
            this.textMesh = textMesh;
            this.start = start;
            this.end = end;

            SetEssentials();
        }

        public TweenTextMeshColor(TextMeshProUGUI textMesh, Color end)
        {
            this.textMesh = textMesh;
            start = textMesh.color;
            this.end = end;

            SetEssentials();
        }

        protected override void TweenUpdate(float percent)
        {
            base.TweenUpdate(percent);

            if (textMesh == null)
                ChangeState(TweenState.Complete);

            textMesh.color = lerped;
        }
    }
}