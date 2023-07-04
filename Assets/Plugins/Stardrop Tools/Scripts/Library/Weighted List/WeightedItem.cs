
namespace StardropTools
{
    [System.Serializable]
    public struct WeightedItem<T>
    {
        public T item;
        [UnityEngine.Min(0)] public int weight;

        public WeightedItem(T item, int weight)
        {
            this.item = item;
            this.weight = weight;
        }
    }
}