
using UnityEngine;
using UnityEngine.UI;

public class ButtonCustomizer : MonoBehaviour
{
    public enum GradientType { None, darkFromTop, darkFromBottom }

    [Header("Main Properties")]
    [SerializeField] Button button;
    [SerializeField] RectTransform customButtonRect;
    [SerializeField] ColorPaletteSO colorPalette;
    [SerializeField] Color4 color4;
    [Space]
    [Range(0, 11)] [SerializeField] int colorIndex;
    [SerializeField] float pixelsPerUnit = 1;
    [SerializeField] Vector2 size = new Vector2(128, 128);
    [Space]
    [SerializeField] float gradientTextureSidePadding = 16;
    [SerializeField] bool highlightActive = true;
    [SerializeField] bool gradientTextureActive = false;

    [Header("Gradients")]
    [SerializeField] GradientType gradientTextureType; 
    [SerializeField] GameObject gradient;
    [SerializeField] GameObject gradientTexture;
    [SerializeField] GameObject highlightImage;
    [SerializeField] RectTransform rectGradientTexture;

    [Header("Image Arrays")]
    [SerializeField] Image[] pixelPerUnitImages;
    [SerializeField] Image[] highlightImages;
    [SerializeField] Image[] colorImages;
    [SerializeField] Image[] shadeImages;
    [SerializeField] Image[] shadeDarkImages;

    public void SetSize(Vector2 size)
    {
        this.size = size;
        customButtonRect.sizeDelta = size;
    }

    public Color4 GetColor4(int index)
    {
        colorIndex = index;
        color4 = colorPalette.GetColor4(index);

        return color4;
    }

    public void SetColor4(Color4 color, bool refresh)
    {
        color4 = color;

        if (refresh)
            RefreshGraphics();
    }

    public void SetColor4(int index, bool refresh) => SetColor4(colorPalette.GetColor4(index), refresh);


    public void RefreshGraphics()
    {
        if (highlightImages.Exists())
            Utilities.SetImageArrayColor(highlightImages, color4.highlight);

        if (colorImages.Exists())
            Utilities.SetImageArrayColor(colorImages, color4.color);

        if (shadeImages.Exists())
            Utilities.SetImageArrayColor(shadeImages, color4.shade);

        if (shadeDarkImages.Exists())
            Utilities.SetImageArrayColor(shadeDarkImages, color4.shadeDark);

        switch (gradientTextureType)
        {
            case GradientType.None:
                gradient.gameObject.SetActive(false);
                break;

            case GradientType.darkFromTop:
                gradient.gameObject.SetActive(true);
                gradient.transform.rotation = Quaternion.identity;
                break;

            case GradientType.darkFromBottom:
                gradient.gameObject.SetActive(true);
                gradient.transform.rotation = Quaternion.Euler(Vector3.forward * 180);
                break;
        }

        highlightImage.SetActive(highlightActive);
        gradientTexture.SetActive(gradientTextureActive);
        rectGradientTexture.sizeDelta = new Vector2(-gradientTextureSidePadding, customButtonRect.sizeDelta.y * .5f);
    }


    private void OnValidate()
    {
        SetSize(size);

        GetColor4(colorIndex);
        RefreshGraphics();

        if (pixelPerUnitImages.Exists())
            Utilities.SetImagePixelsPerUnit(pixelPerUnitImages, pixelsPerUnit);
    }
}
