using System;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Player _player;

    [Header("GUI")]
    [SerializeField] private Scrollbar _health;

    #region Unity Methods

    private void Start()
    {
        _health.size = _player.CurrentHealth / _player.MaxHealth;

    }

    private void OnEnable()
    {
        _player.HealthChanged += OnHealthChange;
    }


    private void OnDisable()
    {
        _player.HealthChanged -= OnHealthChange;
    }

    #endregion

    private void OnHealthChange(int health)
    {
        _health.size = health / _player.MaxHealth;
    }
}
