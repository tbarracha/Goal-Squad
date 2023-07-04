
using StardropTools;
using StardropTools.Tween;
using UnityEngine;

/// <summary>
/// If you don't see any value change, you probably need to change the reference rect
/// </summary>
public class UIHealthBar : BaseComponent
{
    [Header("Health Data Reference")]
    [SerializeField] HealthComponent referenceHealthComponent;
    [NaughtyAttributes.Expandable][SerializeField] HealthContainerSO referenceHealthContainer;

    [Header("UI Components")]
    // If you don't see any value change, change Reference Rect
    [Tooltip("If you don't see any in-game visual change, set a different Reference Rect")]
    [SerializeField] RectTransform containerRectForSizeAnimationReference;

    [Tooltip("0-main rect to animate, 1-delayed rect to animate")]
    [SerializeField] RectTransform[] mainAndDelayedRectsToAnimate;
    [Space]
    [Range(0, 1)][SerializeField] float percent;

    [Header("Graphic Style")]
    [SerializeField] UnityEngine.UI.Image[] imagesToColor;
    [SerializeField] Color mainColor = Color.red;
    [SerializeField] Color secondaryColor = Color.gray;
    [SerializeField] Color backgroundColor = Color.black;
    [SerializeField] Color gradientColor = Color.black;
    [Space]
    [SerializeField] UnityEngine.UI.Image[] pixelPerUnitImagesToUpdate;
    [SerializeField] float pixelsPerUnit = 32f;

    [Header("Animation")]
    [SerializeField] EaseType easeType;
    [SerializeField] float duration = .2f;
    [SerializeField] float delay = .3f;

    [Header("Debug")]
    [Range(0, 1)] [SerializeField] float debugPercent = .5f;
    [SerializeField] bool debugFull;
    [SerializeField] bool debug;

    Tween valueTween, delayedTween, openTween;
    System.Collections.Generic.List<Tween> imageTweens;

    RectTransform valueRect => mainAndDelayedRectsToAnimate[0];
    RectTransform delayedRect => mainAndDelayedRectsToAnimate[1];
    public float Percent => percent;

    public bool IsOpened { get; private set; }

    public readonly EventHandler OnChanged = new EventHandler();
    public readonly EventHandler<float> OnPercentChanged = new EventHandler<float>();

    public override void Initialize()
    {
        base.Initialize();

        if (referenceHealthComponent != null)
            ReferenceDataFetch(referenceHealthComponent, true);

        if (referenceHealthContainer != null)
            ReferenceDataFetch(referenceHealthContainer, true);
    }

    public void ReferenceDataFetch(HealthComponent healthComponent, bool ignoreAnimation)
    {
        ClearHealthListener();

        referenceHealthComponent = healthComponent;
        percent = healthComponent.PercentHealth;
        CalculateDisplay(ignoreAnimation);

        StartHealthListener();
    }

    public void ReferenceDataFetch(HealthContainerSO healthContainer, bool ignoreAnimation)
    {
        ClearHealthListener();

        referenceHealthContainer = healthContainer;
        percent = healthContainer.PercentHealth;
        CalculateDisplay(ignoreAnimation);

        StartHealthListener();
    }


    /// <summary>
    /// Call this if you are certain you have a HealthContainer to listen to
    /// </summary>
    public void StartHealthListener()
    {
        if (referenceHealthComponent != null)
            referenceHealthComponent.OnHealthPercentChanged.AddListener(SetPercent);

        if (referenceHealthContainer != null)
            referenceHealthContainer.OnHealthPercentChanged.AddListener(SetPercent);
    }

    /// <summary>
    /// Call this if you are certain you have a HealthContainer to listen to
    /// </summary>
    public void ClearHealthListener()
    {
        if (referenceHealthComponent != null)
            referenceHealthComponent.OnHealthPercentChanged.RemoveListener(SetPercent);

        if (referenceHealthContainer != null)
            referenceHealthContainer.OnHealthPercentChanged.RemoveListener(SetPercent);
    }


