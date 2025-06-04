using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private ScriptableCharacterData _characterData;
    [SerializeField] private CharacterUI _characterUI;

    public void Bake()
    {
        if (!_characterData) return;

        MeshInstance currentInstance = GetComponentInChildren<MeshInstance>();
        if (currentInstance)
        {
            if (currentInstance.prefabID != _characterData.meshPrefab.GetInstanceID())
                DestoryMesh(currentInstance.gameObject);
            else
                return;
        }

        currentInstance = Instantiate(_characterData.meshPrefab, transform);
        currentInstance.prefabID = _characterData.meshPrefab.GetInstanceID();

        Bounds meshBounds = currentInstance.GetComponentInChildren<SkinnedMeshRenderer>().bounds;
        float meshHeight = (meshBounds.extents.y + Mathf.Abs(meshBounds.center.y)) * currentInstance.transform.localScale.y;

        _characterUI.SetName(_characterData.characterName);
        _characterUI.SetNameHeight(meshHeight + .2f);
        _characterUI.SetDescription(_characterData.description);
    }

    private void DestoryMesh(GameObject toDestroy)
    {
        if (Application.isPlaying)
            Destroy(toDestroy);
        else
            DestroyImmediate(toDestroy);
    }
}
