
using UnityEngine;
using System.Collections.Generic;

namespace StardropTools.PlayerPreferences
{
    [CreateAssetMenu(menuName = "Stardrop / Player Preferences / Scriptable Indexed List / Indexed List Int")]
    public class ScriptableIndexedPlayerPrefListIntSO : ScriptableIndexedPlayerPrefListSO
    {
        [Header("List")]
        [SerializeField] List<int> list;

        /// <summary>
        /// Get the value of the array at current Index
        /// </summary>
        public int CurrentInt => list[Mathf.Clamp(Index, 0, list.Count - 1)];

        /// <summary>
        /// Get the value of the array at current Index
        /// </summary>
        public int GetCurrentInt(bool load)
        {
            if (load)
                LoadIndex();

            return list[Index];
        }


        /// <summary>
        /// Inserts iten into the list
        /// </summary>
        public void AddItem(int item) => list.Add(item);

        /// <summary>
        /// Inserts value in the list ONLY if the list doesn't contain the same value
        /// </summary>
        public void SafeAddItem(int item)
        {
            if (list.Contains(item) == false)
                list.Add(item);
        }

        /// <summary>
        /// Removes value from the list
        /// </summary>
        public void RemoveItem(int item) => list.Remove(item);


        /// <summary>
        /// Removes value from the list ONLY if the list contains the same value
        /// </summary>
        public void SafeRemoveItem(int item)
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
            list = new List<int>();
        }
    }
}