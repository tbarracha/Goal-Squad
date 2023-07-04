﻿
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.ScriptableValue
{
    [CreateAssetMenu(menuName = scriptableListMenuName + "Scriptable List Vector 2")]
    public class ScriptableVector2List : ScriptableList
    {
        [SerializeField] List<Vector2> defaultList;
        [SerializeField] List<Vector2> list;

        protected override void InvokeEvents(bool invoke)
        {
            if (invoke == false)
                return;

            OnValueChanged?.Invoke();
        }

        [NaughtyAttributes.Button]
        public override void Default(bool invokeEvents = true)
        {
            list = new List<Vector2>();
            for (int i = 0; i < defaultList.Count; i++)
                list.Add(defaultList[i]);

            if (invokeEvents == false)
                InvokeEvents(invokeEvents);
        }

        public Vector2 GetVector2(int index) => list[index];

        public Vector2 GetRandom() => list.GetRandom();

        public List<Vector2> GetRandomNonRepeat(int amount) => list.GetRandomNonRepeat(amount);

        public void Add(Vector2 value, bool invokeEvents = true)
        {
            list.Add(value);
            InvokeEvents(invokeEvents);
        }

        public void AddSafe(Vector2 value, bool invokeEvents = true)
        {
            if (list.Contains(value) == false)
                list.Add(value);

            InvokeEvents(invokeEvents);
        }

        public void Remove(Vector2 value, bool invokeEvents = true)
        {
            list.Remove(value);
            InvokeEvents(invokeEvents);
        }

        public void RemoveSafe(Vector3 value, bool invokeEvents = true)
        {
            if (list.Contains(value))
                list.Remove(value);

            InvokeEvents(invokeEvents);
        }

        public Vector2[] ToArray() => list.ToArray();

        public void Clear(bool invokeEvents = true)
        {
            list.Clear();
            InvokeEvents(invokeEvents);
        }
    }
}