using System;

[Serializable]
public class Character : LivingEntity
{
    public override void Kill()
    {
        throw new NotImplementedException();
    }

    public override void TakeDamage(Entity damageSource, float damageQuantity)
    {
        throw new NotImplementedException();
    }
}

