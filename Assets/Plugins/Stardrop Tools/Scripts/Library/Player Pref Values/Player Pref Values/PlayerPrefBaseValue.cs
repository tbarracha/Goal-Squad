
using UnityEngine;

namespace StardropTools.PlayerPreferences
{
    [System.Serializable]
    public abstract class PlayerPrefBaseValue
    {
        [Header("Key & Value")]
        [SerializeField] protected string key;

        public readonly EventHandler OnValueChanged = new EventHandler();


        // public Type Load() { return value type; }

        public void Save() => PlayerPrefs.Save();

        /// <summary>
        /// Deletes player pref key and clears all key and value info
        /// </summary>
        public virtual void Reset()
        {
            PlayerPrefs.DeleteKey(key);
            key = "";
        }

        /// <summary>
        /// Sets value as start value
        /// </summary>
        public abstract void ResetToDefault(bool save);
    }
}