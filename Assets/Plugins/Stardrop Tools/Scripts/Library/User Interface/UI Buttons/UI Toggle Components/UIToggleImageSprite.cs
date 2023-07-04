
using UnityEngine;

namespace StardropTools.UI
{
    public class UIToggleImageSprite : UIToggleButtonComponent
    {
        public UnityEngine.UI.Image image;
        [Tooltip("0-false, 1-true")]
        public Sprite[] sprites;

        public override void Toggle(bool value)
        {
            int index = Utilities.ConvertBoolToInt(value);
            image.sprite = sprites[index];
        }
    }
}