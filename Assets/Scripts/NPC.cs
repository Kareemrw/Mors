using UnityEngine;

public class NPC : MonoBehaviour
{
    private bool canTalk = false;
    private Dialogue currentDialogue;

    [SerializeField] private Dialogue defaultDialogue; // Default dialogue
    [SerializeField] private Dialogue specialDialogue; // Special dialogue
    

    public Trough trough;
    public MovementTest player;

    void Start()
    {
        trough =  FindFirstObjectByType<Trough>();
    }

    void Update()
    {
        // Handle dialogue activation
        if (canTalk && Input.GetKeyDown(KeyCode.E))
        {
            if (trough.IsFull())
            {
                currentDialogue = specialDialogue;
            }
            else
            {
                currentDialogue = defaultDialogue;
            }
            ActivateEKey(false);
            if (currentDialogue != null && !currentDialogue.IsDialogueActive())
            {
                currentDialogue.StartDialogue();
            }
        }

        if (currentDialogue != null && !currentDialogue.IsDialogueActive())
        {
            ActivateEKey(true); // Bring back "E" indicator if dialogue ended
        }
    }

    private void ActivateEKey(bool isActive)
    {
        Transform eKeyTransform = transform.Find("eKey");
        if (eKeyTransform != null)
        {
            eKeyTransform.gameObject.SetActive(isActive);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ActivateEKey(true);
            canTalk = true;

            // Check condition and set dialogue accordingly
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ActivateEKey(false);
            canTalk = false;
            currentDialogue = null; // Clear reference
        }
    }
}
