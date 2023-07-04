
using UnityEngine;

namespace StardropTools.ScriptableValue
{
    [CreateAssetMenu(menuName = scriptableValueMenuName + "Scriptable Vector 2")]
    public class ScriptableVector2 : ScriptableValue
    {
        [SerializeField] Vector2 defaultVector;
        [SerializeField] Vector2 vector;

        public readonly EventHandler<Vector2> OnVectorChanged = new EventHandler<Vector2>();

        public Vector2 Vector { get => vector; set => SetVector2(value); }
        public float ValueX => vector.x;
        public float ValueY => vector.y;

        public Vector2 DefaultVector => defaultVector;
        public float DefaultX => defaultVector.x;
        public float DefaultY => defaultVector.y;


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

        public void SetVector2(Vector2 vector, bool invokeEvents = true)
        {
            this.vector = vector;
            InvokeEvents(invokeEvents);
        }

        public void SetVector2(float x, float y, bool invokeEvents = true)
        {
            vector = new Vector2(x, y);
            InvokeEvents(invokeEvents);
        }

        public void SetDefaultVector2(Vector2 defaultVector, bool setValueEqualsToDefault, bool invokeEvents = true)
        {
            this.defaultVector = defaultVector;

            if (setValueEqualsToDefault)
                vector = defaultVector;

            InvokeEvents(invokeEvents);
        }

        public void SetDefaultVector2(float x, float y, bool setValueEqualsToDefault, bool invokeEvents = true)
        {
            defaultVector = new Vector2(x, y);

            if (setValueEqualsToDefault)
                vector = defaultVector;

            InvokeEvents(invokeEvents);
        }

        public void SetX(float x, bool invokeEvents = true)
        {
            vector = new Vector2(x, vector.y);
            InvokeEvents(invokeEvents);
        }

        public void SetY(float y, bool invokeEvents = true)
        {
            vector = new Vector2(vector.x, y);
            InvokeEvents(invokeEvents);
        }

        public Vector2 AddValue(Vector2 valueToAdd, bool invokeEvents = true)
        {
            vector += valueToAdd;
            InvokeEvents(invokeEvents);

            return vector;
        }

        public Vector2 AddValueX(float valueToAdd, bool invokeEvents = true)
        {
            SetX(ValueX + valueToAdd, invokeEvents);
            return vector;
        }

        public Vector2 AddValueY(float valueToAdd, bool invokeEvents = true)
        {
            SetY(ValueY + valueToAdd, invokeEvents);
            return vector;
        }


        public Vector2 SubtractValue(Vector2 valueToSubtract, bool invokeEvents = true)
        {
            vector += valueToSubtract;
            InvokeEvents(invokeEvents);

            return vector;
        }

        public Vector2 SubtractValueX(float valueToSubtract, bool invokeEvents = true)
        {
            SetX(ValueX - valueToSubtract, invokeEvents);
            return vector;
        }

        public Vector2 SubtractValueY(float valueToSubtract, bool invokeEvents = true)
        {
            SetY(ValueY - valueToSubtract, invokeEvents);
            return vector;
        }
    }
}