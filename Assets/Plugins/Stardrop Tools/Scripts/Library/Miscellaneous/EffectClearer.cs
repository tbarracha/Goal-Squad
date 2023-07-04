
using UnityEngine;

namespace StardropTools
{
    /// <summary>
    /// Class that will clear Trail Rendereres and Particle Systems on Enable(). Optionally call Clear() to clear effects
    /// </summary>
    public class EffectClearer : MonoBehaviour
    {
        [SerializeField] bool clearOnEnable;
        [SerializeField] TrailRenderer[] trails;
        [SerializeField] ParticleSystem[] particles;

        private void OnEnable()
        {
            if (clearOnEnable)
                Clear();
        }

        public void Clear()
        {
            Utilities.ClearTrails(trails);
            Utilities.ClearParticles(particles);
        }

        [NaughtyAttributes.Button("Get Effects")]
        public void GetEffects()
        {
            trails = GetComponentsInChildren<TrailRenderer>();
            particles = GetComponentsInChildren<ParticleSystem>();
        }
    }
}