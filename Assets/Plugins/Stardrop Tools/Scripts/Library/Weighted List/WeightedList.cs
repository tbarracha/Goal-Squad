
using System.Collections.Generic;
using NaughtyAttributes;

namespace StardropTools
{
    /// <summary>
    /// General Type List where each element has a Weight(int), allowing for Weighted Randomization
    /// <para>Ex (ignore synthax): WeightedList(EnemyType) { Basic(type), 5(weight); Rate(type), 2(weight) }</para>
    /// </summary>
    [System.Serializable]
    public class WeightedList<T>
    {
#if UNITY_ENGINE
        [ResizableTextArea][UnityEngine.SerializeField] string description = "Insert Weight Desciption...";
#endif
        public List<WeightedItem<T>> list = new List<WeightedItem<T>>();

        public int Count { get => list.Count; }
        public T RandomValue { get => GetRandom(); }

        public void Add(T item, int weight)
            => list.Add(new WeightedItem<T>(item, weight));

        public void Add(WeightedItem<T> item)
        {
            if (list.Contains(item) == false)
                list.Add(item);
        }

        public void Remove(WeightedItem<T> item)
        {
            if (list.Contains(item) == false)
                list.Remove(item);
        }

        public T GetRandom()
        {
            if (list.Count == 0)
            {
                UnityEngine.Debug.Log("Weight List is empty!");
                return default(T);
            }

            float totalWeight = 0;

            foreach (WeightedItem<T> item in list)
                totalWeight += item.weight;            

            float value = UnityEngine.Random.value * totalWeight;

            float sumWeight = 0;

            foreach (WeightedItem<T> item in list)
            {
                sumWeight += item.weight;

                if (sumWeight >= value)
                    return item.item;
            }

            return default(T);
        }

        public List<T> GetRandomAmount(int amount)
        {
            var list = new List<T>();

            for (int i = 0; i < amount; i++)
                list.Add(GetRandom());

            return list;
        }
    }
}