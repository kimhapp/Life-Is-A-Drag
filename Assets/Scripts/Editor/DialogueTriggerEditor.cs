using UnityEditor;

[CustomEditor(typeof(DialogueTrigger))]
public class DialogueTriggerEditor : Editor
{
    SerializedProperty yarnNodeName;
    SerializedProperty dialogueType;
    SerializedProperty interactableIndicator;

    private void OnEnable()
    {
        yarnNodeName = serializedObject.FindProperty("yarnNodeName");
        dialogueType = serializedObject.FindProperty("dialogueType");
        interactableIndicator = serializedObject.FindProperty("interactableIndicator");
    }

    public override void OnInspectorGUI()
    {
        DialogueTrigger dialogueTrigger = (DialogueTrigger)target;

        serializedObject.Update();
        
        EditorGUILayout.PropertyField(yarnNodeName);
        EditorGUILayout.PropertyField(dialogueType);

        if (dialogueTrigger.dialogueType == DialogueTrigger.Type.Interact)
        {
            EditorGUILayout.PropertyField(interactableIndicator);
        }
        
        serializedObject.ApplyModifiedProperties();
    }
}
