
using UnityEngine;
using UnityEngine.UI;

namespace StardropTools
{
    public class UIImageGroupPixelsPerUnit : MonoBehaviour
    {
        public float pixelsPerUnit = 1;
        public Image[] images;

        public void SetPixelsPerUnit(float pixelsPerUnit)
        {
            if (images.Length == 0)
                return;

            this.pixelsPerUnit = pixelsPerUnit;
            Utilities.SetImagePixelsPerUnit(images, pixelsPerUnit);
        }

        private void OnValidate()
        {
            if (images.Length > 0)
                SetPixelsPerUnit(pixelsPerUnit);
        }
    }
}