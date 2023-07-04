
using UnityEngine;

namespace StardropTools
{
    public struct Vector3Value : IValue
    {
        private Vector3 vector;

        public Vector3 Vector { get => vector; set => SetVector3(value); }
        public float ValueX => vector.x;
        public float ValueY => vector.y;
        public float ValueZ => vector.z;


        public readonly EventHandler<Vector3> OnValueChanged;


        public Vector3Value(Vector3 value)
        {
            this.vector = value;
            OnValueChanged = new EventHandler<Vector3>();
        }

        public void InvokeEvents(bool invoke)
        {
            if (invoke == false)
                return;

            OnValueChanged?.Invoke(vector);
        }

        public void SetVector3(Vector3 value, bool invokeEvents = true)
        {
            this.vector = value;
            InvokeEvents(invokeEvents);
        }

        public void SetVector2(float x, float y, float z, bool invokeEvents = true)
        {
            vector = new Vector3(x, y, z);
            InvokeEvents(invokeEvents);
        }

        public Vector3 SetValueX(float x, bool invokeEvents = true)
        {
            vector = new Vector3(x, ValueY, ValueZ);
            InvokeEvents(invokeEvents);

            return vector;
        }

        public Vector3 SetValueY(float y, bool invokeEvents = true)
        {
            vector = new Vector3(ValueX, y, ValueZ);
            InvokeEvents(invokeEvents);

            return vector;
        }

        public Vector3 SetValueZ(float z, bool invokeEvents = true)
        {
            vector = new Vector3(ValueX, ValueY, z);
            InvokeEvents(invokeEvents);

            return vector;
        }


        public Vector3 Add(Vector3 vectorToAdd, bool invokeEvents = true)
        {
            vector += vectorToAdd;
            InvokeEvents(invokeEvents);

            return vector;
        }

        public Vector3 AddValueX(float xIncrement, bool invokeEvents = true)
        {
            vector = new Vector3(ValueX + xIncrement, ValueY);
            InvokeEvents(invokeEvents);

            return vector;
        }

        public Vector3 AddValueY(float yIncrement, bool invokeEvents = true)
        {
            vector = new Vector3(ValueX, ValueY + yIncrement);
            InvokeEvents(invokeEvents);

            return vector;
        }

        public Vector3 AddValueZ(float zIncrement, bool invokeEvents = true)
        {
            vector = new Vector3(ValueX, ValueY, ValueZ + zIncrement);
            InvokeEvents(invokeEvents);

            return vector;
        }



        public Vector3 Subtract(Vector3 vectorToSubtract, bool invokeEvents = true)
        {
            vector -= vectorToSubtract;
            InvokeEvents(invokeEvents);

            return vector;
        }

        public Vector3 SubtractValueX(float xDecrement, bool invokeEvents = true)
        {
            vector = new Vector2(ValueX - xDecrement, ValueY);
            InvokeEvents(invokeEvents);

            return vector;
        }

        public Vector3 SubtractValueY(float yDecrement, bool invokeEvents = true)
        {
            vector = new Vector2(ValueX, ValueY - yDecrement);
            InvokeEvents(invokeEvents);

            return vector;
        }

        public Vector3 SubtractValueZ(float zDecrement, bool invokeEvents = true)
        {
            vector = new Vector3(ValueX, ValueY, ValueZ - zDecrement);
            InvokeEvents(invokeEvents);

            return vector;
        }
    }
}