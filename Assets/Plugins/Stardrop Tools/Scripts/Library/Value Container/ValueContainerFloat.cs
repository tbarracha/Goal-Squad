
using UnityEngine;
using NaughtyAttributes;

namespace StardropTools
{
    public class ValueContainerFloat : MonoBehaviour
    {
        [ProgressBar("Value Percent", 1, EColor.Gray)]
        [SerializeField] float percent;
        [SerializeField] float startValue;
        [SerializeField] float maxValue;
        [SerializeField] float value;
        [SerializeField] bool isEmpty;

        public float Value => value;
        public float PercentValue => percent;
        public bool IsEmpty => isEmpty;
        

        public EventHandler<float> OnRemoved = new EventHandler<float>();
        public EventHandler<float> OnAdded = new EventHandler<float>();

        public EventHandler<float> OnValueChanged = new EventHandler<float>();
        public EventHandler<float> OnPercentChanged = new EventHandler<float>();
        public EventHandler OnValueEmpty = new EventHandler();


        #region Setters

        public void SetValues(float startHealth, float maxHealth)
        {
            this.startValue = startHealth;
            this.maxValue = maxHealth;
            value = startHealth;

            GetPercent();
            OnValueChanged?.Invoke(value);
        }

        public void SetValues(float startHealth, float maxHealth, float health)
        {
            this.startValue = startHealth;
            this.maxValue = maxHealth;
            this.value = health;

            GetPercent();
            OnValueChanged?.Invoke(value);
        }

        #endregion // Setters

        float GetPercent()
        {
            percent = Mathf.Clamp(value / maxValue, 0, 1);
            OnPercentChanged?.Invoke(percent);

            return percent;
        }


        public float RemoveValue(int amountToRemove)
        {
            if (isEmpty)
                return 0;

            value = Mathf.Clamp(value - amountToRemove, 0, maxValue);

            if (value == 0 && isEmpty == false)
            {
                isEmpty = true;
                OnValueEmpty?.Invoke();
            }

            GetPercent();
            OnValueChanged?.Invoke(value);
            return value;
        }

        /// <summary>
        /// Value from 0 to 1
        /// </summary>
        public float RemovePercentValue(float percent, bool fromMaxValue)
        {
            if (isEmpty)
                return 0;

            int damage = fromMaxValue ? Mathf.CeilToInt(percent * maxValue) : Mathf.CeilToInt(percent * value);
            return RemoveValue(damage);
        }



        public float AddValue(int amountToAdd)
        {
            if (isEmpty)
                return 0;

            value = Mathf.Clamp(value + amountToAdd, 0, maxValue);

            if (value > 0 && isEmpty == true)
                isEmpty = false;

            GetPercent();
            OnValueChanged?.Invoke(value);
            return value;
        }

        public float AddPercentValue(float percent, bool fromMaxValue)
        {
            if (isEmpty)
                return 0;

            int heal = fromMaxValue ? Mathf.CeilToInt(percent * maxValue) : Mathf.CeilToInt(percent * value);
            return AddValue(heal);
        }



        public void ResetValue()
        {
            isEmpty = false;
            value = startValue;

            GetPercent();
            OnValueChanged?.Invoke(value);
        }

        public void ResetValue(int resetValue)
        {
            isEmpty = false;
            value = resetValue;

            GetPercent();
            OnValueChanged?.Invoke(value);
        }

        public void ResetValue(float percentMaxValue)
        {
            isEmpty = false;
            value = Mathf.CeilToInt(percentMaxValue * maxValue);

            GetPercent();
            OnValueChanged?.Invoke(value);
        }
    }
}