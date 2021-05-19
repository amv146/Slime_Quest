using UnityEditor;
using UnityEngine;
/*
*   Name: Luke Driscoll, Mark Griffin, Alex Vallone, Grant Ward
*   ID: 2344496, 2340502, 2343966
*   Email: ldriscoll@chapman.edu, magriffin@chapman.edu, vallone@chapman.edu
*   Class: CPSC244-01
*   Final Project
*   This is my own work. I did not cheat on this assignment
*   This class controls the Conversation Drawer
*/
// [CustomPropertyDrawer(typeof(ConversationPiece))]
public class ConversationPieceDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 190;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var rect = position;
        rect.height = 16;
        EditorGUI.BeginProperty(rect, label, property);
        rect.width = position.width * 0.2f;
        EditorGUIUtility.labelWidth = 32;
        EditorGUI.PropertyField(rect, property.FindPropertyRelative("id"), new GUIContent("ID"));
        rect.x += rect.width;
        EditorGUI.PropertyField(rect, property.FindPropertyRelative("image"), GUIContent.none);
        rect.x += rect.width - 16;
        rect.x = position.width - rect.xMax;
        rect.width = position.width - rect.x;
        rect.height = 64;
        rect = EditorGUI.PrefixLabel(rect, new GUIContent("Text"));
        EditorGUI.PropertyField(rect, property.FindPropertyRelative("text"), GUIContent.none);
        rect.x += rect.height;
        EditorGUI.PropertyField(rect, property.FindPropertyRelative("options"), true);
        EditorGUI.EndProperty();
    }
}

// [CustomPropertyDrawer(typeof(ConversationOption))]
public class ConversationOptionDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 16;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var rect = position;
        rect.height = 16;
        rect.width = position.width * 0.25f;
        EditorGUI.BeginProperty(rect, label, property);
        EditorGUI.PropertyField(rect, property.FindPropertyRelative("nextDialogueID"), GUIContent.none);
        rect.x += rect.width;
        rect.width = 72;
        EditorGUI.PropertyField(rect, property.FindPropertyRelative("image"), GUIContent.none);
        rect.x += rect.width;
        rect.width = position.width * 0.25f;
        EditorGUI.PropertyField(rect, property.FindPropertyRelative("text"), GUIContent.none);
        rect.x += rect.width;
        EditorGUI.PropertyField(rect, property.FindPropertyRelative("enabled"), GUIContent.none);
        rect.y += 20;
        EditorGUI.PropertyField(rect, property.FindPropertyRelative("optionEvents"), GUIContent.none);
        EditorGUI.EndProperty();
    }

}