
using UnityEngine;

namespace StardropTools.Pool.Generic
{
    public struct GenericPoolItem<T> where T : Component
    {
        public int InstanceID { get; private set; }
        public GenericPool<T> OriginPool { get; private set; }
        public T Component { get; private set; }
        public IGenericPoolable<T> Poolable { get; private set; }
        public GameObject GameObject { get; private set; }
        public Transform Transform { get; private set; }

        public bool IsActive => GameObject.activeInHierarchy;

        public GenericPoolItem(GenericPool<T> pool, GameObject instance, int instanceID)
        {
            OriginPool = pool;
            InstanceID = instanceID;

            Component = instance.GetComponent<T>();
            Poolable = instance.GetComponent<IGenericPoolable<T>>();

            GameObject = Component.gameObject;
            Transform = Component.transform;

            Poolable.SetPoolItem(this);
        }

        public void SetActive(bool value)
            => GameObject.SetActive(value);

        public void SetPosition(Vector3 position)
            => Transform.position = position;

        public void SetRotation(Quaternion rotation)
            => Transform.rotation = rotation;

        public void SetParent(Transform parent)
            => Transform.parent = parent;

        public void SetTransforms(Vector3 position, Quaternion rotation, Transform parent)
        {
            SetPosition(position);
            SetRotation(rotation);
            SetParent(parent);
        }

        public void Despawn() => OriginPool.Despawn(this);
    }
}