
namespace StardropTools
{
    using UnityEditor.Rendering;
    using UnityEngine;

    /// <summary>
    /// Base class for all objects that are meant to move around the scene space
    /// </summary>
    public class BaseObject : BaseComponent
    {
        [NaughtyAttributes.Foldout("Object Info")]
        [NaughtyAttributes.ReadOnly]
        [SerializeField] protected Transform selfTransform;

        public Transform Transform => selfTransform;
        public Transform Parent => selfTransform.parent;

        public Vector3 Position { get => selfTransform.position; set => selfTransform.position = value; }
        public Quaternion Rotation { get => selfTransform.rotation; set => selfTransform.rotation = value; }
        public Vector3 EulerAngles { get => selfTransform.eulerAngles; set => selfTransform.eulerAngles = value; }
        public Vector3 LocalScale { get => transform.localScale; set => transform.localScale = value; }


        #region Position
        public Vector3 LocalPosition { get => selfTransform.localPosition; set => selfTransform.localPosition = value; }

        /// <summary>
        /// Transform.forward
        /// </summary>
        public Vector3 Forward { get => selfTransform.forward; }

        /// <summary>
        /// X (horizontal) value of the World Position vector
        /// </summary>
        public float PosX { get => Position.x; set => SetPositionX(value); }

        /// <summary>
        /// Y (height) value of the World Position vector
        /// </summary>
        public float PosY { get => Position.y; set => SetPositionY(value); }

        /// <summary>
        /// Z (depth) value of the World Position vector
        /// </summary>
        public float PosZ { get => Position.z; set => SetPositionZ(value); }

        /// <summary>
        /// X (horizontal) value of the Local Position vector
        /// </summary>
        public float LocalPosX { get => LocalPosition.x; set => SetLocalPositionX(value); }

        /// <summary>
        /// Y (height) value of the Local Position vector
        /// </summary>
        public float LocalPosY { get => LocalPosition.y; set => SetLocalPositionY(value); }

        /// <summary>
        /// Z (depth) value of the Local Position vector
        /// </summary>
        public float LocalPosZ { get => LocalPosition.z; set => SetLocalPositionZ(value); }

        /// <summary>
        /// Set the current world position of this object
        /// </summary>
        public void SetPosition(Vector3 position) => Position = position;

        /// <summary>
        /// Set the current local position of this object
        /// </summary>
        public void SetLocalPosition(Vector3 localPosition) => LocalPosition = localPosition;

        public void SetPositionX(float x) => Position = UtilsVector.SetVectorX(Position, x);
        public void SetPositionY(float y) => Position = UtilsVector.SetVectorY(Position, y);
        public void SetPositionZ(float z) => Position = UtilsVector.SetVectorZ(Position, z);

        public void SetLocalPositionX(float x) => LocalPosition = UtilsVector.SetVectorX(Position, x);
        public void SetLocalPositionY(float y) => LocalPosition = UtilsVector.SetVectorY(Position, y);
        public void SetLocalPositionZ(float z) => LocalPosition = UtilsVector.SetVectorZ(Position, z);

        public void ResetPosition() => Position = Vector3.zero;
        public void ResetLocalPosition() => LocalPosition = Vector3.zero;
        #endregion // position


        #region Rotation
        public Quaternion LocalRotation { get => selfTransform.localRotation; set => selfTransform.localRotation = value; }
        public Vector3 LocalEulerAngles { get => selfTransform.localEulerAngles; set => selfTransform.localEulerAngles = value; }

        public void SetRotation(Quaternion rotation) => Rotation = rotation;
        public void SetLocalRotation(Quaternion localRotation) => LocalRotation = localRotation;

        public void SetEulerAngles(Vector3 eulerAngles) => EulerAngles = eulerAngles;
        public void SetLocalEulerAngles(Vector3 localEulerAngles) => LocalEulerAngles = localEulerAngles;

        public void SetEulerX(float x) => EulerAngles = UtilsVector.SetVectorX(EulerAngles, x);
        public void SetEulerY(float y) => EulerAngles = UtilsVector.SetVectorY(EulerAngles, y);
        public void SetEulerZ(float z) => EulerAngles = UtilsVector.SetVectorZ(EulerAngles, z);

