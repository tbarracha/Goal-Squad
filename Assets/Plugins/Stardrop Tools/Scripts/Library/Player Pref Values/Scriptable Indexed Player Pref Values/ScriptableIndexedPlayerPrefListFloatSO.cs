
using UnityEngine;
using System.Collections.Generic;

namespace StardropTools.PlayerPreferences
{
    [CreateAssetMenu(menuName = "Stardrop / Player Preferences / Scriptable Indexed List / Indexed List Float")]
    public class ScriptableIndexedPlayerPrefListFloatSO : ScriptableIndexedPlayerPrefListSO
    {
        [Header("List")]
        [SerializeField] List<float> list;

        /// <summary>
        /// Get the value of the array at current Index
        /// </summary>
        public float CurrentFloat => list[Mathf.Clamp(Index, 0, list.Count - 1)];

        /// <summary>
        /// Get the value of the array at current Index
        /// </summary>
        public float GetCurrentFloat(bool load)
        {
            if (load)
                LoadIndex();

            return list[Index];
        }


        /// <summary>
        /// Inserts iten into the list
        /// </summary>
        public void AddItem(float item) => list.Add(item);

        /// <summary>
        /// Inserts value in the list ONLY if the list doesn't contain the same value
        /// </summary>
        public void SafeAddItem(float item)
        {
            if (list.Contains(item) == false)
                list.Add(item);
        }

        /// <summary>
        /// Removes value from the list
        /// </summary>
        public void RemoveItem(float item) => list.Remove(item);


        /// <summary>
        /// Removes value from the list ONLY if the list contains the same value
        /// </summary>
        public void SafeRemoveItem(float item)
        {
            if (list.Contains(item) == true)
                list.Remove(item);
        }

        /// <summary>
        /// Clears list, making a new list
        /// </summary>
        public void ClearList()
        {
            SetIndex(0, true);
            list = new List<float>();
        }
    }
}