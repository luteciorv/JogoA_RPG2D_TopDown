using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialog", menuName = "New Dialog/Dialogue Scriptable Object")]
public class DialogueSettings : ScriptableObject
{
    [Header("Settings")]
    public GameObject Actor;

    [Header("Dialogue")]
    public Sprite SpeakerSprite;
    public string Sentence;

    public List<DialogueSentence> Dialogues = new();

    public void Clear()
    {
        SpeakerSprite = null;
        Sentence = string.Empty;
    }

    public string[] GetSentences()
    {
        var sentences = new List<string>();

        foreach(var dialogue in Dialogues)
        {
            switch(GameManager.Instance.Language)
            {
                case ELanguage.Portuguese:
                    sentences.Add(dialogue.Sentence.Portuguese);
                    break;

                case ELanguage.English:
                    sentences.Add(dialogue.Sentence.English);
                    break;

                case ELanguage.Spanish:
                    sentences.Add(dialogue.Sentence.Spanish);
                    break;

                default:
                    throw new Exception("Nenhum idioma foi selecionado");
            }
        }

        return sentences.ToArray();
    }
}
