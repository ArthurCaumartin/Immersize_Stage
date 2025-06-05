using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private ScriptableCharacterData _characterData;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private CharacterHealth _health;
    [SerializeField] private CharacterUI _characterUI;

    [SerializeField] private Character _character;

    private float _meshHeight;

    public void Bake()
    {
        if (!_characterData) return;

        bool isDifferentMesh = InitializeMesh();
        if (isDifferentMesh)
        {
            InitializeComponent();
        }

    }

    private bool InitializeMesh()
    {
        MeshInstance currentInstance = GetComponentInChildren<MeshInstance>();
        if (currentInstance)
        {
            if (currentInstance.prefabID != _characterData.meshPrefab.GetInstanceID())
                DestoryMesh(currentInstance.gameObject);
            else
                return false;
        }

        currentInstance = Instantiate(_characterData.meshPrefab, transform);
        currentInstance.prefabID = _characterData.meshPrefab.GetInstanceID();

        Bounds meshBounds = currentInstance.GetComponentInChildren<SkinnedMeshRenderer>().bounds;
        _meshHeight = (meshBounds.extents.y + Mathf.Abs(meshBounds.center.y)) * currentInstance.transform.localScale.y;
        return true;
    }

    private void InitializeComponent()
    {
        if (_playerMovement)
            _playerMovement.SetMoveSpeed(_characterData.moveSpeed);

        if (_health)
            _health.SetMaxHealth(_characterData.maxHealth);

        _characterUI.SetName(_characterData.characterName);
        _characterUI.SetNameHeight(_meshHeight + .2f);
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
