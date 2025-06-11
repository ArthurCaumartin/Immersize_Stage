using UnityEngine;

public class MeshInstance : MonoBehaviour
{
    [SerializeField] private Renderer _meshRenderer;
    public int prefabID = 999;

    public Renderer Renderer { get => _meshRenderer; }

    private void OnValidate()
    {
        prefabID = transform.GetInstanceID();
    }
}

