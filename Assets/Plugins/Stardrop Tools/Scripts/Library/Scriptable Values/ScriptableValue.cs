
using UnityEngine;

namespace StardropTools.ScriptableValue
{
    public abstract class ScriptableValue : ScriptableObject
    {
        protected const string scriptableValueMenuName = "Stardrop / Scriptables / Scriptable Value / ";
#if UNITY_EDITOR
        [TextArea][SerializeField] protected string description;
#endif

        public readonly EventHandler OnValueChanged = new EventHandler();

        public abstract void Default(bool invoke);
        protected abstract void InvokeEvents(bool invoke);

    }
}