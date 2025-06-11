using UnityEngine;

public class Enemy_Poutch : Enemy
{
    public Enemy_Poutch(string name) : base(name) { }
    [SerializeField] protected bool canDie = false;

    public override void Kill()
    {
        if (canDie)
        {
            base.Kill();
            return;
        }
        currentHealth = maxHealth / 2;
    }
}