
namespace StardropTools.Pool
{
    public class PoolItemTyped<T> where T : UnityEngine.MonoBehaviour
    {
        protected PoolItem poolItem;
        protected T item;

        public PoolItem PoolItem => poolItem;
        public T Item => item;

        public PoolItemTyped(PoolItem poolItem, T item)
        {
            this.poolItem = poolItem;
            this.item = item;
        }
    }
}