
namespace StardropTools
{
    public struct FloatValue : IValue
    {
        private float value;

        public float Float { get => value; set => SetFloat(value); }

        public readonly EventHandler<float> OnFloatChanged;


        public FloatValue(float value)
        {
            this.value = value;
            OnFloatChanged = new EventHandler<float>();
        }

        public void InvokeEvents(bool invoke)
        {
            if (invoke == false)
                return;

            OnFloatChanged?.Invoke(value);
        }

        public void SetFloat(float value, bool invokeEvents = true)
        {
            this.value = value;
            InvokeEvents(invokeEvents);
        }

        public float Add(float valueToAdd, bool invokeEvents = true)
        {
            value += valueToAdd;
            InvokeEvents(invokeEvents);

            return value;
        }

        public float Subtract(float valueToSubtract, bool invokeEvents = true)
        {
            value -= valueToSubtract;
            InvokeEvents(invokeEvents);

            return value;
        }
    }
}