
using UnityEngine;

namespace StardropTools.UI
{
    [System.Serializable]
    public class UIFlexItem
    {
        [SerializeField] string itemName;
        [SerializeField] RectTransform rectTransform;
        [Range(0, 1)] [SerializeField] float percent;

        public RectTransform RectTransform => rectTransform;
        public float Percent { get => percent; set => percent = value; }

        public UIFlexItem(RectTransform rectTransform, float percent)
        {
            this.rectTransform = rectTransform;
            this.percent = percent;

            itemName = rectTransform.name;
        }


        public void SetSize(Vector2 size) => rectTransform.sizeDelta = size;

        public void SetSize(float x, float y) => rectTransform.sizeDelta = new Vector2(x, y);



        public void SetRectSize(Vector2 size)
        {
            Rect rect = rectTransform.rect;
            rect.size = size;
        }

        public void SetRectSize(float x, float y)
        {
            Rect rect = rectTransform.rect;
            rect.size = new Vector2(x, y);
        }
    }
}