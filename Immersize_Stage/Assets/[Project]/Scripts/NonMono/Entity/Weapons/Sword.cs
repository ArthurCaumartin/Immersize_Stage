public class Sword : Weapon
{
    protected override void Attack(LivingEntity target, float damageQuantity)
    {
        target.TakeDamage(this, damageQuantity);
    }
}

