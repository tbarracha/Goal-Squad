
using UnityEngine;

namespace StardropTools.PlayerPreferences
{
    //[CreateAssetMenu(menuName = "Stardrop / Player Preferences / Scriptable Player Preferences / Scriptable Player Pref Bool")]
    public abstract class ScriptablePlayerPrefBaseValue : ScriptableObject
    {
        [Header("Actions")]
        [SerializeField] protected bool resetToDefault;
        [SerializeField] protected bool load;
        [SerializeField] protected bool reset;

        [Header("Key & Value")]
        [SerializeField] protected string key;


        public readonly EventHandler OnValueChanged = new EventHandler();


        // public Type Load() { return value type; }

        public void  Save() => PlayerPrefs.Save();


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

        protected virtual void OnValidate()
        {
            if (reset)
            {
                Reset();
                reset = false;
            }
        }
    }
}