
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.Pool
{
    public class Pool : MonoBehaviour
    {
        [NaughtyAttributes.ShowAssetPreview] [SerializeField] GameObject prefab;
        [SerializeField] int capacity = 0;
        [SerializeField] bool debug;
        [Space]
        [Tooltip("List of all instantiated items")]
        [SerializeField] List<PoolItem> pool;

        [Tooltip("List of all spawned items")]
        [SerializeField] List<PoolItem> activeCache;

        Transform self;
        bool isPopulated;
        Coroutine clearCR;

        public GameObject Prefab => prefab;
        public int PoolCount => pool.Count;
        public int ActiveCount => activeCache.Count;


        public void SetPrefab(GameObject prefab) => this.prefab = prefab;
        public void SetCapacity(int capacity) => this.capacity = capacity;


        public void SetPool(GameObject prefab, int capacity, bool shouldPopulate)
        {
            isPopulated = false;

            SetPrefab(prefab);
            SetCapacity(capacity);

            if (shouldPopulate)
                Populate();
        }


        /// <summary>
        /// Create as many items as set in "capacity" and stores them in a list
        /// </summary>
        public void Populate()
        {
            if (isPopulated)
                return;

            if (prefab.Equals(null))
                Debug.Log($"Pool: {name}, <color=red>NO PREFAB</color>");

            if (capacity <= 0)
                Debug.Log($"Pool: {name}, <color=orange>NO CAPACITY</color>");

            self = transform;

            pool = new List<PoolItem>();
            activeCache = new List<PoolItem>();

            for (int i = 0; i < capacity; i++)
                CreateItem();

            isPopulated = true;
        }

        /// <summary>
        /// Creates a PoolItem obj and sotres it into cache
        /// </summary>
        /// <returns></returns>
        PoolItem CreateItem()
        {
            if (prefab.Equals(null))
            {
                if (debug)
                    Debug.Log($"<color=orange>Prefab is null!</color>");
                return null;
            }

            GameObject obj = null;

            if (Application.isPlaying == true)
                obj = Instantiate(prefab, self);

#if UNITY_EDITOR
            if (Application.isPlaying == false)
                obj = Utilities.CreatePrefab(prefab, self);
#endif

            obj.name += $" - {pool.Count}";

            PoolItem item = new PoolItem(obj, this);
            item.SetActive(false);

            pool.Add(item);

            return item;
        }


        /// <summary>
        /// Loops through instantiated items and tries to find an Innactive one. If not found, creates a new one and adds it to Pool list
        /// </summary>
        /// <returns></returns>
        PoolItem FindInnactiveItem()
        {
            for (int i = 0; i < pool.Count; i++)
            {
                if (pool[i].IsActive == false)
                    return pool[i];
            }

            // if we get here, All items in pool are active
            // we must create more!
            return CreateItem();
        }
        
        public PoolItem Spawn(Vector3 position, Quaternion rotation, Transform parent, float lifetime = 0)
        {
            PoolItem item = FindInnactiveItem();
            item.SetPositionRotationAndParent(position, rotation, parent);
            item.SetActive(true);
            activeCache.Add(item);

            if (lifetime > 0)
                SetItemLifetime(item, lifetime);

            item.OnSpawn();
            return item;
        }

        public T Spawn<T>(Vector3 position, Quaternion rotation, Transform parent, float lifetime = 0)
        {
            T item = Spawn(position, rotation, parent, lifetime).ItemGameObject.GetComponent<T>();
            return item;
        }

        public bool Despawn(PoolItem item)
        {
            if (item.IsFromPool(this) && activeCache.Contains(item))
            {
                item.OnDespawn();
                item.SetActive(false);
                item.SetParent(self);
                activeCache.Remove(item);

                return true;
            }

            else
            {
                if (debug)
                    Debug.Log($"Object: {item.ItemGameObject.name}, didn't come from pool: {name}");
                return false;
            }
        }

        [NaughtyAttributes.Button("Despawn All")]
        public void ClearPool(bool useCoroutine = false)
        {
            if (useCoroutine == false)
            {
                for (int i = 0; i < activeCache.Count; i++)
                {
                    PoolItem item = activeCache[i];
                    Despawn(item);
                    activeCache.Remove(item);
                }

                if (activeCache.Count > 0)
                    ClearPool(useCoroutine);
            }
            
            else
            {
                if (clearCR != null)
                    return;

                clearCR = StartCoroutine(ClearPoolCR(activeCache));
            }
        }

        System.Collections.IEnumerator ClearPoolCR(List<PoolItem> poolItems)
        {
            while (poolItems.Count > 0)
            {
                for (int i = 0; i < poolItems.Count; i++)
                    poolItems[i].Despawn();

                yield return null;
            }
        }


        void SetItemLifetime(PoolItem item, float lifetime)
        {
            Coroutine lifetimeCR = StartCoroutine(item.LifetimeCR(lifetime));
            item.SetLifetimeCoroutine(lifetimeCR);
        }

        public void StopItemLifetimeCoroutine(Coroutine lifetimeCR) => StopCoroutine(lifetimeCR);



        public static void DespawnIPoolables(IPoolable[] pooledArray)
        {
            for (int i = 0; i < pooledArray.Length; i++)
                pooledArray[i].Despawn();
        }

        public static void DespawnIPoolables(List<IPoolable> pooledList)
        {
            for (int i = 0; i < pooledList.Count; i++)
                pooledList[i].Despawn();
        }


        public static void DespawnPoolItems(PoolItem[] pooledArray)
        {
            for (int i = 0; i < pooledArray.Length; i++)
                pooledArray[i].Despawn();
        }

        public static void DespawnPoolItems(List<PoolItem> pooledList)
        {
            for (int i = 0; i < pooledList.Count; i++)
                pooledList[i].Despawn();
        }
    }
}