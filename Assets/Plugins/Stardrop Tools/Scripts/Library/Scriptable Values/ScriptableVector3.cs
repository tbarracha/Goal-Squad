
using UnityEngine;

namespace StardropTools.ScriptableValue
{
    [CreateAssetMenu(menuName = scriptableValueMenuName + "Scriptable Vector 3")]
    public class ScriptableVector3 : ScriptableValue
    {
        [SerializeField] Vector3 defaultVector;
        [SerializeField] Vector3 vector;

        public readonly EventHandler<Vector3> OnVectorChanged = new EventHandler<Vector3>();

        public Vector3 Vector { get => vector; set => SetVector3(value); }
        public float ValueX => vector.x;
        public float ValueY => vector.y;
        public float ValueZ => vector.z;

        public Vector3 DefaultVector => defaultVector;
        public float DefaultX => defaultVector.x;
        public float DefaultY => defaultVector.y;
        public float DefaultZ => defaultVector.z;

        protected override void InvokeEvents(bool invoke)
        {
            if (invoke == false)
                return;
            
            OnValueChanged?.Invoke();
            OnVectorChanged?.Invoke(vector);
        }

        public override void Default(bool invokeEvents = true)
        {
            if (invokeEvents == false)
                return;

            vector = defaultVector;
            InvokeEvents(invokeEvents);
        }

        public void SetVector3(Vector3 vector, bool invokeEvents = true)
        {
            this.vector = vector;
            InvokeEvents(invokeEvents);
        }

        public void SetDefaultVector3(Vector3 defaultVector, bool setValueEqualsToDefault, bool invokeEvents = true)
        {
            this.defaultVector = defaultVector;

            if (setValueEqualsToDefault)
                vector = defaultVector;

            InvokeEvents(invokeEvents);
        }

        public void SetDefaultVector3(float x, float y, float z, bool setValueEqualsToDefault, bool invokeEvents = true)
        {
            defaultVector = new Vector3(x, y, z);

            if (setValueEqualsToDefault)
                vector = defaultVector;

            InvokeEvents(invokeEvents);
        }

        public void SetVector3(float x, float y, float z, bool invokeEvents = true)
        {
            vector = new Vector3(x, y, z);
            InvokeEvents(invokeEvents);
        }

        public void SetX(float x, bool invokeEvents = true)
        {
            vector = new Vector3(x, ValueY, ValueZ);
            InvokeEvents(invokeEvents);
        }

        public void SetY(float y, bool invokeEvents = true)
        {
            vector = new Vector3(ValueX, y, ValueZ);
            InvokeEvents(invokeEvents);
        }

        public void SetZ(float z, bool invokeEvents = true)
        {
            vector = new Vector3(ValueX, ValueY, z);
            InvokeEvents(invokeEvents);
        }


        public Vector3 Add(Vector3 valueToAdd, bool invokeEvents = true)
        {
            vector += valueToAdd;
            InvokeEvents(invokeEvents);

            return vector;
        }

        public Vector3 AddValueX(float valueToAdd, bool invokeEvents = true)
        {
            SetX(ValueX + valueToAdd, invokeEvents);
            return vector;
        }

        public Vector3 AddValueY(float valueToAdd, bool invokeEvents = true)
        {
            SetY(ValueY + valueToAdd, invokeEvents);
            return vector;
        }

        public Vector3 AddValueZ(float valueToAdd, bool invokeEvents = true)
        {
            SetZ(ValueZ + valueToAdd, invokeEvents);
            return vector;
        }


        public Vector3 Subtract(Vector3 valueToSubtract, bool invokeEvents = true)
        {
            vector += valueToSubtract;
            InvokeEvents(invokeEvents);

            return vector;
        }

        public Vector3 SubtractValueX(float valueToSubtract, bool invokeEvents = true)
        {
            SetX(ValueX - valueToSubtract, invokeEvents);
            return vector;
        }

        public Vector3 SubtractValueY(float valueToSubtract, bool invokeEvents = true)
        {
            SetY(ValueY - valueToSubtract, invokeEvents);
            return vector;
        }

        public Vector3 SubtractValueZ(float valueToSubtract, bool invokeEvents = true)
        {
            SetZ(ValueZ - valueToSubtract, invokeEvents);
            return vector;
        }
    }
}