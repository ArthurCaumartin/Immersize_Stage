using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Immersize.Editor {
#if UNITY_EDITOR
    public abstract class ScriptableObjectEditor<T> : UnityEditor.Editor where T : ScriptableObject {
        protected GUIStyle HeaderStyle, BoxStyle, FieldStyle, ButtonStyle, ShadowStyle, DividerStyle;

        protected virtual Color Accent => new(0.22f, 0.44f, 0.95f, 1f);
        protected virtual Color BoxBg => new(.3f, .3f, .3f, 1f);
        protected virtual Color ShadowBg => new(0.88f, 0.88f, 0.94f, 0.2f);
        protected virtual Color DividerColor => new(0.85f, 0.85f, 0.9f, 1f);

        private Vector2 scrollPos;

        public override void OnInspectorGUI() {
            InitStyles();
            serializedObject.Update();

            EditorGUILayout.BeginVertical(ShadowStyle);
            GUILayout.Space(8);
            EditorGUILayout.BeginVertical(BoxStyle);
            GUILayout.Space(8);

            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            DrawHeader();
            DrawDivider();

            DrawSerializedFields();
            DrawExtraFields();
            DrawPreviews();

            DrawDivider();
            DrawCenteredButton("Save", SaveAsset);

            EditorGUILayout.EndScrollView();

            GUILayout.Space(8);
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();
        }

        protected virtual void InitStyles() {
            HeaderStyle ??= new GUIStyle(EditorStyles.boldLabel) {
                fontSize = 22,
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = Color.black }
            };

            BoxStyle ??= new GUIStyle(GUI.skin.box) {
                normal = { background = MakeTex(2, 2, BoxBg) },
                padding = new RectOffset(16, 16, 16, 16),
                margin = new RectOffset(16, 16, 16, 16)
            };

            ShadowStyle ??= new GUIStyle {
                normal = { background = MakeTex(2, 2, ShadowBg) },
                padding = new RectOffset(8, 8, 8, 8),
                margin = new RectOffset(8, 8, 8, 8)
            };

            DividerStyle ??= new GUIStyle {
                normal = { background = MakeTex(1, 2, DividerColor) },
                margin = new RectOffset(0, 0, 8, 8),
                fixedHeight = 2
            };

            FieldStyle ??= new GUIStyle(EditorStyles.textField) {
                fontSize = 15,
                normal = { textColor = Color.black },
                padding = new RectOffset(12, 12, 8, 8),
                border = new RectOffset(8, 8, 8, 8)
            };

            ButtonStyle ??= CreateButtonStyle();
        }

        protected virtual void DrawHeader() {
            if (target != null) EditorGUILayout.LabelField(target.name, HeaderStyle);
        }

        protected virtual void DrawDivider() {
            GUILayout.Space(8);
            GUILayout.Box(GUIContent.none, DividerStyle);
            GUILayout.Space(8);
        }

        protected virtual void DrawSerializedFields() {
            var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(f => IsSerialized(f) && !GetHiddenFields().Contains(f.Name));

            foreach (var field in fields) {
                var prop = serializedObject.FindProperty(field.Name);
                if (prop != null)
                    EditorGUILayout.PropertyField(prop, new GUIContent(ObjectNames.NicifyVariableName(field.Name)), true);
            }
        }

        protected virtual void DrawExtraFields() { }
        protected virtual void DrawPreviews() { }

        protected virtual string[] GetHiddenFields() => Array.Empty<string>();

        protected void DrawCenteredButton(string label, Action onClick) {
            GUILayout.Space(8);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (GUILayout.Button(label, ButtonStyle, GUILayout.Height(36), GUILayout.MinWidth(120)))
                onClick?.Invoke();

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        protected GUIStyle CreateButtonStyle() {
            var style = new GUIStyle(GUI.skin.button) {
                fontSize = 16,
                fontStyle = FontStyle.Bold,
                border = new RectOffset(12, 12, 12, 12),
                margin = new RectOffset(12, 12, 12, 12),
                padding = new RectOffset(16, 16, 12, 12)
            };

            style.normal.textColor = Color.black;
            style.normal.background = MakeTex(2, 2, Accent);
            style.hover.background = MakeTex(2, 2, new Color(Accent.r, Accent.g, Accent.b, 0.85f));
            style.active.background = MakeTex(2, 2, new Color(Accent.r, Accent.g, Accent.b, 0.7f));

            return style;
        }

        protected static Texture2D MakeTex(int width, int height, Color color) {
            var tex = new Texture2D(width, height, TextureFormat.RGBA32, false);
            tex.SetPixels(Enumerable.Repeat(color, width * height).ToArray());
            tex.Apply();
            return tex;
        }

        protected static bool IsSerialized(FieldInfo field) =>
            field.IsPublic || Attribute.IsDefined(field, typeof(SerializeField));

        protected void SaveAsset() {
            EditorUtility.SetDirty(target);
            AssetDatabase.SaveAssets();
        }

        protected SerializedProperty this[string propertyName] => serializedObject.FindProperty(propertyName);
    }
#endif
}
