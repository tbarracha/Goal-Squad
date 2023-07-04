
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenRotationComponent : TweenTransformComponent
    {
        public Vector3 startRot;
        public Vector3 endRot;

        public override Tween StartTween()
        {
            if (simulationSpace == SimulationSpace.WorldSpace)
            {
                if (hasStart)
                    tween = new TweenEulerRotation(target, startRot, endRot);
                else
                    tween = new TweenEulerRotation(target, endRot);
            }

            if (simulationSpace == SimulationSpace.LocalSpace)
            {
                if (hasStart)
                    tween = new TweenLocalEulerRotation(target, startRot, endRot);
                else
                    tween = new TweenLocalEulerRotation(target, endRot);
            }

            SetTweenEssentials();
            tween.SetID(target.GetHashCode()).Initialize();
            StartSequence();

            return tween;
        }

        [NaughtyAttributes.Button("Get Start Rotation")]
        private void GetStart()
        {
            if (simulationSpace == SimulationSpace.WorldSpace)
                startRot = target.eulerAngles;
            else
                startRot = target.localEulerAngles;
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