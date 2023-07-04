
using UnityEngine;
using TMPro;

namespace StardropTools.Tween
{
    public class TweenTextMeshSize : TweenFloat
    {
        public TextMeshProUGUI textMesh;

        protected override void SetEssentials()
        {
            //tweenID = textMesh.GetHashCode();
            tweenType = TweenType.TextMeshSize;
        }

        public TweenTextMeshSize(TextMeshProUGUI textMesh, float start, float end)
        {
            this.textMesh = textMesh;
            this.start = start;
            this.end = end;

            SetEssentials();
        }

        public TweenTextMeshSize(TextMeshProUGUI textMesh, float end)
        {
            this.textMesh = textMesh;
            start = textMesh.fontSize;
            this.end = end;

            SetEssentials();
        }

        protected override void TweenUpdate(float percent)
        {
            base.TweenUpdate(percent);

            if (textMesh == null)
                ChangeState(TweenState.Canceled);

            textMesh.fontSize = lerped;
        }
    }
}