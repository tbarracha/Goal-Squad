
using UnityEngine;

namespace StardropTools.Pool
{
    public class PooledEffect : BaseObject, IPoolable
    {
        [SerializeField] float lifeTime = 0;
        [SerializeField] ParticleSystem[]   particles;
        [SerializeField] TrailRenderer[]    trails;

        #region Poolable
        PoolItem poolItem;

        public void SetPoolItem(PoolItem poolItem) => this.poolItem = poolItem;

        public void Despawn() => poolItem.Despawn();

        public void OnSpawn()
        {
            Utilities.ClearParticles(particles);
            Utilities.ClearTrails(trails);

            if (lifeTime > 0)
                Invoke(nameof(Despawn), lifeTime);
        }

        public void OnDespawn()
        {
            Utilities.ClearParticles(particles);
            Utilities.ClearTrails(trails);
        }

        #endregion // Poolable
    }
}