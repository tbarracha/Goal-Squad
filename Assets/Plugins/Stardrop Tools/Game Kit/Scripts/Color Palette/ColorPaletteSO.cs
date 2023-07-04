using UnityEngine;

[CreateAssetMenu(menuName = "Stardrop / Color / Color Palette")]
public class ColorPaletteSO : ScriptableObject
{
    [SerializeField] Color4[] colors;

    public int ColorCount => colors.Length;

    public Color4 GetColor4(int index) => colors[index];

    /// <summary>
    /// Shade type: 0-highlight, 1-base color, 2-shade, 3-dark shade
    /// </summary>
    /// <param name="color4Index"> Color index is the index of a Color4 in the array </param>
    /// <param name="shadeType"> Shade Type is the index of a color inside a Color4 array </param>
    /// <returns></returns>
    public Color GetColorType(int color4Index, int shadeType)
    {
        Color4 color4 = colors[color4Index];

        switch (shadeType)
        {
            case 0:
                return color4.highlight;

            case 1:
                return color4.color;

            case 2:
                return color4.shade;

            case 3:
                return color4.shadeDark;

            default:
                return Color.black;
        }
    }

    private void OnValidate()
    {
        if (colors.Length > 0)
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i].SetMaxAlpha();
                colors[i].GenerateGradients();
            }
    }
}

[System.Serializable]
public class Color4
{
    public string nameAndDescription = "Insert Description";
    [Space]
    public Color highlight;
    public Color color;
    public Color shade;
    public Color shadeDark;
    [Space]
    public Gradient highlightColorGradient;
    public Gradient colorShadeGradient;
    public Gradient shadeDarkGradient;
    public Gradient color4Gradient;

    public void GenerateGradients()
    {
        var colorKeys = new GradientColorKey[2];


        // Highlight to Color 
        highlightColorGradient = new Gradient();

        colorKeys[0].color = highlight;
        colorKeys[0].time = 0;

        colorKeys[1].color = color;
        colorKeys[1].time = 1;

        highlightColorGradient.colorKeys = colorKeys;


        // Color to Shade
        colorShadeGradient = new Gradient();

        colorKeys[0].color = color;
        colorKeys[0].time = 0;

        colorKeys[1].color = shade;
        colorKeys[1].time = 1;

        colorShadeGradient.colorKeys = colorKeys;


        // Shade to Dark Shade
        shadeDarkGradient = new Gradient();

        colorKeys[0].color = shade;
        colorKeys[0].time = 0;

        colorKeys[1].color = shadeDark;
        colorKeys[1].time = 1;

        shadeDarkGradient.colorKeys = colorKeys;


        // Highlight to Dark Shade
        color4Gradient = new Gradient();
        colorKeys = new GradientColorKey[4];

        colorKeys[0].color = highlight;
        colorKeys[0].time = 0;

        colorKeys[1].color = color;
        colorKeys[1].time = .25f;

        colorKeys[2].color = shade;
        colorKeys[2].time = .5f;

        colorKeys[3].color = shadeDark;
        colorKeys[3].time = 1;

        color4Gradient.colorKeys = colorKeys;
    }

    public void SetMaxAlpha()
    {
        highlight.a = 1;
        color.a = 1;
        shade.a = 1;
        shadeDark.a = 1;
    }
}