
namespace StardropTools.Pool.Generic
{
    /// <summary>
    /// Interface used in all Components that you want to be part of a TPool (Generic Type Pool)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericPoolable<T> where T : UnityEngine.Component
    {
        public void SetPoolItem(GenericPoolItem<T> poolItem);

        public void OnSpawn();
        public void OnDespawn();

        public void Despawn();
    }
}