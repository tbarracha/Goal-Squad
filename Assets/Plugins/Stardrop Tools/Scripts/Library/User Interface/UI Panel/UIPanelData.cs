
using UnityEngine;

namespace StardropTools.UI
{
    [System.Serializable]
    public struct UIPanelData
    {
        [Header("Pivot & Anchor")]
        public UIPanelType panelType;
        public UIPivot uiPivot;
        public UIAnchor uiAnchor;
        public bool validate;

        [Header("Stretch")]
        public float minStretch;

        public UIPanelData(UIPanelType panelType, UIPivot uiPivot, UIAnchor uiAnchor)
        {
            this.panelType = panelType;
            this.uiPivot = uiPivot;
            this.uiAnchor = uiAnchor;
            validate = false;

            minStretch = 100;
        }
    }
}