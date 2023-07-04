
using UnityEngine;

namespace StardropTools
{
    /// <summary>
    /// Class that is attached to a Health Component that bridges the events of health
    /// <para>This is usefeull because this scriptable object can be reused between scenes and eliminates the need for a direct reference of a specific Health Component</para>
    /// </summary>
    [CreateAssetMenu(menuName = "Stardrop / Health / Health Container SO")]
    public class HealthContainerSO : ScriptableObject
    {
        [NaughtyAttributes.ProgressBar("Health Percent", 1, NaughtyAttributes.EColor.Red)]
        [Range(0, 1)] [SerializeField] float percent;
        [Space]
        [Min(0)] [SerializeField] int maxHealth;
        [Min(0)] [SerializeField] int startHealth;
        [Space]
        [Min(0)] [SerializeField] int health;

        public int MaxHealth => maxHealth;
        public int StartHealth => startHealth;
        public int Health => health;
        public float PercentHealth => percent;

        public readonly EventHandler<int> OnHealthChanged = new EventHandler<int>();
        public readonly EventHandler<float> OnHealthPercentChanged = new EventHandler<float>();
        
        public void SetHealth(int health)
        {
            this.health = health;
            percent = Mathf.Clamp(health / (float)maxHealth, 0, 1);

            OnHealthChanged?.Invoke(this.health);
        }

        public void SetPercentHealth(float percent)
        {
            this.percent = Mathf.Max(1, percent);
            OnHealthPercentChanged?.Invoke(this.percent);
        }

        private void OnValidate()
        {
            percent = Mathf.Clamp(health / (float)maxHealth, 0, 1);
        }
    }
}