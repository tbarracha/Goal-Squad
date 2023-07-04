using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using StardropTools;

/// <summary>
/// Base class for objects with Health (Damageable Objects)
/// </summary>
[RequireComponent(typeof(HealthComponent))]
public class HealthyObject : BaseObject, IDamageable
{
    [Header("Health")]
    [SerializeField] protected HealthComponent healthComponent;

    public EventHandler OnDamaged => healthComponent.OnDamaged;
    public EventHandler OnHealed => healthComponent.OnHealed;

    public EventHandler OnRevived => healthComponent.OnRevived;
    public EventHandler OnDeath => healthComponent.OnDeath;

    public EventHandler<int> OnHealthChanged => healthComponent.OnHealthChanged;


    public int Health => healthComponent.Health;

    public int ApplyDamage(int damageAmount) => healthComponent.ApplyDamage(damageAmount);

    public int ApplyHeal(int healAmount) => healthComponent.ApplyHeal(healAmount);

    public void Kill() => healthComponent.Kill();

    public void Revive() => healthComponent.Revive();

    protected virtual void Death() { }

    protected override void OnValidate()
    {
        base.OnValidate();

        if (healthComponent == null)
            healthComponent = GetComponent<HealthComponent>();
    }
}