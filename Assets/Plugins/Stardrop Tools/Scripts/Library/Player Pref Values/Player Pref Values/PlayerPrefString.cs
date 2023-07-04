
using UnityEngine;

namespace StardropTools.PlayerPreferences
{
    [System.Serializable]
    public class PlayerPrefString : PlayerPrefBaseValue
    {
        [SerializeField] string defaultString;
        [SerializeField] string value;

        public readonly EventHandler<string> OnStringChanged = new EventHandler<string>();

        public string String => value;

        public string GetString(bool load)
        {
            if (load)
                return Load();

            else
                return value;
        }


        public void SetString(string value, bool save)
        {
            if (value == this.value)
                return;

            PlayerPrefs.SetString(key, value);
            this.value = value;

            OnValueChanged?.Invoke();
            OnStringChanged?.Invoke(this.value);

            if (save)
                Save();
        }

        public string Load()
        {
            value = PlayerPrefs.GetString(key);
            return value;
        }

        public override void Reset()
        {
            base.Reset();

            defaultString = "";
            value = "";
        }

        public override void ResetToDefault(bool save) => SetString(defaultString, save);
    }
}