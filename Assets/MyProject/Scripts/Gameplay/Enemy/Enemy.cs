using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    [Header("Enemy options")]
    [SerializeField] protected int _maxHealth = 100;
    protected int _currentHealth;
    protected bool _isAlive = true;

    protected private StateMachine _stateMachine;

    public int Health => _currentHealth;

    public EventHandler<int> TakeDamage => OnTakeDamage;
    public EventHandler<int> TakeHeal => OnHeal;

    protected virtual void Die()
    {
        print("Dead");
    }

    protected virtual void OnHeal(object sender, int heal)
    {
        if (_currentHealth < _maxHealth)
            _currentHealth += heal;

        if (_currentHealth > _maxHealth)
            _currentHealth = _maxHealth;
    }

    protected virtual void OnTakeDamage(object sender, int damage)
    {
        if (sender is not Attacker)
            return;

        if (Health > damage)
        {
            _currentHealth -= damage;
        }
        else if (_isAlive)
        {
            _currentHealth = 0;
            Die();
        }

        print($"Health = {Health} | Damage = {damage}");
    }

}
