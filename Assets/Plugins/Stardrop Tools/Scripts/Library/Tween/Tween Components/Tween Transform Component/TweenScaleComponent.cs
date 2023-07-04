
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenScaleComponent : TweenTransformComponent
    {
        public Vector3 startScale;
        public Vector3 endScale;

        public override Tween StartTween()
        {
            if (hasStart)
                tween = new TweenLocalScale(target, startScale, endScale);
            else
                tween = new TweenLocalScale(target, endScale);

            SetTweenEssentials();
            tween.SetID(target.GetHashCode()).Initialize();
            StartSequence();

            return tween;
        }

        [NaughtyAttributes.Button("Get Start Scale")]
        private void GetStart()
        {
            startScale = target.localScale;
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

            if (simulationSpace != SimulationSpace.LocalSpace)
                simulationSpace = SimulationSpace.LocalSpace;
        }
    }
}