        public void SetLocalEulerX(float x) => LocalEulerAngles = UtilsVector.SetVectorX(LocalEulerAngles, x);
        public void SetLocalEulerY(float y) => LocalEulerAngles = UtilsVector.SetVectorY(LocalEulerAngles, y);
        public void SetLocalEulerZ(float z) => LocalEulerAngles = UtilsVector.SetVectorZ(LocalEulerAngles, z);

        public void ResetRotation() => Rotation = Quaternion.identity;
        public void ResetLocalRotation() => LocalRotation = Quaternion.identity;
        #endregion // Rotation


        #region Scale
        public void SetScale(Vector3 scale) => LocalScale = scale;

        public void SetScaleX(float x) => LocalScale = UtilsVector.SetVectorX(LocalScale, x);
        public void SetScaleY(float y) => LocalScale = UtilsVector.SetVectorY(LocalScale, y);
        public void SetScaleZ(float z) => LocalScale = UtilsVector.SetVectorZ(LocalScale, z);

        public void ResetScale() => LocalScale = Vector3.one;
        #endregion // Scale



        #region Events

        /// <summary>
        /// Event fired when we change parent via the SetParent() method
        /// </summary>
        public readonly EventHandler OnParentChange = new EventHandler();

        /// <summary>
        /// Event fired when children change via the SetChild() method
        /// </summary>
        public readonly EventHandler OnChildrenChange = new EventHandler();
        #endregion // Events



        protected override void OnValidate()
        {
            base.OnValidate();

            if (selfTransform == null)
                selfTransform = transform;
        }


        /// <summary>
        /// Sets parent as Null
        /// </summary>
        public void ClearParent() => SetParent(null);


        /// <summary>
        /// Set the new Parent of this object, if it isn't its child
        /// </summary>
        public void SetParent(Transform parent)
        {
            if (selfTransform.parent != parent)
            {
                selfTransform.parent = parent;
                OnParentChange?.Invoke();
            }

            else
                Debug.Log(name + " is already child of " + parent);
        }

        /// <summary>
        /// Set a new child of this object, it it isn't already a child
        /// </summary>
        public void SetChild(Transform child)
        {
            if (child.parent != selfTransform)
            {
                child.parent = selfTransform;
                OnChildrenChange?.Invoke();
            }

            else
                Debug.Log(name + "is already parent of " + child);
        }


        /// <summary>
        /// Set this objects children index in relation to its parent
        /// </summary>
        public void SetSiblingIndex(int siblingIndex) => selfTransform.SetSiblingIndex(siblingIndex);

        /// <summary>
        /// Returns the Direction TO target Vector
        /// </summary>
        public Vector3 DirectionTo(Vector3 target) => target - Position;

        /// <summary>
        /// Returns the Direction from this objects position TO target Transform
        /// </summary>
        public Vector3 DirectionTo(Transform target) => target.position - Position;

        /// <summary>
        /// Returns the Direction FROM target Transform to this objects position
        /// </summary>
        public Vector3 DirectionFrom(Vector3 target) => Position - target;

        /// <summary>
        /// Returns the Direction FROM target Transform to this objects position
        /// </summary>
        public Vector3 DirectionFrom(Transform target) => Position - target.position;


        /// <summary>
        /// Returns the Distance TO and FROM this objects position relative to target Vector
        /// </summary>
        public float DistanceTo(Vector3 target) => DirectionTo(target).magnitude;

        /// <summary>
        /// Returns the Distance TO and FROM this objects position relative to target Transform
        /// </summary>
        public float DistanceTo(Transform target) => DirectionTo(target).magnitude;


        /// <summary>
        /// Rotates this object towards target Vector imediately.
        /// <para>Optionally, can lock certain axis</para>
        /// </summary>
        public Quaternion LookAt(Vector3 direction, bool lockX = true, bool lockY = false, bool lockZ = true)
        {
            if (direction == Vector3.zero)
                return Quaternion.identity;

            Quaternion lookRot = Quaternion.LookRotation(direction);

            if (lockX) lookRot.x = 0;
            if (lockY) lookRot.y = 0;
            if (lockZ) lookRot.z = 0;

            Rotation = lookRot;

            return lookRot;
        }


