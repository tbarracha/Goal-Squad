
using UnityEngine;
using StardropTools.Tween;

namespace StardropTools.UI
{
    public class UIToggleTextColor : UIToggleButtonComponent
    {
        public TMPro.TextMeshProUGUI textMesh;
        [Tooltip("0-false, 1-true")]
        public Color[] colors = { Color.gray, Color.white };
        [Space]
        public EaseType easeType = EaseType.EaseOutSine;
        public float duration = .2f;

        Tween.Tween tween;

        public override void Toggle(bool value)
        {
            int index = Utilities.ConvertBoolToInt(value);

            if (duration > 0)
            {
                if (tween != null)
                    tween.Stop();

                tween = new TweenTextMeshColor(textMesh, colors[index])
                    .SetEaseType(easeType)
                    .SetDuration(duration)
                    .SetID(textMesh.GetHashCode())
                    .Initialize();
            }

            else
                textMesh.color = colors[index];
        }
    }
}