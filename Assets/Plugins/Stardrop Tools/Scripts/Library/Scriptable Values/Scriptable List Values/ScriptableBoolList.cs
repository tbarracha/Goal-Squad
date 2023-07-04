
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.ScriptableValue
{
    [CreateAssetMenu(menuName = scriptableListMenuName + "Scriptable List Bool")]
    public class ScriptableBoolList : ScriptableList
    {
        [SerializeField] List<bool> defaultList;
        [SerializeField] List<bool> list;

        protected override void InvokeEvents(bool invoke)
        {
            if (invoke == false)
                return;

            OnValueChanged?.Invoke();
        }

        [NaughtyAttributes.Button]
        public override void Default(bool invokeEvents = true)
        {
            list = new List<bool>();
            for (int i = 0; i < defaultList.Count; i++)
                list.Add(defaultList[i]);

            if (invokeEvents == false)
                InvokeEvents(invokeEvents);
        }

        public bool GetBool(int index) => list[index];

        public bool GetRandom() => list.GetRandom();

        public List<bool> GetRandomNonRepeat(int amount) => list.GetRandomNonRepeat(amount);

        public void Add(bool value, bool invokeEvents = true)
        {
            list.Add(value);
            InvokeEvents(invokeEvents);
        }

        public void AddSafe(bool value, bool invokeEvents = true)
        {
            if (list.Contains(value) == false)
                list.Add(value);

            InvokeEvents(invokeEvents);
        }

        public void Remove(bool value, bool invokeEvents = true)
        {
            list.Remove(value);
            InvokeEvents(invokeEvents);
        }

        public void RemoveSafe(bool value, bool invokeEvents = true)
        {
            if (list.Contains(value))
                list.Remove(value);

            InvokeEvents(invokeEvents);
        }

        public bool[] ToArray() => list.ToArray();

        public void Clear(bool invokeEvents = true)
        {
            list.Clear();
            InvokeEvents(invokeEvents);
        }
    }
}