using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private ScriptableCharacterData _characterData;
    [SerializeField] private CharacterUI _characterUI;
    [SerializeField, HideInInspector] private GameObject _meshInstancePrefab;

    private void OnValidate() => Bake();

    public void Bake()
    {
        if (!_characterData) return;
        // if (_meshInstancePrefab) print("instance : " + _meshInstancePrefab.name);
        // print("prefab : " + _characterData.meshPrefab.name);

        if (_meshInstancePrefab)
        {
            DestoryCurrentMesh();
        }

        _meshInstancePrefab = Instantiate(_characterData.meshPrefab, transform);

        Animator animator = _meshInstancePrefab.AddComponent<Animator>();
        animator.runtimeAnimatorController = _characterData.animator;

        Bounds meshBounds = _meshInstancePrefab.GetComponentInChildren<SkinnedMeshRenderer>().bounds;
        float meshHeight = (meshBounds.extents.y + Mathf.Abs(meshBounds.center.y)) * _meshInstancePrefab.transform.localScale.y;

        _characterUI.SetName(_characterData.characterName);
        _characterUI.SetNameHeight(meshHeight + .2f);
        _characterUI.SetDescription(_characterData.description);
    }

    private void DestoryCurrentMesh()
    {
        if (Application.isPlaying)
            Destroy(_meshInstancePrefab);
        else
            DestroyImmediate(_meshInstancePrefab);
    }
}


