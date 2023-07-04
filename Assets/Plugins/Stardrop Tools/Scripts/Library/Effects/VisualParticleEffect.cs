
using UnityEngine;

namespace StardropTools
{
    public class VisualParticleEffect : BaseObject
    {
        [SerializeField] private ParticleSystem[] particleSystems;
        [SerializeField] private TrailRenderer[]  trailRenderers;
        [SerializeField] float lifeTime = 1;

        public void SetLifeTime(float lifeTime) => this.lifeTime = lifeTime;

        public void Play()
        {
            SetActive(true);
            ResetComponent();
            Utilities.PlayParticles(particleSystems);
            Utilities.PlayTrails(trailRenderers);

            if (lifeTime > 0)
                Invoke(nameof(Stop), lifeTime);
        }

        public void Stop()
        {
            Utilities.StopParticles(particleSystems);
            Utilities.StopTrails(trailRenderers);
        }

        public override void ResetComponentPublic() => ResetComponent();

        protected override void ResetComponent()
        {
            Utilities.ClearParticles(particleSystems);
            Utilities.ClearTrails(trailRenderers);
        }

        [NaughtyAttributes.Button("Get Effects")]
        public void GetEffects()
        {
            particleSystems = GetComponentsInChildren<ParticleSystem>();
            trailRenderers = GetComponentsInChildren<TrailRenderer>();
        }
    }
}