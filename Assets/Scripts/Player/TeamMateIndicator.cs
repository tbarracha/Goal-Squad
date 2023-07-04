using StardropTools;
using StardropTools.Tween;
using UnityEngine;

public class TeamMateIndicator : BaseObject
{
    [SerializeField] new SpriteRenderer renderer;
    [SerializeField] TweenSpriteOpacityComponent tweenSpriteOpacity;

    public void IndicateMate(Transform parent)
    {
        if (renderer.enabled == false)
            renderer.enabled = true;

        SetParent(parent);
        SetLocalPosition(Vector3.zero);

        tweenSpriteOpacity.StartTween();
    }

    public void Hide()
    {
        renderer.enabled = false;
    }
}