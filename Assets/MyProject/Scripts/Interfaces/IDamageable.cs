public interface IDamageable
{
    public int Health { get; set; }
    public void TakeDamage(int damage);
    public void Heal(int heal);
    public void Die();
}
