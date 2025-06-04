using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CharacterManager)), CanEditMultipleObjects]
public class CharacterManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CharacterManager c = target as CharacterManager;
        if (GUILayout.Button("Bake Character Data"))
            c.Bake();
        GUILayout.Space(5);
        base.OnInspectorGUI();
    }
}


