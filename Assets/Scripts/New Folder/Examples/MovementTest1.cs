using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTest1 : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontal;
    private float vertical;
    public Animator animator;
    Vector2 movement;
    public float moveSpeed = 5f;
    public bool hasItem = false;
    public bool full = false;
    private bool isPlayerNearby = false;
    private bool isIntroMonologueActive = true; // Flag for intro monologue
    private Trough1 nearbyTrough = null; // Reference to the nearby trough

    public GameObject emptyBucket;
    public GameObject filledBucket;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(StartIntroMonologue());

        emptyBucket.SetActive(false);
        filledBucket.SetActive(false);
    }

    void Update()
    {
        // Prevent movement during intro monologue or active dialogues
        if (isIntroMonologueActive || IsAnyDialogueActive())
        {
            movement = Vector2.zero;
            animator.SetFloat("Speed", 0);
            return;
        }

        // Movement input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (movement.sqrMagnitude > 0)
        {
            animator.SetFloat("LastInputX", movement.x);
            animator.SetFloat("LastInputY", movement.y);
        }

        // Interact with objects
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            HandleInteraction();
        }
    }

    private void FixedUpdate()
    {
        // Prevent movement during intro monologue or active dialogues
        if (isIntroMonologueActive || IsAnyDialogueActive())
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        // Move the player
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    private IEnumerator StartIntroMonologue()
    {
        Dialogue introDialogue = GetComponent<Dialogue>();
        if (introDialogue != null)
        {
            introDialogue.StartDialogue();
            while (introDialogue.IsDialogueActive())
            {
                yield return null; // Wait until the monologue ends
            }
        }

        isIntroMonologueActive = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("River") && !hasItem)
        {
            ActivateFKey(true);
            isPlayerNearby = true;
        }
        else if (other.CompareTag("Trough") && hasItem)
        {
            ActivateFKey(true);
            isPlayerNearby = true;
            nearbyTrough = other.GetComponent<Trough1>(); // Store the specific trough reference
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("River") || other.CompareTag("Trough"))
        {
            ActivateFKey(false);
            isPlayerNearby = false;

            if (other.CompareTag("Trough"))
            {
                nearbyTrough = null; // Clear reference when leaving the trough
            }
        }
    }

    private void HandleInteraction()
    {
        if (isPlayerNearby)
        {
            if (!hasItem)
            {
                Debug.Log("Picked up water from the river.");
                hasItem = true;
                ActivateFKey(false);

                emptyBucket.SetActive(false);
                filledBucket.SetActive(true);
            }
            else if (hasItem && nearbyTrough != null && !nearbyTrough.IsFull())
            {
                Debug.Log("Filled the trough with water.");
                nearbyTrough.FillTrough();
                full = true;
                hasItem = false;
                ActivateFKey(false);

                filledBucket.SetActive(false);
            }
        }
    }

    private void ActivateFKey(bool isActive)
    {
        Transform fKeyTransform = transform.Find("fKey");
        if (fKeyTransform != null)
        {
            fKeyTransform.gameObject.SetActive(isActive);
        }
    }

    private bool IsAnyDialogueActive()
    {
        Dialogue[] dialogues = FindObjectsOfType<Dialogue>();
        foreach (Dialogue dialogue in dialogues)
        {
            if (dialogue.IsDialogueActive())
            {
                return true;
            }
        }
        return false;
    }
}
