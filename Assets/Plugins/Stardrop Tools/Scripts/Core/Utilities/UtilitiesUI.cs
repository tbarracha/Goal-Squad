
using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StardropTools.UI;
using StardropTools.Tween;

public static class UtilitiesUI
{
    #region UI Pivot

    // Top
    public static readonly Vector2 PivotTopLeft = new Vector2(0, 1);
    public static readonly Vector2 PivotTopCenter = new Vector2(.5f, 1);
    public static readonly Vector2 PivotTopRight = new Vector2(1, 1);

    // Center
    public static readonly Vector2 PivotCenterLeft = new Vector2(0, .5f);
    public static readonly Vector2 PivotCenter = new Vector2(.5f, .5f);
    public static readonly Vector2 PivotCenterRight = new Vector2(1, .5f);

    // Bottom
    public static readonly Vector2 PivotBottomLeft = new Vector2(0, 0);
    public static readonly Vector2 PivotBottomCenter = new Vector2(.5f, 0);
    public static readonly Vector2 PivotBottomRight = new Vector2(1, 0);

    /// <summary>
    /// Returns Vector2 pivot based on UIPivot enum
    /// </summary>
    public static Vector2 SetRectTransformPivot(RectTransform rectTransform, UIPivot pivot)
    {
        switch (pivot)
        {
            // Top
            case UIPivot.TopLeft:
                rectTransform.pivot = PivotTopLeft;
                return PivotTopLeft;

            case UIPivot.TopCenter:
                rectTransform.pivot = PivotTopCenter;
                return PivotTopCenter;

            case UIPivot.TopRight:
                rectTransform.pivot = PivotTopRight;
                return PivotTopRight;


            // Center
            case UIPivot.CenterLeft:
                rectTransform.pivot = PivotCenterLeft;
                return PivotCenterLeft;

            case UIPivot.Center:
                rectTransform.pivot = PivotCenter;
                return PivotCenter;

            case UIPivot.CenterRight:
                rectTransform.pivot = PivotCenterRight;
                return PivotCenterRight;


            // Bottom
            case UIPivot.BottomLeft:
                rectTransform.pivot = PivotBottomLeft;
                return PivotBottomLeft;

            case UIPivot.BottomCenter:
                rectTransform.pivot = PivotBottomCenter;
                return PivotBottomCenter;

            case UIPivot.BottomRight:
                rectTransform.pivot = PivotBottomRight;
                return PivotBottomRight;

            default:
                return PivotCenter;
        }
    }


    /// <summary>
    /// Returns UIPivot enum, based on Vector2
    /// </summary>
    public static UIPivot GetUIPivot(Vector2 pivot)
    {
        // Top
        if (pivot == PivotTopLeft)
            return UIPivot.TopLeft;
        else if (pivot == PivotTopCenter)
            return UIPivot.TopCenter;
        else if (pivot == PivotTopRight)
            return UIPivot.TopRight;

        // Center
        if (pivot == PivotCenterLeft)
            return UIPivot.CenterLeft;
        else if (pivot == PivotCenter)
            return UIPivot.Center;
        else if (pivot == PivotCenterRight)
            return UIPivot.CenterRight;

        // Bottom
        if (pivot == PivotBottomLeft)
            return UIPivot.BottomLeft;
        else if (pivot == PivotBottomCenter)
            return UIPivot.BottomCenter;
        else if (pivot == PivotBottomRight)
            return UIPivot.BottomRight;

        else
            return UIPivot.Center;
    }

    /// <summary>
    /// Returns UIPivot enum, based on RectTransforms Pivot Vector2
    /// </summary>
    public static UIPivot GetUIPivot(RectTransform rectTransform)
        => GetUIPivot(rectTransform.pivot);

    #endregion // Pivot

    #region UI Anchor
    public struct AnchorMinMax
    {
        public Vector2 min;
        public Vector2 max;

        public AnchorMinMax(Vector2 min, Vector2 max)
        {
            this.min = min;
            this.max = max;
        }
    }


    // Top
    public static readonly AnchorMinMax AnchorTopLeft = new AnchorMinMax(new Vector2(0, 1), new Vector2(0, 1));
    public static readonly AnchorMinMax AnchorTopCenter = new AnchorMinMax(new Vector2(.5f, 1), new Vector2(.5f, 1));
    public static readonly AnchorMinMax AnchorTopRight = new AnchorMinMax(new Vector2(1, 1), new Vector2(1, 1));

