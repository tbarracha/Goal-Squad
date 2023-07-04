
using UnityEngine;

namespace StardropTools.UI
{
    public class BaseUIObject : BaseObject
    {
        [SerializeField] RectTransform rectTransform;
        public RectTransform RectTransform => rectTransform;


        #region Rect & RectTransform

        public Vector2 AnchoredPosition { get => rectTransform.anchoredPosition; set => rectTransform.anchoredPosition = value; }
        public Vector2 SizeDelta { get => rectTransform.sizeDelta; set => rectTransform.sizeDelta = value; }

        public float WidthDelta { get => SizeDelta.x; set => SetWidthDelta(value); }
        public float HeightDelta { get => SizeDelta.y; set => SetHeightDelta(value); }


        public Rect Rect { get => rectTransform.rect; }
        public Vector2 SizeRect
        {
            get => rectTransform.rect.size;
            set
            {
                Rect rect = rectTransform.rect;
                rect.size = value;
            }
        }

        public float WidthRect { get => SizeRect.x; set => SetWidthRect(value); }
        public float HeightRect { get => SizeRect.x; set => SetHeightRect(value); }


        public void SetWidthDelta(float width) => SizeDelta = UtilsVector.SetVectorX(SizeDelta, width);
        public void SetHeightDelta(float height) => SizeDelta = UtilsVector.SetVectorY(SizeDelta, height);

        public void SetWidthRect(float width) => SizeRect = UtilsVector.SetVectorX(SizeRect, width);
        public void SetHeightRect(float height) => SizeRect = UtilsVector.SetVectorY(SizeRect, height);
        #endregion // Rect

        protected override void OnValidate()
        {
            base.OnValidate();

            if (rectTransform == null)
                rectTransform = GetComponent<RectTransform>();
        }

        public void SetAnchoredPosition(Vector2 anchoredPosition) => AnchoredPosition = anchoredPosition;
        public void SetAnchoredPosition(float x, float y) => AnchoredPosition = new Vector2(x, y);

        // Set Pivot & Anchor
        public void SetPivot(UIPivot uiPivot) => UtilitiesUI.SetRectTransformPivot(RectTransform, uiPivot);
        public void SetAnchor(UIAnchor uiAnchor) => UtilitiesUI.SetRectTransformAnchor(RectTransform, uiAnchor);


        // Copy Sizes
        public void CopySizeRect(RectTransform rectTransform) => SizeRect = rectTransform.rect.size;
        public void CopySizeDelta(RectTransform rectTransform) => SizeDelta = rectTransform.sizeDelta;
    }
}