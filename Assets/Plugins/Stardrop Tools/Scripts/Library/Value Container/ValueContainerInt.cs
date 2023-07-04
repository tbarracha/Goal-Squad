
using UnityEngine;
using NaughtyAttributes;

namespace StardropTools
{
    public class ValueContainerInt : MonoBehaviour
    {
        [ProgressBar("Value Percent", 1, EColor.Gray)]
        [SerializeField] float percent;
        [SerializeField] int startValue;
        [SerializeField] int maxValue;
        [SerializeField] int value;
        [SerializeField] bool isEmpty;

        public int Value => value;
        public float PercentValue => percent;
        public bool IsEmpty => isEmpty;
        

        public EventHandler<int> OnRemoved = new EventHandler<int>();
        public EventHandler<int> OnAdded = new EventHandler<int>();

        public EventHandler<int> OnValueChanged = new EventHandler<int>();
        public EventHandler<float> OnPercentChanged = new EventHandler<float>();
        public EventHandler OnValueEmpty = new EventHandler();


        #region Constructors

        public void SetValue(int startHealth, int maxHealth)
        {
            this.startValue = startHealth;
            this.maxValue = maxHealth;
            value = startHealth;

            GetPercent();
            OnValueChanged?.Invoke(value);
        }

        public void SetValue(int startHealth, int maxHealth, int health)
        {
            this.startValue = startHealth;
            this.maxValue = maxHealth;
            this.value = health;

            GetPercent();
            OnValueChanged?.Invoke(value);
        }

        #endregion // Constructos

        float GetPercent()
        {
            percent = Mathf.Clamp(value / maxValue, 0, 1);
            OnPercentChanged?.Invoke(percent);

            return percent;
        }

        public int RemoveValue(int amountToRemove)
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
        public int RemovePercentValue(float percent, bool fromMaxValue)
        {
            if (isEmpty)
                return 0;

            int damage = fromMaxValue ? Mathf.CeilToInt(percent * maxValue) : Mathf.CeilToInt(percent * value);
            GetPercent();

            return RemoveValue(damage);
        }



        public int AddValue(int amountToAdd)
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

        public int AddPercentValue(float percent, bool fromMaxValue)
        {
            if (isEmpty)
                return 0;

            int heal = fromMaxValue ? Mathf.CeilToInt(percent * maxValue) : Mathf.CeilToInt(percent * value);

            return AddValue(heal);
        }



        public void ResetValue()
        {
            isEmpty = false;
            value = maxValue;

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