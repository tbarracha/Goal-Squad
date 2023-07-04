
using UnityEngine;

namespace StardropTools.Tween
{
    public abstract class TweenSpriteComponent : TweenComponent
    {
        [Header("Target Transform")]
        public SpriteRenderer target;

        protected override void SetTweenEssentials()
        {
            base.SetTweenEssentials();

            tween.SetID(target.GetHashCode());
        }
    }
}