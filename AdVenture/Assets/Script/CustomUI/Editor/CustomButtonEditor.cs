using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(CustomButton))]
public class CustomButtonEditor : ButtonEditor
{
    SerializedProperty prop;

    protected override void OnEnable()
    {
        base.OnEnable();
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();

        DrawProperty("selectable");
        if (prop.boolValue)
            DrawProperty("isSelect");
        else
            DrawProperty("resetOnClick");

        DrawLabel("Tweening");
        DrawProperty("slideOnHover");
        if (prop.boolValue)
            DrawProperty("transitionOnHover");
        DrawProperty("scaleOnHover");

        if (GUILayout.Button("SET_ORIGINAL_VALUES"))
        {
            CustomButton btn = target as CustomButton;
            btn.SetOriginalValues();
        }

        DrawLabel("Text");
        DrawProperty("text");
        if (prop.objectReferenceValue != null)
            DrawProperty("textColor");
        DrawLabel("Border");
        DrawProperty("border");
        if (prop.objectReferenceValue != null)
            DrawProperty("borderColor");

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawLabel(string labelName)
    {
        EditorGUILayout.LabelField(labelName);
    }

    private void DrawProperty(string propName)
    {
        prop = serializedObject.FindProperty(propName);

        EditorGUILayout.PropertyField(prop);
    }
}
