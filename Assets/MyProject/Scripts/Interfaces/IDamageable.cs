using System;
using UnityEngine.EventSystems;

public interface IDamageable
{
    public int Health { get; }
    public EventHandler<int> TakeDamage { get; }
    public EventHandler<int> TakeHeal { get; }
}