    // Center
    public static readonly AnchorMinMax AnchorCenterLeft = new AnchorMinMax(new Vector2(0, .5f), new Vector2(0, .5f));
    public static readonly AnchorMinMax AnchorCenter = new AnchorMinMax(new Vector2(.5f, .5f), new Vector2(.5f, .5f));
    public static readonly AnchorMinMax AnchorCenterRight = new AnchorMinMax(new Vector2(1, .5f), new Vector2(1, .5f));

    // Bottom
    public static readonly AnchorMinMax AnchorBottomLeft = new AnchorMinMax(new Vector2(0, .5f), new Vector2(0, 0));
    public static readonly AnchorMinMax AnchorBottomCenter = new AnchorMinMax(new Vector2(.5f, .5f), new Vector2(.5f, 0));
    public static readonly AnchorMinMax AnchorBottomRight = new AnchorMinMax(new Vector2(1, .5f), new Vector2(1, 0));

    // Stretch
    public static readonly AnchorMinMax StretchLeft = new AnchorMinMax(new Vector2(0, 0), new Vector2(0, 1));
    public static readonly AnchorMinMax StretchTop = new AnchorMinMax(new Vector2(0, 1), new Vector2(1, 1));
    public static readonly AnchorMinMax StretchRight = new AnchorMinMax(new Vector2(1, 0), new Vector2(1, 1));
    public static readonly AnchorMinMax StretchBottom = new AnchorMinMax(new Vector2(0, 0), new Vector2(1, 0));
    public static readonly AnchorMinMax StretchFull = new AnchorMinMax(new Vector2(0, 0), new Vector2(1, 1));

    public static void SetRectAnchorMinMax(RectTransform rectTransform, AnchorMinMax minMax)
    {
        rectTransform.anchorMin = minMax.min;
        rectTransform.anchorMax = minMax.max;
    }

    public static bool CompareAnchors(this AnchorMinMax anchorA, AnchorMinMax anchorB)
    {
        if (anchorA.min == anchorB.min && anchorA.max == anchorB.max)
            return true;
        else
            return false;
    }


    public static void SetRectTransformAnchor(RectTransform rectTransform, UIAnchor anchor)
    {
        switch (anchor)
        {
            // Top
            case UIAnchor.TopLeft:
                SetRectAnchorMinMax(rectTransform, AnchorTopLeft);
                break;

            case UIAnchor.TopCenter:
                SetRectAnchorMinMax(rectTransform, AnchorTopCenter);
                break;

            case UIAnchor.TopRight:
                SetRectAnchorMinMax(rectTransform, AnchorTopRight);
                break;


            // Center
            case UIAnchor.CenterLeft:
                SetRectAnchorMinMax(rectTransform, AnchorCenterRight);
                break;

            case UIAnchor.Center:
                SetRectAnchorMinMax(rectTransform, AnchorCenter);
                break;

            case UIAnchor.CenterRight:
                SetRectAnchorMinMax(rectTransform, AnchorCenterRight);
                break;


            // Bottom
            case UIAnchor.BottomLeft:
                SetRectAnchorMinMax(rectTransform, AnchorBottomLeft);
                break;

            case UIAnchor.BottomCenter:
                SetRectAnchorMinMax(rectTransform, AnchorBottomCenter);
                break;

            case UIAnchor.BottomRight:
                SetRectAnchorMinMax(rectTransform, AnchorBottomRight);
                break;


            // Stretch
            case UIAnchor.StretchLeft:
                SetRectAnchorMinMax(rectTransform, StretchLeft);
                break;

            case UIAnchor.StretchTop:
                SetRectAnchorMinMax(rectTransform, StretchTop);
                break;

            case UIAnchor.StretchRight:
                SetRectAnchorMinMax(rectTransform, StretchRight);
                break;

            case UIAnchor.StretchBottom:
                SetRectAnchorMinMax(rectTransform, StretchBottom);
                break;

            case UIAnchor.StretchAll:
                SetRectAnchorMinMax(rectTransform, StretchFull);
                break;
        }
    }

