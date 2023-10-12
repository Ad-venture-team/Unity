using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UIView))]
public class UIViewEditor : Editor
{
    SerializedProperty prop;
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawProperty("slideType");
        if (prop.enumValueIndex != (int)UITransition.NONE)
            DrawProperty("slideTime");

        DrawProperty("notInteractable");

        DrawProperty("isFade");
        if (prop.boolValue)
            DrawProperty("fadeTime");

        DrawProperty("isShow");


        if (GUILayout.Button("SHOW"))
        {
            UIView view = target as UIView;
            view.ForceShow();
        }

        if (GUILayout.Button("HIDE"))
        {
            UIView view = target as UIView;
            view.ForceHide();
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawProperty(string propName)
    {
        prop = serializedObject.FindProperty(propName);
        EditorGUILayout.PropertyField(prop);
    }
}
