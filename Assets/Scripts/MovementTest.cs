using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementTest : MonoBehaviour
{
    private Rigidbody2D rb;

    private bool canTalk = false;
    private Dialogue currentDialogue;
    private float horizontal;
    private float vertical;

    public float runSpeed = 20.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Handle dialogue activation
        if (canTalk && Input.GetKeyDown(KeyCode.E))
        {
            ActivateEKey(false);
            if (currentDialogue != null && !currentDialogue.IsDialogueActive())
            {
                currentDialogue.StartDialogue();
            }
        }

        // Movement input
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        // Stop movement if dialogue is active
        if (currentDialogue != null && currentDialogue.IsDialogueActive())
        {
            rb.linearVelocity = Vector2.zero;
        }
        else
        {
            rb.linearVelocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
        }
    }

    private void ActivateEKey(bool isActive, Transform npcTransform = null)
    {
        npcTransform = npcTransform ?? currentDialogue?.transform;
        Transform eKeyTransform = npcTransform?.Find("eKey");

        if (eKeyTransform != null)
        {
            eKeyTransform.gameObject.SetActive(isActive);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("NPC"))
        {
            ActivateEKey(true, collision.transform);
            canTalk = true;
            currentDialogue = collision.GetComponent<Dialogue>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("NPC"))
        {
            ActivateEKey(false, collision.transform);
            canTalk = false;
            currentDialogue = null; // Clear reference
        }
    }
}
