using UnityEngine;
using Entity.Character;
using Unity.VisualScripting;

public class CharacterBaker : MonoBehaviour
{
    [SerializeField] protected CharacterData characterData;
    [SerializeField] protected Character character;
    [SerializeField] protected CharacterUI characterUI;
    [SerializeField] protected CharacterAnimation characterAnimation;
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

        character.BakeCharacter(characterData);
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
        characterAnimation.Animator = currentInstance.GetComponentInChildren<Animator>();


        Renderer renderer = currentInstance.Renderer;
        Bounds meshBounds = renderer.bounds;
        meshHeight = (meshBounds.extents.y + Mathf.Abs(meshBounds.center.y)) * currentInstance.transform.localScale.y;
        return true;
    }

    protected virtual void InitializeComponent()
    {
        characterUI.SetName(characterData.Name);
        characterUI.SetNameHeight(meshHeight + 1f);
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