    /// <summary>
    /// Value from 0 to 1
    /// </summary>
    public void SetPercent(float percent)
    {
        this.percent = percent;
        CalculateDisplay();
    }

    /// <summary>
    /// Value from 0 to 1
    /// </summary>
    public void SetPercent(float percent, bool ignoreAnimation)
    {
        this.percent = percent;
        CalculateDisplay(ignoreAnimation);
    }

    public void CalculateDisplay(bool ignoreAnimation = false)
    {
        float refWidth = containerRectForSizeAnimationReference.sizeDelta.x;
        float targetWidth = refWidth * percent;
        Vector2 targetSize = Vector2.right * targetWidth;

        if (ignoreAnimation)
        {
            for (int i = 0; i < mainAndDelayedRectsToAnimate.Length; i++)
                mainAndDelayedRectsToAnimate[i].sizeDelta = targetSize;
        }

        else
        {
            StopTweens();
            
            valueTween = new TweenSizeDelta(valueRect, targetSize)
                        .SetDuration(duration)
                        .SetEaseType(easeType)
                        .SetID(valueRect.GetHashCode())
                        .Initialize();

            delayedTween = new TweenSizeDelta(delayedRect, targetSize)
                        .SetDurationAndDelay(duration, delay)
                        .SetEaseType(easeType)
                        .SetID(delayedRect.GetHashCode())
                        .Initialize();
        }
    }

    void StopTweens()
    {
        if (valueTween != null)
            valueTween.Stop();

        if (delayedTween != null)
            delayedTween.Stop();
    }

    void StopOpenTweens()
    {
        if (openTween != null)
            openTween.Stop();

        if (imageTweens.Exists())
            foreach (Tween tween in imageTweens)
                tween.Stop();
    }

    void ImageTweens(float targetAlpha)
    {
        // create new list
        imageTweens = new System.Collections.Generic.List<Tween>();

        // add tweens to list
        for (int i = 0; i < pixelPerUnitImagesToUpdate.Length; i++)
        {
            Tween tween = new TweenImageOpacity(pixelPerUnitImagesToUpdate[i], targetAlpha)
                .SetDuration(.3f)
                .SetEaseType(EaseType.EaseOutSine)
                .SetID(pixelPerUnitImagesToUpdate[i].GetHashCode());

            imageTweens.Add(tween);
        }

        // initialize tweens
        foreach (Tween tween in imageTweens)
            tween.Initialize();
    }

    public void Open()
    {
        IsOpened = true;
        gameObject.SetActive(true);

        StopOpenTweens();


        openTween = new TweenLocalScale(transform, Vector3.zero, Vector3.one)
            .SetDuration(.3f)
            .SetEaseType(EaseType.EaseOutBack)
            .SetID(GetHashCode())
            .Initialize();

        if (pixelPerUnitImagesToUpdate[0].color.a < 1)
            ImageTweens(1);
    }

    public void Close()
    {
        IsOpened = false;

        StopOpenTweens();
        ImageTweens(0);
        gameObject.SetActive(false);
    }

    private void OnValidate()
    {
        if (debugFull)
        {
            SetPercent(1, true);
            debugFull = false;
        }

        if (Application.isPlaying)
        {
            if (debug)
            {
                SetPercent(debugPercent, false);
                debug = false;
            }
        }

#if UNITY_EDITOR
        if (pixelPerUnitImagesToUpdate.Exists())
            UtilitiesUI.SetImagesPixelsPerUnitMultiplier(pixelPerUnitImagesToUpdate, pixelsPerUnit);

        if (imagesToColor.Exists())
        {
            imagesToColor[0].color = mainColor;
            imagesToColor[1].color = secondaryColor;
            imagesToColor[2].color = backgroundColor;
            imagesToColor[3].color = gradientColor;
        }
#endif
    }
}