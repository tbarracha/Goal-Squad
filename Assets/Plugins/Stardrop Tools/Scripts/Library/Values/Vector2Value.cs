
using UnityEngine;

namespace StardropTools
{
    public struct Vector2Value : IValue
    {
        private Vector2 vector;

        public Vector2 Vector { get => vector; set => SetVector2(value); }
        public float ValueX => vector.x;
        public float ValueY => vector.y;


        public readonly EventHandler<Vector2> OnValueChanged;


        public Vector2Value(Vector2 value)
        {
            this.vector = value;
            OnValueChanged = new EventHandler<Vector2>();
        }

        public void InvokeEvents(bool invoke)
        {
            if (invoke == false)
                return;

            OnValueChanged?.Invoke(vector);
        }

        public void SetVector2(Vector2 value, bool invokeEvents = true)
        {
            this.vector = value;
            InvokeEvents(invokeEvents);
        }

        public void SetVector2(float x, float y, bool invokeEvents = true)
        {
            vector = new Vector2(x, y);
            InvokeEvents(invokeEvents);
        }

        public Vector2 SetValueX(float x, bool invokeEvents = true)
        {
            vector = new Vector2(x, ValueY);
            InvokeEvents(invokeEvents);

            return vector;
        }

        public Vector2 SetValueY(float y, bool invokeEvents = true)
        {
            vector = new Vector2(ValueX, y);
            InvokeEvents(invokeEvents);

            return vector;
        }


        public Vector2 Add(Vector2 vectorToAdd, bool invokeEvents = true)
        {
            vector += vectorToAdd;
            InvokeEvents(invokeEvents);

            return vector;
        }

        public Vector3 AddValueX(float xIncrement, bool invokeEvents = true)
        {
            vector = new Vector2(ValueX + xIncrement, ValueY);
            InvokeEvents(invokeEvents);

            return vector;
        }

        public Vector3 AddValueY(float yIncrement, bool invokeEvents = true)
        {
            vector = new Vector2(ValueX, ValueY + yIncrement);
            InvokeEvents(invokeEvents);

            return vector;
        }



        public Vector2 Subtract(Vector2 vectorToSubtract, bool invokeEvents = true)
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
    }
}