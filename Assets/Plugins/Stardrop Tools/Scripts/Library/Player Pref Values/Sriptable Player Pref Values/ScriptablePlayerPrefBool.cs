
using UnityEngine;

namespace StardropTools.PlayerPreferences
{
    [CreateAssetMenu(menuName = "Stardrop / Player Preferences / Scriptable Player Preferences / Scriptable Player Pref Bool")]
    public class ScriptablePlayerPrefBool : ScriptablePlayerPrefBaseValue
    {
        [Space]
        [SerializeField] bool defaultBool;
        [SerializeField] bool value;

        public readonly EventHandler<bool> OnBoolChanged = new EventHandler<bool>();

        public bool Bool => value;

        public bool GetBool(bool load)
        {
            if (load)
                return Load();

            else
                return value;
        }


        public void SetBool(bool value, bool save)
        {
            if (value == this.value)
                return;

            PlayerPrefs.SetInt(key, Utilities.ConvertBoolToInt(value));
            this.value = value;

            OnValueChanged?.Invoke();
            OnBoolChanged?.Invoke(this.value);

            if (save)
                Save();
        }

        public bool Load()
        {
            value = Utilities.ConvertIntToBool(PlayerPrefs.GetInt(key));
            return value;
        }

        public override void Reset()
        {
            base.Reset();

            defaultBool = false;
            value = false;
        }

        public override void ResetToDefault(bool save) => SetBool(defaultBool, save);


        protected override void OnValidate()
        {
            base.OnValidate();

            if (load)
            {
                Load();
                load = false;
            }

            if (resetToDefault)
            {
                ResetToDefault(true);
                resetToDefault = false;
            }
        }
    }
}