using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public float DialogueRange;
    public LayerMask PlayerLayer;

    public DialogueSettings Dialogue;

    private bool _speak;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _speak)
            DialogueManager.Instance.Speak(Dialogue.GetSentences());
    }

    private void FixedUpdate()
    {
        ShowDialogue();
    }

    private void ShowDialogue()
    {
        var collider2D = Physics2D.OverlapCircle(transform.position, DialogueRange, PlayerLayer);

        _speak = collider2D != null;
        if (!_speak)
            DialogueManager.Instance.EndDialogue();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, DialogueRange);
    }
}