    public static UIAnchor GetUIAnchor(RectTransform rectTransform)
    {
        AnchorMinMax minMax = new AnchorMinMax(rectTransform.anchorMin, rectTransform.anchorMax);

        // Top
        if (CompareAnchors(minMax, AnchorTopLeft))
            return UIAnchor.TopLeft;
        if (CompareAnchors(minMax, AnchorTopCenter))
            return UIAnchor.TopCenter;
        if (CompareAnchors(minMax, AnchorTopRight))
            return UIAnchor.TopRight;

        // Center
        if (CompareAnchors(minMax, AnchorCenterLeft))
            return UIAnchor.CenterLeft;
        if (CompareAnchors(minMax, AnchorCenter))
            return UIAnchor.Center;
        if (CompareAnchors(minMax, AnchorCenterRight))
            return UIAnchor.CenterRight;

        // Bottom
        if (CompareAnchors(minMax, AnchorBottomLeft))
            return UIAnchor.BottomLeft;
        if (CompareAnchors(minMax, AnchorBottomCenter))
            return UIAnchor.BottomCenter;
        if (CompareAnchors(minMax, AnchorBottomRight))
            return UIAnchor.BottomRight;

        // Stretch
        if (CompareAnchors(minMax, StretchLeft))
            return UIAnchor.StretchLeft;
        if (CompareAnchors(minMax, StretchTop))
            return UIAnchor.StretchTop;
        if (CompareAnchors(minMax, StretchRight))
            return UIAnchor.StretchRight;
        if (CompareAnchors(minMax, StretchBottom))
            return UIAnchor.StretchBottom;
        if (CompareAnchors(minMax, StretchFull))
            return UIAnchor.StretchAll;

        return UIAnchor.StretchAll;
    }
    #endregion // UI Anchor

    public static void CopySizeRect(RectTransform reference, RectTransform target)
    {
        Vector2 sizeRect = reference.rect.size;
        Rect rect = target.rect;
        rect.size = sizeRect;
    }

    public static void CopySizeDelta(RectTransform reference, RectTransform target)
        => target.sizeDelta = reference.sizeDelta;

    public static void CopySizeRects(RectTransform reference, RectTransform[] rects)
    {
        if (rects.Length == 0)
        {
            Debug.Log("Not enough RectTransforms to copy!");
            return;
        }

        Rect rect;
        Vector2 sizeRect = reference.rect.size;

        for (int i = 0; i < rects.Length; i++)
        {
            rect = rects[i].rect;
            rect.size = sizeRect;
        }
    }

    public static void CopySizeRects(RectTransform reference, List<RectTransform> rects)
        => CopySizeRects(reference, rects.ToArray());

    public static void CopySizeDeltas(RectTransform reference, RectTransform[] rects)
    {
        if (rects.Length == 0)
        {
            Debug.Log("Not enough RectTransforms to copy!");
            return;
        }

        Vector2 sizeDelta = reference.sizeDelta;

        for (int i = 0; i < rects.Length; i++)
            rects[i].sizeDelta = sizeDelta;
    }

    public static void CopySizeDeltas(RectTransform reference, List<RectTransform> rects)
        => CopySizeDeltas(reference, rects.ToArray());

    #region Image

    public static Image SetImagePixelsPerUnitMultiplier(Image img, float pixels)
    {
        img.pixelsPerUnitMultiplier = pixels;
        return img;
    }

    public static void SetImagesPixelsPerUnitMultiplier(Image[] images, float pixels)
    {
        for (int i = 0; i < images.Length; i++)
            SetImagePixelsPerUnitMultiplier(images[i], pixels);
    }

    public static void SetImageOpacity(Image img, float opacity)
    {
        Color color = img.color;
        color.a = opacity;
        img.color = color;
    }

    public static void SetImagesOpacity(Image[] images, float opacity)
    {
        for (int i = 0; i < images.Length; i++)
            SetImageOpacity(images[i], opacity);
    }

    public static void SetTextMeshOpacity(TextMeshProUGUI textMesh, float opacity)
    {
        Color color = textMesh.color;
        color.a = opacity;
        textMesh.color = color;
    }

    public static void SetTextMeshesOpacity(TextMeshProUGUI[] textMeshes, float opacity)
    {
        for (int i = 0; i < textMeshes.Length; i++)
            SetTextMeshOpacity(textMeshes[i], opacity);
    }

    #endregion // image
}