using UnityEngine;
using Entity.Character;

public class CharacterBaker : MonoBehaviour
{
    [SerializeField] protected CharacterData characterData;
    [SerializeField] protected CharacterUI characterUI;
    protected float meshHeight;

    private void Start()
    {
        Bake();
    }

    public virtual void Bake()
    {
        if (!characterData) return;

        bool isDifferentMesh = InitializeMesh();
        if (isDifferentMesh)
        {
            InitializeComponent();
        }
    }

    protected virtual bool InitializeMesh()
    {
        MeshInstance currentInstance = GetComponentInChildren<MeshInstance>();
        if (currentInstance)
        {
            if (currentInstance.prefabID != characterData.meshPrefab.GetInstanceID())
                DestoryMesh(currentInstance.gameObject);
            else
                return false;
        }

        currentInstance = Instantiate(characterData.meshPrefab, transform).GetComponent<MeshInstance>();
        currentInstance.prefabID = characterData.meshPrefab.GetInstanceID();

        Bounds meshBounds = currentInstance.GetComponentInChildren<Renderer>().bounds;
        meshHeight = (meshBounds.extents.y + Mathf.Abs(meshBounds.center.y)) * currentInstance.transform.localScale.y;
        return true;
    }

    protected virtual void InitializeComponent()
    {
        characterUI.SetName(characterData.Name);
        characterUI.SetNameHeight(meshHeight + .4f);
        characterUI.SetDescription(characterData.Description);
    }

    protected virtual void DestoryMesh(GameObject toDestroy)
    {
        if (Application.isPlaying)
            Destroy(toDestroy);
        else
            DestroyImmediate(toDestroy);
    }
}
