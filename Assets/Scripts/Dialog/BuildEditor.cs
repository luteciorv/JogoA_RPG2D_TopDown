using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(DialogueSettings))]
public class BuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var dialogueSettings = (DialogueSettings)target;
        
        var dialogueLanguages = new DialogueLanguages
        {
            Portuguese = dialogueSettings.Sentence
        };

        var dialogueSentence = new DialogueSentence
        {
            Profile = dialogueSettings.SpeakerSprite,
            Sentence = dialogueLanguages
        };

        if(GUILayout.Button("Create Dialogue"))
        {
            if (!string.IsNullOrEmpty(dialogueSettings.Sentence))
            {
                dialogueSettings.Dialogues.Add(dialogueSentence);
                dialogueSettings.Clear();
            }
            else
                Debug.Log("Informe uma sentença para poder criar o diálogo");
        }
    }

}
#endif
