
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenShakePositionComponent : TweenShakeTransformComponent
    {
        public override Tween StartTween()
        {
            if (simulationSpace == SimulationSpace.WorldSpace)
                tween = new TweenShakePosition(target, target.position);

            if (simulationSpace == SimulationSpace.LocalSpace)
                tween = new TweenShakeLocalPosition(target, target.localPosition);

            SetTweenEssentials();
            tween.SetID(target.GetHashCode()).Initialize();
            StartSequence();

            return tween;
        }

        [NaughtyAttributes.Button("Start Tween")]
        private void TweenStart()
        {
            if (Application.isPlaying)
                StartTween();
        }


        protected override void OnValidate()
        {
            base.OnValidate();
        }
    }
}