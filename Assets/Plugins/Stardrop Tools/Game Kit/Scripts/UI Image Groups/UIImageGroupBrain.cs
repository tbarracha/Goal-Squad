
using UnityEngine;

namespace StardropTools
{
    [RequireComponent(typeof(UIImageGroupColor), typeof(UIImageGroupPixelsPerUnit))]
    public class UIImageGroupBrain : MonoBehaviour
    {
        [Header("Controls")]
        [SerializeField] int colorIndex = 0;
        [SerializeField] float pixelsPerUnit = 1;

        [Header("Components")]
        [SerializeField] UIImageGroupColor groupColor;
        [SerializeField] UIImageGroupPixelsPerUnit groupPixels;

        private void OnValidate()
        {
            if (groupColor == null)
                groupColor = GetComponent<UIImageGroupColor>();

            if (groupPixels == null)
                groupPixels = GetComponent<UIImageGroupPixelsPerUnit>();

            groupColor.SetColor4(colorIndex, true);
            groupPixels.SetPixelsPerUnit(pixelsPerUnit);
        }
    }
}