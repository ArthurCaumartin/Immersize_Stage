using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Immersize/CharacterData")]
public class ScriptableCharacterData : ScriptableObject
{
    public string characterName;
    [TextArea] public string description;
    [Space]
    public float maxHealth;
    public float moveSpeed;
    [Space]
    public GameObject meshPrefab;
    public RuntimeAnimatorController animator;
}
