
using UnityEngine;

namespace StardropTools.Tween
{
    public abstract class TweenShakeTransformComponent : TweenComponent
    {
        [Header("Target Transform")]
        public SimulationSpace simulationSpace;
        public Transform target;
        public float intensity;

        protected override void SetTweenEssentials()
        {
            base.SetTweenEssentials();

            tween.SetID(target.GetHashCode());
            tween.asShakeVector3.SetIntensity(intensity);
        }



        [NaughtyAttributes.Button("Get Self Transform")]
        private void GetTransform()
        {
            target = transform;
        }
    }
}