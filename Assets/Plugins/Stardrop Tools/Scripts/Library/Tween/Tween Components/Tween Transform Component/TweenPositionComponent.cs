
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenPositionComponent : TweenTransformComponent
    {
        public Vector3 startPos;
        public Vector3 endPos;

        public override Tween StartTween()
        {
            if (simulationSpace == SimulationSpace.WorldSpace)
            {
                if (hasStart)
                    tween = new TweenPosition(target, startPos, endPos);
                else
                    tween = new TweenPosition(target, endPos);
            }

            if (simulationSpace == SimulationSpace.LocalSpace)
            {
                if (hasStart)
                    tween = new TweenLocalPosition(target, startPos, endPos);
                else
                    tween = new TweenLocalPosition(target, endPos);
            }

            SetTweenEssentials();
            tween.SetID(target.GetHashCode()).Initialize();
            StartSequence();

            return tween;
        }

        [NaughtyAttributes.Button("Get Start Position")]
        private void GetStart()
        {
            if (simulationSpace == SimulationSpace.WorldSpace)
                startPos = target.position;
            else
                startPos = target.localPosition;
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