        /// <summary>
        /// Rotates object towards target Transform imediately.
        /// <para>Optionally, can lock certain axis</para>
        /// </summary>
        public Quaternion LookAt(Transform target, bool lockX = false, bool lockY = true, bool lockZ = false)
        {
            Vector3 lookDir = DirectionTo(target.position);
            Quaternion targetRot = LookAt(lookDir, lockX, lockY, lockZ);

            return targetRot;
        }


        /// <summary>
        /// Rotates object smoothly based on lookSpeed toward target direction. Must be updated!
        /// <para>Optionally, can lock certain axis</para>
        /// </summary>
        public Quaternion SmoothLookAt(Vector3 direction, float lookSpeed, bool lockX = false, bool lockY = true, bool lockZ = false)
        {
            if (direction == Vector3.zero)
                return Quaternion.identity;

            Quaternion lookRot = Quaternion.LookRotation(direction);
            Quaternion targetRot = Quaternion.Slerp(Rotation, lookRot, Time.deltaTime * lookSpeed);

            if (lockX) lookRot.x = 0;
            if (lockY) lookRot.y = 0;
            if (lockZ) lookRot.z = 0;

            Rotation = targetRot;

            return targetRot;
        }

        /// <summary>
        /// Rotates object smoothly based on lookSpeed toward target transform. Must be updated!
        /// <para>Optionally, can lock certain axis</para>
        /// </summary>
        public Quaternion SmoothLookAt(Transform target, float lookSpeed, bool lockX = false, bool lockY = true, bool lockZ = false)
        {
            Vector3 lookDir = DirectionTo(target.position);
            Quaternion targetRot = SmoothLookAt(lookDir, lookSpeed, lockX, lockY, lockZ);

            return targetRot;
        }

        /// <summary>
        /// Clamps this objects position. Set a Clamp Type for more controll over positive or negative clamps
        /// </summary>
        public void ClampPosition(Vector3 clampedVector, BaseObjectPositionClampType clampType = BaseObjectPositionClampType.Both)
        {
            ClampPositionX(clampedVector.x, clampType);
            ClampPositionY(clampedVector.y, clampType);
            ClampPositionZ(clampedVector.z, clampType);
        }

        /// <summary>
        /// Clamps this objects X axis position
        /// </summary>
        public void ClampPositionX(float maxClampX, BaseObjectPositionClampType clampType = BaseObjectPositionClampType.Both)
        {
            if (PosX < maxClampX)
                return;

            if (clampType == BaseObjectPositionClampType.Both)
                PosX = Mathf.Clamp(PosX, -maxClampX, maxClampX);

            else if (clampType == BaseObjectPositionClampType.Positive)
                PosX = Mathf.Clamp(PosX, 0, maxClampX);

            else if (clampType == BaseObjectPositionClampType.Negative)
                PosX = Mathf.Clamp(PosX, -maxClampX, 0);
        }

        /// <summary>
        /// Clamps this objects Y axis position
        /// </summary>
        public void ClampPositionY(float maxClampY, BaseObjectPositionClampType clampType = BaseObjectPositionClampType.Both)
        {
            if (PosY < maxClampY)
                return;

            if (clampType == BaseObjectPositionClampType.Both)
                PosY = Mathf.Clamp(PosX, -maxClampY, maxClampY);

            else if (clampType == BaseObjectPositionClampType.Positive)
                PosY = Mathf.Clamp(PosX, 0, maxClampY);

            else if (clampType == BaseObjectPositionClampType.Negative)
                PosY = Mathf.Clamp(PosX, -maxClampY, 0);
        }

        /// <summary>
        /// Clamps this objects Z axis position
        /// </summary>
        public void ClampPositionZ(float maxClampY, BaseObjectPositionClampType clampType = BaseObjectPositionClampType.Both)
        {
            if (PosZ < maxClampY)
                return;

            if (clampType == BaseObjectPositionClampType.Both)
                PosZ = Mathf.Clamp(PosX, -maxClampY, maxClampY);

            else if (clampType == BaseObjectPositionClampType.Positive)
                PosZ = Mathf.Clamp(PosX, 0, maxClampY);

            else if (clampType == BaseObjectPositionClampType.Negative)
                PosZ = Mathf.Clamp(PosX, -maxClampY, 0);
        }
    }
}