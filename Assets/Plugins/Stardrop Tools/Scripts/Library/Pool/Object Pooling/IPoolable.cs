
namespace StardropTools.Pool
{
    public interface IPoolable
    {
        public void OnSpawn();
        public void OnDespawn();
        public void Despawn();
        public void SetPoolItem(PoolItem poolItem);

        /*
        #region Poolable
        PoolItem poolItem;

        public void SetPoolItem(PoolItem poolItem) => this.poolItem = poolItem;

        public void Despawn() => poolItem.Despawn();

        public virtual void OnSpawn()
        {

        }

        public virtual void OnDespawn()
        {

        }

        #endregion // Poolable
        */
    }
}