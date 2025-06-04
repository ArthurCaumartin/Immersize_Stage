using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(CharacterManager)), CanEditMultipleObjects]
public class CharacterManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Bake Character Data"))
        {
            CharacterManager characterManager = target as CharacterManager;
            characterManager.Bake();

            EditorUtility.SetDirty(characterManager);
            if (!Application.isPlaying)
                EditorSceneManager.MarkSceneDirty(characterManager.gameObject.scene);

            foreach (var item in characterManager.GetComponentsInChildren<TextMeshProUGUI>())
            {
                item.SetSerializeProperty(item.text);
            }

            Undo.RegisterCompleteObjectUndo(characterManager, "Bake");
        }
        GUILayout.Space(5);
        base.OnInspectorGUI();
    }
}


