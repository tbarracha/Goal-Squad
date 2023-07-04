
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.ScriptableValue
{
    [CreateAssetMenu(menuName = scriptableListMenuName + "Scriptable List Float")]
    public class ScriptableFloatList : ScriptableList
    {
        [SerializeField] List<float> defaultList;
        [SerializeField] List<float> list;

        protected override void InvokeEvents(bool invoke)
        {
            if (invoke == false)
                return;

            OnValueChanged?.Invoke();
        }

        [NaughtyAttributes.Button]
        public override void Default(bool invokeEvents = true)
        {
            list = new List<float>();
            for (int i = 0; i < defaultList.Count; i++)
                list.Add(defaultList[i]);

            if (invokeEvents == false)
                InvokeEvents(invokeEvents);
        }

        public float GetFloat(int index) => list[index];

        public float GetRandom() => list.GetRandom();

        public List<float> GetRandomNonRepeat(int amount) => list.GetRandomNonRepeat(amount);

        public void Add(float value, bool invokeEvents = true)
        {
            list.Add(value);
            InvokeEvents(invokeEvents);
        }

        public void AddSafe(float value, bool invokeEvents = true)
        {
            if (list.Contains(value) == false)
                list.Add(value);

            InvokeEvents(invokeEvents);
        }

        public void Remove(float value, bool invokeEvents = true)
        {
            list.Remove(value);
            InvokeEvents(invokeEvents);
        }

        public void RemoveSafe(float value, bool invokeEvents = true)
        {
            if (list.Contains(value))
                list.Remove(value);

            InvokeEvents(invokeEvents);
        }

        public float[] ToArray() => list.ToArray();

        public void Clear(bool invokeEvents = true)
        {
            list.Clear();
            InvokeEvents(invokeEvents);
        }
    }
}