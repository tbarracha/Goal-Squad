
using UnityEngine;

namespace StardropTools.PlayerPreferences
{
    [CreateAssetMenu(menuName = "Stardrop / Player Preferences / Scriptable Player Preferences / Scriptable Player Pref Float")]
    public class ScriptablePlayerPrefFloat : ScriptablePlayerPrefBaseValue
    {
        [Space]
        [SerializeField] float defaultFloat;
        [SerializeField] float value;

        public readonly EventHandler<float> OnFloatChanged = new EventHandler<float>();

        public float Float => value;

        public float GetFloat(bool load)
        {
            if (load)
                return Load();

            else
                return value;
        }


        public void SetFloat(float value, bool save)
        {
            if (value == this.value)
                return;

            PlayerPrefs.SetFloat(key, value);
            this.value = value;

            OnValueChanged?.Invoke();
            OnFloatChanged?.Invoke(this.value);

            if (save)
                Save();
        }

        public float AddValue(float valueToAdd, bool save)
        {
            value += valueToAdd;
            SetFloat(value, save);

            return value;
        }

        public float SubtractValue(float valueToSubtract, bool save)
        {
            value -= valueToSubtract;
            SetFloat(value, save);

            return value;
        }

        public float MultiplyBy(float valueToMultiply, bool save)
        {
            value *= valueToMultiply;
            SetFloat(value, save);

            return value;
        }

        public float DivideBy(float valueToDivide, bool save)
        {
            value /= valueToDivide;
            SetFloat(value, save);

            return value;
        }

        public float IncrementOne(bool save) => AddValue(1, save);

        public float DecrementOne(bool save) => SubtractValue(1, save);


        public float Load()
        {
            value = PlayerPrefs.GetFloat(key);
            return value;
        }

        public override void Reset()
        {
            base.Reset();

            defaultFloat = 0;
            value = 0;
        }

        public override void ResetToDefault(bool save) => SetFloat(defaultFloat, save);


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