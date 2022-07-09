namespace RPG_Project
{
    public interface IDamageable
    {
        void OnDamage(DamageInfo damage, Knockback knockback);
    }
}