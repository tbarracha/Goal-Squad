using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.UI
{
    public class UIRootCanvas : BaseUIObject
    {
        [Header("Root Canvas")]
        [SerializeField] List<RectTransform> rectsToCopy;
        [SerializeField] bool getRects;
        [SerializeField] bool copy;

        public override void Initialize()
        {
            base.Initialize();

            CopySizes();
        }

        public void AddRectToCopy(RectTransform rectTransform, bool refreshCopy = false)
        {
            if (rectsToCopy.Contains(rectTransform) == false)
                rectsToCopy.Add(rectTransform);

            if (refreshCopy)
                CopySizes();
        }

        public void RemoveRectToCopy(RectTransform rectTransform)
        {
            if (rectsToCopy.Contains(rectTransform) == true)
                rectsToCopy.Remove(rectTransform);
        }

        public void CopySizes() => UtilitiesUI.CopySizeDeltas(RectTransform, rectsToCopy.ToArray());



        protected override void OnValidate()
        {
            base.OnValidate();

            if (getRects)
            {
                rectsToCopy = Utilities.GetListComponentsInChildren<RectTransform>(RectTransform);
                getRects = false;
            }

            if (copy)
            {
                CopySizes();
                copy = false;
            }
        }
    }
}