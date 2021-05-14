using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using UnityEditor.SceneManagement;
using System.Linq;

public class ConversationOptionEditor : ScriptableWizard
{
    public ConversationScript conversationScript;
    public ConversationOption conversationOption;
    public ConversationPiece conversationPiece;
    public string[] targets;

    internal static void More(ConversationScript conversationScript, ConversationPiece conversationPiece, ConversationOption conversationOption) {
        var w = ScriptableWizard.DisplayWizard<ConversationOptionEditor>("More Options", "Update");
        w.conversationScript = conversationScript;
        w.conversationOption = conversationOption;
        w.targets = (from i in conversationScript.items select i.id).ToArray();
        w.conversationPiece = conversationPiece;
    }

    protected override bool DrawWizardGUI()
    {
        var targetIndex = System.Array.IndexOf(targets, conversationOption.nextDialogueID);
        if (Event.current.isKey && Event.current.keyCode == KeyCode.Escape)
        {
            Close();
            return true;
        }
        isValid = true;
        EditorGUI.BeginChangeCheck();

        EditorGUILayout.PrefixLabel("Next Dialogue ID");
        int length = targets.Length + 1;
        List<string> dialogueIDOptions = new List<string>();
        dialogueIDOptions.Add("None");
        for (int i = 0; i < targets.Length; ++i) {
            dialogueIDOptions.Add(targets[i]);
        }
        conversationOption.nextDialogueID = targets[EditorGUILayout.Popup(0, dialogueIDOptions.ToArray())];


        EditorGUILayout.PrefixLabel("Text");
        conversationOption.text = EditorGUILayout.TextArea(conversationOption.text);

        SerializedObject newObject = new SerializedObject(conversationOption);
        EditorGUILayout.PrefixLabel("Option Events");
        EditorGUILayout.PropertyField((newObject).FindProperty("optionEvent"), true);

        newObject.ApplyModifiedProperties();


        return EditorGUI.EndChangeCheck();
    }

    void OnWizardCreate() {
        Undo.RecordObject(conversationScript, "Update Item");
        EditorUtility.SetDirty(conversationScript);
    }
}
