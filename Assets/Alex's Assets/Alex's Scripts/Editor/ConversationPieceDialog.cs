using System;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEditor.SceneManagement;

public class ConversationPieceDialog : ScriptableWizard
{

    public ConversationPiece conversationPiece, originalConversationPiece;

    public ReorderableList options;
    ConversationScript conversationScript;
    bool isUpdate = false;
    string[] targets;

    internal static void New(ConversationScript conversationScript)
    {
        var w = ScriptableWizard.DisplayWizard<ConversationPieceDialog>("New Conversation Piece", "Create");
        w.conversationScript = conversationScript;

        List<string> tempTargets = new List<string>();
        tempTargets.Add("None");
        foreach (ConversationPiece conversationPiece in conversationScript.items) {
            tempTargets.Add(conversationPiece.id);
        }

        w.targets = tempTargets.ToArray();
        w.conversationPiece = new ConversationPiece() { id = "", text = "", options = new List<ConversationOption>() };
        w.isUpdate = false;
    }

    internal static void Edit(ConversationScript conversationScript, ConversationPiece conversationPiece)
    {
        var w = ScriptableWizard.DisplayWizard<ConversationPieceDialog>("Edit Conversation Piece", "Update");
        Debug.Log(w.targets);

        List<string> tempTargets = new List<string>();
        tempTargets.Add("None");
        foreach (ConversationPiece tempConversationPiece in conversationScript.items) {
            if (tempConversationPiece.id != conversationPiece.id) {
                tempTargets.Add(tempConversationPiece.id);
            }
        }

        w.targets = tempTargets.ToArray();
        w.originalConversationPiece = conversationPiece;
        w.conversationPiece = conversationPiece;
        w.conversationScript = conversationScript;
        w.isUpdate = true;
    }

    void BuildOptionList()
    {
        options = new ReorderableList(conversationPiece.options, typeof(ConversationOption), true, true, true, true);
        options.elementHeight = 2.5f * EditorGUIUtility.singleLineHeight;
        options.drawElementCallback = OnDrawOption;
        options.drawHeaderCallback = OnDrawOptionHeader;
    }

    void OnDrawOptionHeader(Rect rect)
    {
        GUI.Label(rect, "Branches");
    }

    void OnDrawOption(Rect rect, int index, bool isActive, bool isFocused)
    {
        var item = conversationPiece.options[index];
        var targetIndex = System.Array.IndexOf(targets, item.nextDialogueID);

        if (targetIndex < 0) {
            targetIndex = 0;
        }

        var optionRect = rect;
        optionRect.height = 16;
        optionRect.width = rect.width * 0.2f;

        targetIndex = EditorGUI.Popup(optionRect, targetIndex, targets);

        item.nextDialogueID = targets[targetIndex];

        optionRect.x += optionRect.width * 2;
        optionRect.width = rect.width * 0.6f;

        item.text = EditorGUI.TextField(optionRect, item.text);

        optionRect.x = rect.x;
        optionRect.y += optionRect.height * 1.2f;
        optionRect.width = rect.width * 0.2f;

        if (GUI.Button(optionRect, "More", EditorStyles.miniButton)) {
            ConversationOptionEditor.More(conversationScript, conversationPiece, item);
        }



        conversationPiece.options[index] = item;
    }

    private void OnAdd(ReorderableList list)
    {
        list.list.Add(new ConversationOption() { nextDialogueID = "", text = "" });
    }

    void OnWizardCreate()
    {
        if (isUpdate)
        {
            Undo.RecordObject(conversationScript, "Update Item");
            conversationScript.Set(originalConversationPiece, conversationPiece);
            EditorUtility.SetDirty(conversationScript);
        }
        else
        {
            Undo.RecordObject(conversationScript, "Add Item");
            conversationScript.Add(conversationPiece);
            EditorUtility.SetDirty(conversationScript);
        }
    }

    void Update()
    {

    }

    protected override bool DrawWizardGUI()
    {
        if (Event.current.isKey && Event.current.keyCode == KeyCode.Escape)
        {
            Close();
            return true;
        }
        isValid = true;
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PrefixLabel("ID");
        conversationPiece.id = EditorGUILayout.TextField(conversationPiece.id).Trim().ToUpper();
        if (conversationPiece.id.Length == 0)
        {
            EditorGUILayout.HelpBox("The ID field cannot be empty.", MessageType.Error);
            isValid = false;
        }
        else if (isUpdate && conversationPiece.id != originalConversationPiece.id)
        {
            if (conversationScript.ContainsKey(conversationPiece.id))
            {
                EditorGUILayout.HelpBox("This ID already exists in this conversation and cannot be saved.", MessageType.Error);
                isValid = false;
            }
            else
            {
                EditorGUILayout.HelpBox("ID has changed and will be updated in related records", MessageType.Warning);
            }
        }

        EditorGUILayout.PrefixLabel("Image (Optional)");
        conversationPiece.image = (Sprite)EditorGUILayout.ObjectField(conversationPiece.image, typeof(Sprite), false);
        EditorGUILayout.PrefixLabel("Text");
        conversationPiece.text = EditorGUILayout.TextArea(conversationPiece.text);

        if (options == null || options.count == 0) {
            var targetIndex = System.Array.IndexOf(targets, conversationPiece.nextDialogueID);
            if (targetIndex < 0) {
                targetIndex = 0;
            }

            EditorGUILayout.PrefixLabel("Next Dialogue ID");
            int length = targets.Length + 1;
            List<string> dialogueIDOptions = new List<string>();
            for (int i = 0; i < targets.Length; ++i) {
                if (targets[i] != conversationPiece.id) {
                    dialogueIDOptions.Add(targets[i]);
                }
            }
            conversationPiece.nextDialogueID = targets[EditorGUILayout.Popup(targetIndex, dialogueIDOptions.ToArray())];
        }
        

        if (conversationScript.items.Count > 0)
        {
            if (options == null) BuildOptionList();
            options.DoLayoutList();
        }

        return EditorGUI.EndChangeCheck();
    }
}