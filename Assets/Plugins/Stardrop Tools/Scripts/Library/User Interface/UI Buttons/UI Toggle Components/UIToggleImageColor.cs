
using UnityEngine;
using StardropTools.Tween;

namespace StardropTools.UI
{
    public class UIToggleImageColor : UIToggleButtonComponent
    {
        public UnityEngine.UI.Image image;
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

                tween = new TweenImageColor(image, colors[index])
                    .SetEaseType(easeType)
                    .SetDuration(duration)
                    .SetID(image.GetHashCode())
                    .Initialize();
            }

            else
                image.color = colors[index];
        }
    }
}