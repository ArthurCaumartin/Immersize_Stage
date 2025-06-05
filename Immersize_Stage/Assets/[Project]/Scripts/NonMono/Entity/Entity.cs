using UnityEngine;


public abstract class Entity
{
    [SerializeField] protected string _name = "Entity";
    [SerializeField] protected string _description = "Not Set";

}


public abstract class NonLivingEntity : Entity
{
    // je sais po :/
}




