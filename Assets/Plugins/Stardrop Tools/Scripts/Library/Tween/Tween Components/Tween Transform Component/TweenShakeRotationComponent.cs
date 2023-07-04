
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenShakeRotationComponent : TweenShakeTransformComponent
    {
        public override Tween StartTween()
        {
            if (simulationSpace == SimulationSpace.WorldSpace)
                tween = new TweenShakeEulerRotation(target, target.eulerAngles);

            if (simulationSpace == SimulationSpace.LocalSpace)
                tween = new TweenShakeLocalEulerRotation(target, target.localEulerAngles);

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