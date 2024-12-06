using UnityEngine;

public class NPC : MonoBehaviour
{
    private bool canTalk = false;
    private bool isCooldownActive = false; // Tracks if cooldown is active
    private Dialogue currentDialogue;

    [SerializeField] private Dialogue defaultDialogue; // Default dialogue
    [SerializeField] private Dialogue specialDialogue; // Special dialogue
    [SerializeField] private float talkCooldown = 2f;  // Cooldown duration in seconds

    public Trough trough;
    public MovementTest player;
    public bool talked = false;
    void Start()
    {
        trough = FindFirstObjectByType<Trough>();
    }

    void Update()
    {
        // Handle dialogue activation
        if (canTalk && !isCooldownActive && Input.GetKeyUp(KeyCode.E))
        {
            
            if (trough != null && trough.IsFull())
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
                talked = true;
                currentDialogue.StartDialogue();
                StartCoroutine(StartTalkCooldown()); // Start the cooldown after talking
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
            eKeyTransform.gameObject.SetActive(isActive && !isCooldownActive); // Only active if not on cooldown
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canTalk = true;
            ActivateEKey(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canTalk = false;
            ActivateEKey(false);
            currentDialogue = null; // Clear reference
        }
    }

    private System.Collections.IEnumerator StartTalkCooldown()
    {
        isCooldownActive = true;
        ActivateEKey(false); // Disable the "E" key during cooldown
        yield return new WaitForSeconds(talkCooldown);
        isCooldownActive = false;
        if (canTalk) ActivateEKey(true); // Reactivate "E" key if player is still in range
    }
}
