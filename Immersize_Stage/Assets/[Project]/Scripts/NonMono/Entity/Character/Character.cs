using System;
using Entity;

[Serializable]
public class Character : LivingEntity
{
    public Character(string name) : base(name) { }

    public override void Kill()
    {
        throw new NotImplementedException();
    }

    public override void TakeDamage(Weapon damageSource, float damageQuantity)
    {
        throw new NotImplementedException();
    }
}

