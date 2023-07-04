
using UnityEngine;

namespace StardropTools.PlayerPreferences
{
    [CreateAssetMenu(menuName = "Stardrop / Player Preferences / Scriptable Player Preferences / Scriptable Player Pref Int")]
    public class ScriptablePlayerPrefInt : ScriptablePlayerPrefBaseValue
    {
        [Space]
        [SerializeField] int defaultInt;
        [SerializeField] int value;

        public readonly EventHandler<int> OnIntChanged = new EventHandler<int>();

        public int Int => value;

        public int GetInt(bool load)
        {
            if (load)
                return Load();

            else
                return value;
        }

        public void SetInt(int value, bool save)
        {
            if (value == this.value)
                return;

            PlayerPrefs.SetInt(key, value);
            this.value = value;

            OnValueChanged?.Invoke();
            OnIntChanged?.Invoke(this.value);

            if (save)
                Save();
        }

        public int AddValue(int valueToAdd, bool save)
        {
            value += valueToAdd;
            SetInt(value, save);

            return value;
        }

        public int SubtractValue(int valueToSubtract, bool save)
        {
            value -= valueToSubtract;
            SetInt(value, save);

            return value;
        }

        public int MultiplyBy(int valueToMultiply, bool save)
        {
            value *= valueToMultiply;
            SetInt(value, save);

            return value;
        }

        public int MultiplyBy(float valueToMultiply, bool save)
        {
            value = Mathf.RoundToInt(value * valueToMultiply);
            SetInt(value, save);

            return value;
        }

        public int DivideBy(int valueToDivide, bool save)
        {
            value /= valueToDivide;
            SetInt(value, save);

            return value;
        }

        public int DivideBy(float valueToDivide, bool save)
        {
            value = Mathf.RoundToInt(value / valueToDivide);
            SetInt(value, save);

            return value;
        }

        public int IncrementOne(bool save) => AddValue(1, save);

        public int DecrementOne(bool save) => SubtractValue(1, save);


        public int Load()
        {
            value = PlayerPrefs.GetInt(key);
            return value;
        }

        public override void Reset()
        {
            base.Reset();

            defaultInt = 0;
            value = 0;
        }

        public override void ResetToDefault(bool save) => SetInt(defaultInt, save);


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