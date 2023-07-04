
using UnityEngine;
using NaughtyAttributes;

namespace StardropTools
{
    public class HealthComponent : MonoBehaviour, IDamageable, IHealeable, IAlive
    {
        [Header("Scriptable Reference")]
        [SerializeField] HealthContainerSyncType syncType;
        [Expandable] [SerializeField] HealthContainerSO healthContainer;

        [ShowIf("showMaxHeal")]
        [SerializeField] int startHealth;
        [ShowIf("showMaxHeal")]
        [SerializeField] int maxHealth;

        [Header("Health")]
        [ProgressBar("Health Percent", 1, EColor.Red)]
        [Range(0, 1)] [SerializeField] float healthPercent;
        [SerializeField] int health;
        [Space]
        [SerializeField] bool isDead;

        

        public int Health => health;
        public int StartHealth => startHealth;
        public int MaxHealth => maxHealth;
        public float PercentHealth => healthPercent;
        public bool IsDead => isDead;

        #region Naughty Attributes booleans
        bool showMaxHeal => (healthContainer == null && syncType != HealthContainerSyncType.FromContainer);
        #endregion

        public EventHandler OnDamaged = new EventHandler();
        public EventHandler OnHealed = new EventHandler();

        public EventHandler<int> OnDamagedAmount = new EventHandler<int>();
        public EventHandler<int> OnHealedAmount = new EventHandler<int>();

        public EventHandler<float> OnPercentDamaged = new EventHandler<float>();
        public EventHandler<float> OnPercentHealed = new EventHandler<float>();

        public EventHandler<int> OnHealthChanged = new EventHandler<int>();
        public EventHandler<float> OnHealthPercentChanged = new EventHandler<float>();

        public EventHandler OnDeath = new EventHandler();
        public EventHandler OnRevived = new EventHandler();


        public void SetHealth(int health)
        {
            this.startHealth = health;
            this.maxHealth = health;
            this.health = health;

            InitialEvents();
        }

        public void SetHealth(int startHealth, int maxHealth)
        {
            this.startHealth = startHealth;
            this.maxHealth = maxHealth;
            health = startHealth;

            InitialEvents();
        }

        public void SetHealth(int startHealth, int maxHealth, int health)
        {
            this.startHealth = startHealth;
            this.maxHealth = maxHealth;
            this.health = health;

            InitialEvents();
        }


        void InitialEvents()
        {
            GetPercent();
            OnHealthChanged?.Invoke(health);

            if (healthContainer != null)
            {
                OnHealthChanged.AddListener(healthContainer.SetHealth);
                OnHealthPercentChanged.AddListener(healthContainer.SetPercentHealth);
            }
        }


        /// <summary>
        /// Decreases health by damage and returns remaining health. Also checks if health reaches zero
        /// </summary>
        public int ApplyDamage(int damageAmount)
        {
            if (isDead)
                return 0;

            health = Mathf.Clamp(health - damageAmount, 0, maxHealth);

            if (health == 0 && isDead == false)
                Death();            

            GetPercent();

            OnDamaged?.Invoke();
            OnDamagedAmount?.Invoke(damageAmount);
            OnPercentDamaged?.Invoke(healthPercent);
            OnHealthChanged?.Invoke(health);

            return health;
        }

        /// <summary>
        /// Value from 0 to 1 and Returns remaining health
        /// </summary>
        public int ApplyDamagePercent(float percent, bool fromMaxHealth)
        {
            if (isDead)
                return 0;

            int damage = fromMaxHealth ? Mathf.CeilToInt(percent * maxHealth) : Mathf.CeilToInt(percent * health);
            return ApplyDamage(damage);
        }



        /// <summary>
        /// Returns remaining health
        /// </summary>
        public int ApplyHeal(int healAmount)
        {
            if (isDead)
                return 0;

            health = Mathf.Clamp(health + healAmount, 0, maxHealth);

            if (health > 0 && isDead == true)
                isDead = false;

            GetPercent();

            OnHealed?.Invoke();
            OnHealedAmount?.Invoke(healAmount);
            OnPercentHealed?.Invoke(healthPercent);
            OnHealthChanged?.Invoke(health);

            return health;
        }


        /// <summary>
        /// Value from 0 to 1 and Returns remaining health
        /// </summary>
        public int ApplyHealPercent(float percent, bool fromMaxHealth)
        {
            if (isDead)
                return 0;

            int heal = fromMaxHealth ? Mathf.CeilToInt(percent * maxHealth) : Mathf.CeilToInt(percent * health);
            return ApplyHeal(heal);
        }


        /// <summary>
        /// Clear dead flag and fill Health to Max
        /// </summary>
        public void Revive()
        {
            isDead = false;
            health = maxHealth;
            GetPercent();

            OnHealthChanged?.Invoke(health);
        }


        /// <summary>
        /// Clear dead flag and fill Health to set value
        /// </summary>
        public void Revive(int reviveHealth)
        {
            isDead = false;
            health = reviveHealth;
            GetPercent();

            OnHealthChanged?.Invoke(health);
            OnRevived?.Invoke();
        }


        /// <summary>
        /// Clear dead flag and fill Health to set percent
        /// </summary>
        public void Revive(float percentMaxHealth)
        {
            isDead = false;
            health = Mathf.CeilToInt(percentMaxHealth * maxHealth);
            GetPercent();

            OnHealthChanged?.Invoke(health);
        }

        float GetPercent()
        {
            healthPercent = Mathf.Clamp(health / (float)maxHealth, 0, 1);
            OnHealthPercentChanged?.Invoke(healthPercent);

            return healthPercent;
        }



        public void Kill() => ApplyDamagePercent(1, true);

        protected void Death()
        {
            OnDeath?.Invoke();
            isDead = true;
        }


        public void SyncHealthToContainer()
        {
            if (healthContainer != null)
            {
                healthContainer.SetHealth(health);
                healthContainer.SetPercentHealth(healthPercent);
            }
        }

        public void SetHealthFromContainer(HealthContainerSO healthContainer)
        {
            if (healthContainer == null)
                return;

            maxHealth = healthContainer.MaxHealth;
            startHealth = healthContainer.StartHealth;
            health = healthContainer.Health;

            InitialEvents();
        }

        public void SetHealthFromContainer() => SetHealthFromContainer(healthContainer);

        void CheckSyncType()
        {
            if (syncType == HealthContainerSyncType.FromContainer)
                SetHealthFromContainer();
            if (syncType == HealthContainerSyncType.ToContainer)
                SyncHealthToContainer();
        }



        [Button("Set Health to Start Health")]
        void SetHealthToStart()
        {
            health = startHealth;
            GetPercent();
        }

        [Button("Set Health to Max Health")]
        void SetHealthToMax()
        {
            health = maxHealth;
            GetPercent();
        }

        private void OnValidate()
        {
            GetPercent();

            CheckSyncType();

            if (startHealth > maxHealth)
                maxHealth = startHealth;

            health = Mathf.Clamp(health, 0, maxHealth);
        }
    }
}