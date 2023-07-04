
using UnityEngine;
using StardropTools.Tween;

namespace StardropTools.Pool.Generic
{
    /// <summary>
    /// Class used for poolable objects with limited lifetime, like Particle Effects or Effects in general
    /// </summary>
    [RequireComponent(typeof(EffectClearer))]
    public class GenericPooledEffect : BaseObject, IGenericPoolable<GenericPooledEffect>
    {
        [SerializeField] protected float lifetime = 0;
        [SerializeField] protected bool resetOnSpawn = true;
        [Space]
        [SerializeField] protected EffectClearer effectClearer;
        [SerializeField] protected TweenComponent[] tweenComponents;

        protected GenericPoolItem<GenericPooledEffect> poolItem;

        public void SetPoolItem(GenericPoolItem<GenericPooledEffect> poolItem) => this.poolItem = poolItem;

        public virtual void OnSpawn()
        {
            RefreshLifetime();

            if (lifetime > 0)
                Invoke(nameof(Despawn), lifetime);

            if (resetOnSpawn)
                effectClearer.Clear();

            TweenManager.StartTweenComponents(tweenComponents);
        }

        public virtual void OnDespawn() { }

        public void Despawn() => poolItem.Despawn();

        public void SetLifetime(float targetLifetime)
        {
            if (targetLifetime == lifetime)
                return;

            lifetime = targetLifetime;
        }

        /// <summary>
        /// First method called on Spawn. Use this to override dynamic Lifetime changes inside the code.
        /// <para>Ex: lifetime = GetComponent().tweenDuration </para>
        /// </summary>
        public virtual void RefreshLifetime()
        {

        }

        protected override void OnValidate()
        {
            base.OnValidate();

            if (effectClearer == null)
                effectClearer = GetComponent<EffectClearer>();
        }
    }
}