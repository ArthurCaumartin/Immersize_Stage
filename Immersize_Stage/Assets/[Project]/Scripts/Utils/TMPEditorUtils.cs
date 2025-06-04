using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public static class TMPEditorUtils
{
    public static void ForceSerializeTextField(this TextMeshProUGUI textMesh, string value)
    {
        SerializedObject serializedObj = new SerializedObject(textMesh);
        SerializedProperty property = serializedObj.FindProperty("m_text");

        if (property != null)
        {
            // Debug.Log("Property Find : " + property.name);
            property.stringValue = value;
            serializedObj.ApplyModifiedProperties();

            EditorUtility.SetDirty(textMesh);
            EditorSceneManager.MarkSceneDirty(textMesh.gameObject.scene);
        }
        // else
        // {
        //     Debug.Log("No property find");
        // }
    }
}