
using UnityEngine;
using StardropTools.Tween;

namespace StardropTools.UI
{
    public class UIToggleScale : UIToggleButtonComponent
    {
        public RectTransform rectTransform;
        [Tooltip("0-false, 1-true")]
        public float[] scales = { 1, 1.5f };
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

                tween = new TweenLocalScale(rectTransform, Vector3.one * scales[index])
                    .SetEaseType(easeType)
                    .SetDuration(duration)
                    .SetID(rectTransform.GetHashCode())
                    .Initialize();
            }

            else
                rectTransform.localScale = Vector3.one * scales[index];
        }
    }
}