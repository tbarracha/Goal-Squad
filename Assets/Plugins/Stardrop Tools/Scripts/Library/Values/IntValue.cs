
namespace StardropTools
{
    public struct IntValue : IValue
    {
        private int value;

        public int Int { get => value; set => SetInt(value); }

        public readonly EventHandler<int> OnValueChanged;


        public IntValue(int value)
        {
            this.value = value;
            OnValueChanged = new EventHandler<int>();
        }

        public void InvokeEvents(bool invoke)
        {
            if (invoke == false)
                return;

            OnValueChanged?.Invoke(value);
        }

        public void SetInt(int value, bool invokeEvents = true)
        {
            this.value = value;
            InvokeEvents(invokeEvents);
        }

        public int Add(int valueToAdd, bool invokeEvents = true)
        {
            value += valueToAdd;
            InvokeEvents(invokeEvents);

            return value;
        }

        public int Subtract(int valueToSubtract, bool invokeEvents = true)
        {
            value -= valueToSubtract;
            InvokeEvents(invokeEvents);

            return value;
        }
    }
}