using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Components")]
    public GameObject DialogueWindow;

    [Header("Dialogue")]
    public Text ActorNameText;
    public Image ProfileSprite;
    public TextMeshProUGUI SpeechText;

    [Header("Settings")]
    public float TypingSpeed;

    private bool _windowVisible;
    private int _sentenceIndex = 0;
    private string[] _sentences = Array.Empty<string>();

    public static DialogueManager Instance;

    public void Awake()
    {
        Instance = this;
    }

    private IEnumerator TypeSentence()
    {
        foreach(var letter in _sentences[_sentenceIndex].ToCharArray())
        {
            SpeechText.text += letter;

            yield return new WaitForSeconds(TypingSpeed);
        }
    }

    public void NextSentence()
    {
        // Verificar se a frase já acabou
        if(SpeechText.text == _sentences[_sentenceIndex])
        {
            SpeechText.text = string.Empty;

            // Verificar se tem outras frases
            if (_sentenceIndex < _sentences.Length - 1)
            {
                _sentenceIndex++;
                StartCoroutine(TypeSentence());
            }
            else
                EndDialogue();
        }
    }

    public void Speak(string[] sentences)
    {
        if (sentences.Length == 0)
            throw new Exception("Nenhuma fala foi passada para o diálogo");

        if(_windowVisible)
            NextSentence();

        else
        {
            DialogueWindow.SetActive(true);
            _sentences = sentences;

            SpeechText.text = string.Empty;
            StartCoroutine(TypeSentence());
            _windowVisible = true; 
        }
    }

    public void EndDialogue()
    {
        _sentenceIndex = 0;
        _windowVisible = false;
        DialogueWindow.SetActive(false);
        _sentences = Array.Empty<string>();
    }
}
