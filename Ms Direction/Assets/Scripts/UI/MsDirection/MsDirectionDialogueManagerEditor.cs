using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MsDirectionDialogueManager))]
public class MsDirectionDialogueManagerEditor : Editor
{
    private SerializedProperty popups;

    private void OnEnable()
    {
        popups = serializedObject.FindProperty("popups");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("UI References", EditorStyles.boldLabel);
        Draw("dialogueRoot");
        Draw("dialogueLabel");
        Draw("portraitImage");
        Draw("msDirectionPortrait");

        EditorGUILayout.Space(12);
        EditorGUILayout.LabelField("Ms. Direction Popups", EditorStyles.boldLabel);

        for (int i = 0; i < popups.arraySize; i++)
        {
            SerializedProperty popup = popups.GetArrayElementAtIndex(i);
            string popupId = popup.FindPropertyRelative("id").stringValue;
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            popup.isExpanded = EditorGUILayout.Foldout(popup.isExpanded,
                "Popup " + i + (string.IsNullOrEmpty(popupId) ? "" : " — " + popupId), true);

            if (popup.isExpanded)
            {
                Field(popup, "id", "Popup ID");
                Field(popup, "dialogueText", "Dialogue Text");
                Field(popup, "duration", "Duration (Seconds)");
                Field(popup, "triggerOnlyOnce", "Trigger Only Once");

                EditorGUILayout.Space(6);
                EditorGUILayout.LabelField("Popup Conditions", EditorStyles.boldLabel);

                Condition(popup, "onTaskActivation", "Task Activation", "activationTaskId", "Task ID");
                Condition(popup, "requireActiveTask", "Require Active Task", "activeTaskId", "Required Task ID");
                Field(popup, "onTriggerHit", "Trigger Hit");
                Condition(popup, "onTaskCompletion", "Task Completion", "completionTaskId", "Task ID");

                Field(popup, "timedFollowUp", "Timed Follow-up");
                if (popup.FindPropertyRelative("timedFollowUp").boolValue)
                {
                    Field(popup, "afterPopupId", "After Popup ID");
                    Field(popup, "followUpDelay", "Delay (Seconds)");
                }

                Field(popup, "onVendorEvent", "Vendor Event (UnityEvent)");
                if (popup.FindPropertyRelative("onVendorEvent").boolValue)
                    EditorGUILayout.HelpBox("In InteractableVendor > Responses > On Success, select the Dialogue Manager's VendorEvent(string) method and enter this popup's ID.", MessageType.Info);
                if (popup.FindPropertyRelative("onTriggerHit").boolValue)
                    EditorGUILayout.HelpBox("Add MsDirectionTrigger to a trigger collider and enter this popup's ID.", MessageType.Info);

                EditorGUILayout.Space(5);
                if (GUILayout.Button("Remove Popup"))
                {
                    popups.DeleteArrayElementAtIndex(i);
                    EditorGUILayout.EndVertical();
                    break;
                }
            }
            EditorGUILayout.EndVertical();
        }

        if (GUILayout.Button("+ Add Popup"))
        {
            int index = popups.arraySize;
            popups.InsertArrayElementAtIndex(index);
            SerializedProperty fresh = popups.GetArrayElementAtIndex(index);
            fresh.FindPropertyRelative("id").stringValue = "";
            fresh.FindPropertyRelative("dialogueText").stringValue = "";
            fresh.FindPropertyRelative("duration").floatValue = 5f;
            fresh.FindPropertyRelative("triggerOnlyOnce").boolValue = true;
            foreach (string condition in new[] { "onTaskActivation", "requireActiveTask", "onTriggerHit", "onTaskCompletion", "timedFollowUp", "onVendorEvent" })
                fresh.FindPropertyRelative(condition).boolValue = false;
            fresh.FindPropertyRelative("activationTaskId").stringValue = "";
            fresh.FindPropertyRelative("activeTaskId").stringValue = "";
            fresh.FindPropertyRelative("completionTaskId").stringValue = "";
            fresh.FindPropertyRelative("afterPopupId").stringValue = "";
            fresh.FindPropertyRelative("followUpDelay").floatValue = 3f;
            fresh.isExpanded = true;
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void Draw(string name) => EditorGUILayout.PropertyField(serializedObject.FindProperty(name));

    private static void Field(SerializedProperty parent, string name, string label)
    {
        EditorGUILayout.PropertyField(parent.FindPropertyRelative(name), new GUIContent(label), true);
    }

    private static void Condition(SerializedProperty parent, string toggle, string label, string field, string fieldLabel)
    {
        Field(parent, toggle, label);
        if (parent.FindPropertyRelative(toggle).boolValue)
        {
            EditorGUI.indentLevel++;
            Field(parent, field, fieldLabel);
            EditorGUI.indentLevel--;
        }
    }
}
