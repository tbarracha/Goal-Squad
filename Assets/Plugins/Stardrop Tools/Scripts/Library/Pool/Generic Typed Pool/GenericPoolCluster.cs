using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.Pool.Generic
{
    /// <summary>
    /// Although a pool cluster is meant to be a group of several Item Pools that inherit from the same Component, you can still use it to group non component related objects and get them through the Spawn TComponent method.
    /// <para> Useful when spawning derived/inherited components of the main Component. Ex: enemies, bullets, spells </para>
    /// </summary>
    [System.Serializable]
    public class GenericPoolCluster<T> where T : Component
    {
        [Header("Objects To Pool")]
        [SerializeField] GameObject[] objectsToPool;
        public bool generatePools;

        [Header("Pool")]
        [SerializeField] Transform parent;
        [SerializeField] List<GenericPool<T>> pools;

        bool isPopulated;

        public GenericPoolCluster(Transform parent, GameObject[] prefabs, int capacity, bool populate = true)
        {
            pools = new List<GenericPool<T>>();
            this.parent = parent;

            for (int i = 0; i < prefabs.Length; i++)
            {
                Transform poolParent = Utilities.CreateEmpty("Pool - " + prefabs[i].name, Vector3.zero, parent);
                GenericPool<T> pool = new GenericPool<T>(prefabs[i], capacity, poolParent, false);

                pools.Add(pool);
            }

            if (populate)
                PopulateCluster();
        }

        public void PopulateCluster()
        {
            if (isPopulated)
                return;

            for (int i = 0; i < pools.Count; i++)
                pools[i].Populate();

            isPopulated = true;
        }


        public T SpawnFromPool(int poolIndex) => pools[poolIndex].Spawn();

        public T SpawnFromPool(int poolIndex, Vector3 position, Quaternion rotation, Transform parent)
            => pools[poolIndex].Spawn(position, rotation, parent);



        public TComponent SpawnFromPool<TComponent>(int poolIndex)
            => pools[poolIndex].Spawn().GetComponent<TComponent>();

        public TComponent SpawnFromPool<TComponent>(int poolIndex, Vector3 position, Quaternion rotation, Transform parent)
            => pools[poolIndex].Spawn<TComponent>(position, rotation, parent);



        public void DespawnFromPool(int poolIndex, T item)
            => pools[poolIndex].Despawn(item);

        public void DespawnFromPool(int poolIndex, GameObject item)
            => pools[poolIndex].Despawn(item);

        public void Despawn(T item)
        {
            for (int i = 0; i < pools.Count; i++)
            {
                if (pools.GetType() == item.GetType())
                    pools[i].Despawn(item);
            }
        }

        public void DespawnCluster(bool clearOverflow)
        {
            for (int i = 0; i < pools.Count; i++)
                pools[i].DespawnAll(clearOverflow);
        }


        public void RefreshPoolIDs()
        {
            for (int i = 0; i < pools.Count; i++)
                pools[i].poolID = i;
        }

        public void GeneratePoolsFromObjectToPool()
        {
            if (generatePools == false)
                return;

            // List where all new prefabs to pool will be cached
            List<GameObject> prefabs = new List<GameObject>();

            // Loop through Objects to Pool
            for (int i = 0; i < objectsToPool.Length; i++)
            {
                var prefab = objectsToPool[i];
                bool exists = false;

                // check if there already exists a pool with prefab
                foreach (GenericPool<T> pool in pools)
                    if (pool.Prefab.name == objectsToPool[i].name)
                    {
                        exists = true;
                        break;
                    }

                if (exists == false)
                    prefabs.Add(prefab);
            }

            // Create Pool parents
            //List<Transform> parents = new List<Transform>();
            //for (int i = 0; i < prefabs.Count; i++)
            //    parents.Add(Utilities.CreateEmpty("Pool - " + prefabs[i].name, parent.position, parent));

            // Create prefab pools
            int count = 0;
            foreach (GameObject prefab in prefabs)
            {
                //Pool<T> pool = new Pool<T>(prefab, 0, parents[count], false);
                GenericPool<T> pool = new GenericPool<T>(prefab, 0, parent, false);
                pool.poolName = prefab.name;

                pools.Add(pool);
                count++;
            }

            objectsToPool = new GameObject[0];
            RefreshPoolIDs();
            generatePools = false;
        }
    }
}