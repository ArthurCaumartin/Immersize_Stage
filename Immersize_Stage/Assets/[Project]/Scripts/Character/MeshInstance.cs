using UnityEngine;

public class MeshInstance : MonoBehaviour
{
    public int prefabID = 999;

    private void OnValidate()
    {
        prefabID = transform.GetInstanceID();
    }
}

