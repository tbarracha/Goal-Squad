
using UnityEngine;

namespace StardropTools.ScriptableValue
{
    [CreateAssetMenu(menuName = scriptableValueMenuName + "Scriptable Float")]
    public class ScriptableFloat : ScriptableValue
    {
        [SerializeField] float defaultFloat;
        [SerializeField] float value;

        public readonly EventHandler<float> OnFloatChanged = new EventHandler<float>();

        public float Float { get => value; set => SetFloat(value); }
        public float DefaultFloat => defaultFloat;

        protected override void InvokeEvents(bool invoke)
        {
            if (invoke == false)
                return;

            OnValueChanged?.Invoke();
            OnFloatChanged?.Invoke(value);
        }

        public override void Default(bool invokeEvents = true)
        {
            if (invokeEvents == false)
                return;

            value = defaultFloat;
            InvokeEvents(invokeEvents);
        }



        public void SetFloat(float value, bool invokeEvents = true)
        {
            this.value = value;
            InvokeEvents(invokeEvents);
        }

        public void SetFloat(float value) => SetFloat(value, true);


        public void SetDefaultFloat(float defaultFloat, bool setValueEqualsToDefault, bool invokeEvents = true)
        {
            this.defaultFloat = defaultFloat;

            if (setValueEqualsToDefault)
                value = defaultFloat;

            InvokeEvents(invokeEvents);
        }

        public float AddAmount(float amountToAdd, bool invokeEvents = true)
        {
            value += amountToAdd;
            InvokeEvents(invokeEvents);

            return value;
        }

        public float AddOne(bool invokeEvents = true)
        {
            value++;
            InvokeEvents(invokeEvents);

            return value;
        }

        public float SubtractAmount(float amountToSubtract, bool invokeEvents = true)
        {
            value -= amountToSubtract;
            InvokeEvents(invokeEvents);

            return value;
        }

        public float SubtractOne(bool invokeEvents = true)
        {
            value--;
            InvokeEvents(invokeEvents);

            return value;
        }
    }
}