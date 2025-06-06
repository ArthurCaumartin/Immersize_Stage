#nullable enable
using UnityEngine;
#if UNITY_EDITOR
#endif

public abstract class ScriptableItemData : ScriptableObject
{
    // si jamais on ajoute d'autre type de stuff (anneau / armure / trinket) ???
    // j'ai essayer de mettre les truc commun a tout les item ici, mais ca a tout explosé
    [SerializeField] protected int _price;
    public int Price { get => _price; }
    // [SerializeField] protected string _weaponName = "Unnamed Weapon";
    // [SerializeField] protected int _levelRequirement = 1;
    // [SerializeField] protected float _weight = 1f;
    // [SerializeField] protected float _durability = 0f;
    // [SerializeField] protected string? _description;
}
