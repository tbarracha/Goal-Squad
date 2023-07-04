
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace StardropTools.Pool.Generic
{
    /// <summary>
    /// Generic Type Pool
    /// </summary>
    [System.Serializable]
    public class GenericPool<T> where T : Component
    {
        const int overflowedID = -1;

        [Header("Pool Identification")]
        public string poolName;
        public int poolID;
        [Space]
        [SerializeField] Transform parent;
        [ShowAssetPreview][SerializeField] GameObject prefab;
        [SerializeField] int capacity;
        [SerializeField] bool canOverflow;

        Queue<GenericPoolItem<T>> itemQueue;   // items waiting to be activated
        List<GenericPoolItem<T>> itemCache;    // active items

        public bool IsPopulated { get; private set; }

        public GameObject Prefab { get => prefab; }
        public int Capacity { get => capacity; }
        public bool CanOverflow { get => canOverflow; }

        public readonly EventHandler OnSpawn = new EventHandler();
        public readonly EventHandler OnDespawn = new EventHandler();

        public readonly EventHandler<T> OnSpawnObject = new EventHandler<T>();
        public readonly EventHandler<T> OnDespawnObject = new EventHandler<T>();


        #region Constructors

        public GenericPool(GameObject prefab, int capacity, Transform parent, bool populate = true)
        {
            poolName = "Pool - " + prefab.name;
            poolID = prefab.GetInstanceID();

            this.prefab = prefab;
            this.parent = parent;
            this.capacity = capacity;
            canOverflow = true;

            if (populate)
                Populate();
        }

        public GenericPool(GameObject prefab, int capacity, Transform parent, bool canOverflow, bool populate = true)
        {
            poolName = "Pool - " + prefab.name;
            poolID = prefab.GetInstanceID();

            this.prefab = prefab;
            this.parent = parent;
            this.capacity = capacity;
            this.canOverflow = canOverflow;

            if (populate)
                Populate();
        }

        public GenericPool(string poolName, int poolID, GameObject prefab, int capacity, Transform parent, bool canOverflow, bool populate = false)
        {
            this.poolName = poolName;
            this.poolID = poolID;

            this.prefab = prefab;
            this.parent = parent;
            this.capacity = capacity;
            this.canOverflow = canOverflow;

            if (populate)
                Populate();
        }
        #endregion // constructors

        /// <summary>
        /// Fill the pool with items under parent
        /// </summary>
        public void Populate(Transform parent)
        {
            this.parent = parent;
            Populate();
        }

        /// <summary>
        /// Fill the pool with items
        /// </summary>
        public void Populate()
        {
            if (IsPopulated)
                return;

            itemQueue = new Queue<GenericPoolItem<T>>();
            itemCache = new List<GenericPoolItem<T>>();

            for (int i = 0; i < capacity; i++)
            {
                GenericPoolItem<T> item = CreateInstance(prefab, parent, i);
                itemQueue.Enqueue(item);
                itemCache.Add(item);
            }

            IsPopulated = true;
        }

        /// <summary>
        /// -1 Id, is used to itentify Overflowed items
        /// </summary>
        GenericPoolItem<T> CreateInstance(GameObject prefab, Transform parent, int instanceID)
        {
            // Game Object
            GameObject instance = Object.Instantiate(prefab, parent);
            instance.SetActive(false);
            instance.transform.position = parent.position;
            instance.name = prefab.name + " - " + instanceID;

            return new GenericPoolItem<T>(this, instance, instanceID);
        }

        private GenericPoolItem<T> GetItem()
        {
            // get from queue
            if (itemQueue.Count > 0)
            {
                GenericPoolItem<T> item = itemQueue.Dequeue();

                // make sure we take a deactivated item
                if (parent != null)
                {
                    int iterations = 0;
                    while (item.IsActive)
                    {
                        itemQueue.Enqueue(item);
                        item = itemQueue.Dequeue();

                        // break infinite loop
                        iterations++;
                        if (iterations == capacity)
                            break;
                    }
                }

                return item;
            }

            // create new instance / overflowed instance
            else
            {
                GenericPoolItem<T> item = CreateInstance(prefab, parent, overflowedID);
                itemCache.Add(item);

                return item;
            }
        }

        /// <summary>
        /// Grab a Pool Item from the Item Queue, Activates it and returns its Component
        /// </summary>
        public T Spawn()
        {
            GenericPoolItem<T> item = GetItem();

            item.SetParent(null);
            item.SetActive(true);

            if (item.Poolable != null)
                item.Poolable.OnSpawn();

            OnSpawn?.Invoke();
            OnSpawnObject?.Invoke(item.Component);

            return item.Component;
        }

        /// <summary>
        /// Grab a Pool Item from the Item Queue, Activates it, Repositions it and returns its Component
        /// </summary>
        public T Spawn(Vector3 position, Quaternion rotation, Transform parent)
        {
            GenericPoolItem<T> item = GetItem();
            item.SetTransforms(position, rotation, parent);

            item.SetActive(true);

            if (item.Poolable != null)
                item.Poolable.OnSpawn();

            OnSpawn?.Invoke();
            OnSpawnObject?.Invoke(item.Component);

            return item.Component;
        }

        /// <summary>
        /// Spawn Item and attempts to get Component
        /// </summary>
        public TComponent Spawn<TComponent>() => Spawn().GetComponent<TComponent>();

        /// <summary>
        /// Spawn Item, attempts to get Component and set Position
        /// </summary>
        public TComponent Spawn<TComponent>(Vector3 position, Quaternion rotation, Transform parent)
            => Spawn(position, rotation, parent).GetComponent<TComponent>();


        /// <summary>
        /// Deactivates object and places back on Item Queue
        /// </summary>
        public void Despawn(GenericPoolItem<T> item)
        {
            if (itemCache.Contains(item) == false)
            {
                Debug.Log("Item is not from this pool!");
                return;
            }

            if (item.Poolable != null)
                item.Poolable.OnDespawn();

            item.SetActive(false);
            item.SetParent(parent);
            itemQueue.Enqueue(item);

            OnDespawn?.Invoke();
            OnDespawnObject?.Invoke(item.Component);
        }

        /// <summary>
        /// Finds Pool Item carrying the Component, deactivates it and places it back on the Item Queue
        /// </summary>
        public void Despawn(T component)
        {
            int instanceID = component.GetInstanceID();

            for (int i = 0; i < itemCache.Count; i++)
            {
                if (itemCache[i].Component.GetInstanceID() == instanceID)
                {
                    Despawn(itemCache[i]);
                    return;
                }
            }

            Debug.Log("Item is not from this pool!");
        }


        /// <summary>
        /// Finds Pool Item carrying the Game Object, deactivates it and places it back on the Item Queue
        /// </summary>
        public void Despawn(GameObject poolObject)
        {
            for (int i = 0; i < itemCache.Count; i++)
            {
                if (itemCache[i].GameObject == poolObject)
                {
                    Despawn(itemCache[i]);
                    return;
                }
            }

            Debug.Log("Item is not from this pool!");
        }

        /// <summary>
        /// Grabs all Pool Items originated from this pool and Despawns them.
        /// <para> Optionally, can destroy overflowed Pool Items </para>
        /// </summary>
        public void DespawnAll(bool clearOverflow)
        {
            if (clearOverflow)
            {
                while (itemCache.Count != capacity)
                {
                    for (int i = 0; i < itemCache.Count; i++)
                    {
                        var item = itemCache[i];

                        if (item.InstanceID == overflowedID)
                        {
                            itemCache.Remove(item);
                            Object.Destroy(item.GameObject);
                        }
                    }
                }

                itemQueue = new Queue<GenericPoolItem<T>>();
            }

            for (int i = 0; i < itemCache.Count; i++)
                Despawn(itemCache[i]);
        }
    }
}