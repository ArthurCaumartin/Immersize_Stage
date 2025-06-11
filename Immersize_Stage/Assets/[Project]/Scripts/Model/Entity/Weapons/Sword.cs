public class Sword : Weapon {
    public Sword(string name) : base(name) { }

    protected override void Attack(LivingEntity target, float damageQuantity)
    {
        target.TakeDamage(this, damageQuantity);
    }
}

