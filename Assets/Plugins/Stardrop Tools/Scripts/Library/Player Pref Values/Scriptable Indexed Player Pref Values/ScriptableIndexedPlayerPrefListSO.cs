
using UnityEngine;

namespace StardropTools.PlayerPreferences
{
    /// <summary>
    /// Class that has a PlayerPref Integer (Index) and a list. We can query list items based on playerPref Index. Increment it, decrement it, save it and load it
    /// </summary>
    public abstract class ScriptableIndexedPlayerPrefListSO : ScriptableObject
    {
        [Header("Index")]
        [NaughtyAttributes.ResizableTextArea] [TextArea] [SerializeField] protected string description = "Insert list description... \n Ex: 'Enemy damage levels (this means that it has X dmg at index/lvl 0, Y dmg at index/lvl 1, etc)'";
        [Space]
        [SerializeField] protected bool resetToDefaultIndex;
        [SerializeField] protected PlayerPrefInt playerPrefListIndex;

        public EventHandler OnValueChanged => playerPrefListIndex.OnValueChanged;
        public EventHandler<int> OnIndexChanged => playerPrefListIndex.OnIntChanged;


        /// <summary>
        /// Int value from the PlayerPrefInt
        /// </summary>
        public int Index => playerPrefListIndex.Int;

        /// <summary>
        /// Resets Index to 0, (zero)
        /// </summary>
        public void ResetToDefault(bool save) => playerPrefListIndex.ResetToDefault(save);

        /// <summary>
        /// Set Index value
        /// </summary>
        public void SetIndex(int index, bool save) => playerPrefListIndex.SetInt(index, save);

        /// <summary>
        /// Increase Index by one, up to max allowed int
        /// </summary>
        public void IncrementIndex(bool save) => playerPrefListIndex.SetInt((int)Mathf.Clamp(playerPrefListIndex.Int + 1, 0, int.MaxValue), save);

        /// <summary>
        /// Decrease Index by one, up to 0, (zero)
        /// </summary>
        public void DecrementIndex(bool save) => playerPrefListIndex.SetInt((int)Mathf.Clamp(playerPrefListIndex.Int - 1, 0, int.MaxValue), save);

        /// <summary>
        /// Load saved Index value and return it
        /// </summary>
        public int LoadIndex()
        {
            playerPrefListIndex.Load();
            return Index;
        }

        protected virtual void OnValidate()
        {
            if (resetToDefaultIndex)
            {
                ResetToDefault(true);
                resetToDefaultIndex = false;
            }
        }
    }
}