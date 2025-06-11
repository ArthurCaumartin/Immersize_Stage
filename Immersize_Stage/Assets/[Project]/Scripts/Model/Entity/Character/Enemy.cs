public class Enemy : Character
{
    public Enemy(string name) : base(name) { }


    public override void Attack()
    {
        base.Attack();
    }

    public override void Kill()
    {
        base.Kill();
    }

    public override void TakeDamage(float damageQuantity)
    {
        base.TakeDamage(damageQuantity);
    }
}
