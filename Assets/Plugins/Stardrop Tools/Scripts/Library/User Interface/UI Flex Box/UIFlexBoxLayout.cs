
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StardropTools.UI
{
    public class UIFlexBoxLayout : LayoutGroup
    {
        [SerializeField] float spacing;

        [Header("Flex box")]
        [SerializeField] UIOrientation orientation;
        [Space]
        [SerializeField] bool clickToGetFlexItems;
        [SerializeField] UIFlexItem[] flexItems = new UIFlexItem[0];
        [SerializeField] List<RectTransform> rectsToIgnore = new List<RectTransform>();

        bool uniform;

        Vector2 SizeDelta { get => rectTransform.sizeDelta; set => rectTransform.sizeDelta = value; }
        Vector2 SizeRect => rectTransform.rect.size;


        public override void CalculateLayoutInputHorizontal()
        {
            if (childAlignment != TextAnchor.UpperLeft)
                childAlignment = TextAnchor.UpperLeft;

            base.CalculateLayoutInputHorizontal();

            if (clickToGetFlexItems)
            {
                GetFlexItems();
                clickToGetFlexItems = false;
            }

            RefreshFlexItems();
        }

        public void RefreshFlexItems()
        {
            //if (Application.isPlaying)
            //    return;

            if (flexItems.Length == 0)
                return;

            // Get Reference size
            float remainingSize = 0;

            if (orientation == UIOrientation.Horizontal)
                remainingSize = SizeRect.x;

            else if (orientation == UIOrientation.Vertical)
                remainingSize = SizeRect.y;

            // Loop through items and set size
            // They occupy percentage, based on space left
            for (int i = 0; i < flexItems.Length; i++)
            {
                UIFlexItem item = flexItems[i];

                if (uniform)
                    item.Percent = 1f / flexItems.Length;

                float targetSize = remainingSize * item.Percent;
                remainingSize -= targetSize;

                UtilitiesUI.SetRectTransformAnchor(item.RectTransform, UIAnchor.TopLeft);
                UtilitiesUI.SetRectTransformPivot(item.RectTransform, UIPivot.TopLeft);

                UIFlexItem prevItem = flexItems[Mathf.Clamp(i - 1, 0, flexItems.Length - 1)];

                switch (orientation)
                {
                    case UIOrientation.Horizontal:

                        item.SetSize(targetSize - (spacing / rectChildren.Count * 2) - (padding.left / rectChildren.Count) - (padding.right / rectChildren.Count), SizeRect.y - padding.bottom - padding.top);

                        if (i > 0)
                            item.RectTransform.anchoredPosition = new Vector2(prevItem.RectTransform.anchoredPosition.x + prevItem.RectTransform.sizeDelta.x + spacing, -padding.top);
                        else
                            item.RectTransform.anchoredPosition = new Vector2(padding.left, -padding.top);

                        break;


                    case UIOrientation.Vertical:

                        item.SetSize(SizeRect.x - padding.left - padding.right, targetSize - (spacing / rectChildren.Count * 2) - (padding.bottom / rectChildren.Count) - (padding.top / rectChildren.Count));

                        if (i > 0)
                            item.RectTransform.anchoredPosition = new Vector2(padding.left, prevItem.RectTransform.anchoredPosition.y - prevItem.RectTransform.sizeDelta.y - spacing);
                        else
                            item.RectTransform.anchoredPosition = new Vector2(padding.left, -padding.top);

                        break;


                    default:
                        Debug.Log("Flex Orientation CAN'T be set to BOTH");
                        break;
                }
            }
        }

        public void AddRectToIgnore(RectTransform rect)
        {
            rectsToIgnore.Add(rect);
        }

        /// <summary>
        /// Get flex items, ignoring rects inside the RectsToIgnore array
        /// </summary>
        public void GetFlexItems()
        {
            List<UIFlexItem> flexList = new List<UIFlexItem>();
            float percent = 1f / transform.childCount;

            for (int i = 0; i < rectChildren.Count; i++)
            {
                RectTransform rect = rectChildren[i].GetComponent<RectTransform>();

                // ignore and move next
                if (rectsToIgnore.Contains(rect))
                    continue;

                UIFlexItem item = new UIFlexItem(rect, percent);
                UtilitiesUI.SetRectTransformAnchor(item.RectTransform, UIAnchor.TopLeft);
                UtilitiesUI.SetRectTransformPivot(item.RectTransform, UIPivot.TopLeft);

                flexList.Add(item);
            }

            flexItems = flexList.ToArray();
        }


        #region Ignore
        public override void CalculateLayoutInputVertical()
        {

        }

        public override void SetLayoutHorizontal()
        {

        }

        public override void SetLayoutVertical()
        {

        }
        #endregion
    }
}