using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxHealth = 100;
    private int _currentHealth;

    public int Health
    {
        get => _currentHealth;
        set { _currentHealth = value; }
    }

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    #region IDamage

    public void Die()
    {

    }

    public void Heal(int heal)
    {

    }

    public void TakeDamage(int damage)
    {
        if (Health > damage)
        {
            Health -= damage;
        }
        else
        {
            Health = 0;
            Die();
        }

        print($"Health = {Health} | Damage = {damage}");
    }

    #endregion

}
