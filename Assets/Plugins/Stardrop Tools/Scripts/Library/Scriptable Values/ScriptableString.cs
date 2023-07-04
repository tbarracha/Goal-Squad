
using UnityEngine;

namespace StardropTools.ScriptableValue
{
    [CreateAssetMenu(menuName = scriptableValueMenuName + "Scriptable String")]
    public class ScriptableString : ScriptableValue
    {
        [SerializeField] string defaultString;
        [SerializeField] string value;

        public string String { get => value; set => SetString(value); }
        public string DefaultString => defaultString;

        public readonly EventHandler<string> OnStringChanged = new EventHandler<string>();

        protected override void InvokeEvents(bool invoke)
        {
            if (invoke == false)
                return;

            OnValueChanged?.Invoke();
            OnStringChanged?.Invoke(value);
        }

        public override void Default(bool invokeEvents = true)
        {
            if (invokeEvents == false)
                return;

            value = defaultString;
            InvokeEvents(invokeEvents);
        }

        public void SetString(string value, bool invokeEvents = true)
        {
            this.value = value;
            InvokeEvents(invokeEvents);
        }

        public void SetDefaultString(string defaultString, bool setValueEqualsToDefault, bool invokeEvents = true)
        {
            this.defaultString = defaultString;

            if (setValueEqualsToDefault)
                value = defaultString;

            InvokeEvents(invokeEvents);
        }
    }
}