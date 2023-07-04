
using UnityEngine;
using UnityEngine.UI;

namespace StardropTools
{
    public class UIImageGroupColor : MonoBehaviour
    {
        public ColorPaletteSO palette;
        public Color4 color4;
        public int colorIndex = 0;

        [Header("Image ")]
        public Image[] highlightImages;
        public Image[] baseImages;
        public Image[] shadedImages;
        public Image[] darkShadedImages;

        public void SetColor4(Color4 color, bool refresh)
        {
            color4 = color;
            if (refresh)
                RefreshGraphics();
        }

        public void SetColor4(int index, bool refresh)
        {
            colorIndex = Mathf.Clamp(index, 0, palette.ColorCount - 1);
            SetColor4(palette.GetColor4(colorIndex), refresh);
        }


        public void RefreshGraphics()
        {
            if (highlightImages.Exists())
                Utilities.SetImageArrayColor(highlightImages, color4.highlight);

            if (baseImages.Exists())
                Utilities.SetImageArrayColor(baseImages, color4.color);

            if (shadedImages.Exists())
                Utilities.SetImageArrayColor(shadedImages, color4.shade);

            if (darkShadedImages.Exists())
                Utilities.SetImageArrayColor(darkShadedImages, color4.shadeDark);
        }

        private void OnValidate()
        {
            colorIndex = Mathf.Clamp(colorIndex, 0, palette.ColorCount - 1);

            SetColor4(colorIndex, true);
        }
    }
}

