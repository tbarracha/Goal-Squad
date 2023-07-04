using System.Collections;
using UnityEngine;
using StardropTools;

namespace StardropTools
{
    /// <summary>
    /// Base class for objects with Health and Animator Handlers
    /// </summary>
    [RequireComponent(typeof(HealthComponent), typeof(AnimatorHandler))]
    public class AnimatedHealthyObject : HealthyObject
    {
        [SerializeField] protected AnimatorHandler animator;

        public void PlayAnimation(int animationID, bool disableOnFinish = false) => animator.PlayAnimation(animationID, disableOnFinish);

        public void CrossFadeAnimation(int animationID, bool disableOnFinish = false) => animator.CrossFadeAnimation(animationID, disableOnFinish);

        public void TriggerAnimation(int animationID, float resetTriggerDelay = .01f, bool disableOnFinish = false) => animator.TriggerAnimation(animationID, resetTriggerDelay, disableOnFinish);

        protected override void OnValidate()
        {
            base.OnValidate();

            if (animator == null)
                animator = GetComponent<AnimatorHandler>();
        }
    }
}