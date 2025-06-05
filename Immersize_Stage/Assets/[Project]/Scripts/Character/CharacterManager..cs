using UnityEngine;
using Entity.Character;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private CharacterData _characterData;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private CharacterHealth _health;
    [SerializeField] private CharacterUI _characterUI;
    private float _meshHeight;

    private void Start()
    {
        Bake();
    }

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

        currentInstance = Instantiate(_characterData.meshPrefab, transform).GetComponent<MeshInstance>();
        currentInstance.prefabID = _characterData.meshPrefab.GetInstanceID();

        Bounds meshBounds = currentInstance.GetComponentInChildren<SkinnedMeshRenderer>().bounds;
        _meshHeight = (meshBounds.extents.y + Mathf.Abs(meshBounds.center.y)) * currentInstance.transform.localScale.y;
        return true;
    }

    private void InitializeComponent()
    {
        _playerMovement?.SetMoveSpeed(_characterData.MoveSpeed);
        if (_health)
        {
            _health.SetMaxHealth(_characterData.MaxHealth);
            _health.OnHealthChange.AddListener((ratio) => _characterUI.SetHealthBarValue(ratio));
        }

        _characterUI.SetName(_characterData.Name);
        _characterUI.SetNameHeight(_meshHeight + .4f);
        _characterUI.SetDescription(_characterData.Description);
    }

    private void DestoryMesh(GameObject toDestroy)
    {
        if (Application.isPlaying)
            Destroy(toDestroy);
        else
            DestroyImmediate(toDestroy);
    }
}
