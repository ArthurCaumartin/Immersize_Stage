using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

using Immersize.Editor;
using Entity.Weapons;

[CustomEditor(typeof(ScriptAbleWeaponData))]
public class ScriptAbleWeaponDataEditor : ScriptableObjectEditor<ScriptAbleWeaponData> {
    protected override void DrawExtraFields() {
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Metadata", HeaderStyle);
        EditorGUILayout.PropertyField(this["icon"], new GUIContent("Weapon Icon"));
        EditorGUILayout.PropertyField(this["modelPrefab"], new GUIContent("3D Model"));
    }

    protected override void DrawPreviews() {
        DrawPreview(this["icon"], "Icon Preview");
        DrawPreview(this["modelPrefab"], "3D Model Preview");
    }

    protected override string[] GetHiddenFields() => new[] { "icon", "modelPrefab" };

    private void DrawPreview(SerializedProperty prop, string label) {
        if (prop?.objectReferenceValue == null) return;

        GUILayout.Space(8);
        EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
        Rect rect = GUILayoutUtility.GetRect(128, 128, GUILayout.ExpandWidth(false));
        GUI.Box(rect, GUIContent.none, BoxStyle);
        Rect texRect = new(rect.x + 8, rect.y + 8, rect.width - 16, rect.height - 16);

        _ = prop.objectReferenceValue switch {
            Sprite s when s.texture != null =>
                Run(() => GUI.DrawTexture(texRect, s.texture, ScaleMode.ScaleToFit)),
            GameObject go => Run(() => {
                var tex = AssetPreview.GetAssetPreview(go);
                GUI.DrawTexture(texRect, tex ?? Texture2D.grayTexture, ScaleMode.ScaleToFit);
            }),
            _ => Run(() => GUI.Label(texRect, "Unsupported", EditorStyles.centeredGreyMiniLabel))
        };

        GUILayout.Space(8);
    }

    private static object Run(Action a) {
        a(); return null;
    }
}