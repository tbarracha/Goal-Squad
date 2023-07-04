
namespace StardropTools.Pool
{
    using System.Collections;
    using UnityEngine;

    [System.Serializable]
    public class PoolItem
    {
        [SerializeField] Pool pool;
        [SerializeField] GameObject itemObject;
        [SerializeField] Transform itemTransform;
        IPoolable poolable;

        public Coroutine lifetimeCR;

        public GameObject ItemGameObject    => itemObject;
        public Transform ItemTransform      => itemTransform;
        public bool IsActive { get; private set; }

        public PoolItem(GameObject prefab, Pool pool)
        {
            this.pool       = pool;

            itemObject      = prefab;
            itemTransform   = prefab.transform;

            poolable        = prefab.GetComponent<IPoolable>();

            IsActive        = false;
            prefab.SetActive(IsActive);

            if (poolable != null)
                poolable.SetPoolItem(this);
        }

        public void OnSpawn()
        {
            if (poolable != null)
                poolable.OnSpawn();
        }

        public void OnDespawn()
        {
            if (poolable != null)
                poolable.OnDespawn();

            if (lifetimeCR != null)
                pool.StopItemLifetimeCoroutine(lifetimeCR);
        }

        public void SetActive(bool value)
        {
            itemObject.SetActive(value);
            IsActive = value;
        }

        public void SetPosition(Vector3 position)       => ItemTransform.position = position;
        public void SetRotation(Quaternion rotation)    => ItemTransform.rotation = rotation;

        public void SetParent(Transform parent)         => itemTransform.parent = parent;

        public void SetPositionRotationAndParent(Vector3 position, Quaternion rotation, Transform parent)
        {
            SetPosition(position);
            SetRotation(rotation);
            SetParent(parent);
        }

        public bool IsFromPool(Pool targetPool)
        {
            if (targetPool.name == pool.name)
                return true;
            else
                return false;
        }



        public void SetLifetimeCoroutine(Coroutine lifetimeCR)
        {
            this.lifetimeCR = lifetimeCR;
        }

        public IEnumerator LifetimeCR(float lifetime)
        {
            float t = 0;

            while (t < lifetime)
            {
                t += Time.deltaTime;
                yield return null;
            }

            Despawn();
        }

        public void Despawn()
        {
            if (pool != null)
                pool.Despawn(this);
        }
    }
}