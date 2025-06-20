using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(CharacterBaker)), CanEditMultipleObjects]
public class CharacterManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Bake Character Data"))
        {
            CharacterBaker characterManager = target as CharacterBaker;
            characterManager.Bake();

            EditorUtility.SetDirty(characterManager);
            if (!Application.isPlaying)
                EditorSceneManager.MarkSceneDirty(characterManager.gameObject.scene);

            //TODO faire une list dans l'editeur
            foreach (var item in characterManager.GetComponentsInChildren<TextMeshProUGUI>())
            {
                item.ForceSerializeTextField(item.text);
            }

            Undo.RegisterCompleteObjectUndo(characterManager, "Bake");
        }
        GUILayout.Space(5);
        base.OnInspectorGUI();
    }
}


