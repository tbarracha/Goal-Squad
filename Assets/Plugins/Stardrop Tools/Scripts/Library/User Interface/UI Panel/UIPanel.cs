
using UnityEngine;

namespace StardropTools.UI
{
    public class UIPanel : BaseUIObject
    {
        [Header("Panel")]
        [SerializeField] UIPanelData panelData;

        public UIPanelType PanelType { get => panelData.panelType; set => panelData.panelType = value; }
        public UIPivot UIPivot { get => panelData.uiPivot; set => panelData.uiPivot = value; }
        public UIAnchor UIAnchor { get => panelData.uiAnchor; set => panelData.uiAnchor = value; }


        public void SetPanelType(UIPanelType panelType, bool ignoreSame = false)
        {
            if (panelType == UIPanelType.Custom)
                return;

            if (ignoreSame == false && panelType == PanelType)
                return;


            if (panelType == UIPanelType.Left)
            {
                UIPivot = UIPivot.CenterLeft;
                UIAnchor = UIAnchor.StretchLeft;
            }

            else if (panelType == UIPanelType.Top)
            {
                UIPivot = UIPivot.TopCenter;
                UIAnchor = UIAnchor.StretchTop;
            }

            else if (panelType == UIPanelType.Right)
            {
                UIPivot = UIPivot.CenterRight;
                UIAnchor = UIAnchor.StretchRight;
            }

            else if (panelType == UIPanelType.Bottom)
            {
                UIPivot = UIPivot.BottomCenter;
                UIAnchor = UIAnchor.StretchBottom;
            }

            else if (panelType == UIPanelType.Center)
            {
                UIPivot = UIPivot.Center;
                UIAnchor = UIAnchor.StretchAll;
            }

            PanelType = panelType;
            RefreshPivotAndAnchor();
            RefreshStretch();
        }

        public void RefreshPivotAndAnchor()
        {
            SetPivot(panelData.uiPivot);
            SetAnchor(panelData.uiAnchor);
        }

        public void RefreshStretch()
        {
            if (UIAnchor == UIAnchor.StretchLeft || UIAnchor == UIAnchor.StretchRight)
            {
                HeightDelta = 0;
                WidthDelta = panelData.minStretch;
            }

            if (UIAnchor == UIAnchor.StretchTop || UIAnchor == UIAnchor.StretchBottom)
            {
                WidthDelta = 0;
                HeightDelta = panelData.minStretch;
            }

            if (UIAnchor == UIAnchor.StretchAll)
                SizeDelta = Vector2.zero;
        }


        protected override void OnValidate()
        {
            base.OnValidate();

            if (panelData.validate)
            {
                if (PanelType != UIPanelType.Custom)
                    SetPanelType(PanelType, true);
                else
                {
                    RefreshPivotAndAnchor();
                    RefreshStretch();
                }
            }
        }
    }
}