
using UnityEngine;

namespace StardropTools.Tween
{
    public abstract class TweenRectTransformComponent : TweenComponent
    {
        [Header("Target Rect")]
        [SerializeField] protected RectTransform target;
    }
}