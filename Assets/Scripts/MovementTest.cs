using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementTest : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontal;
    private float vertical;
    public Animator animator;
    Vector2 movement;
    public float moveSpeed = 5f;
    public bool hasItem = false;
    public bool full = false;
    public int sceneNum = 1;
    private bool isPlayerNearby = false;
    public bool isIntroMonologueActive = true; // Flag for intro monologue
    private Trough nearbyTrough = null; // Reference to the nearby trough

    public GameObject emptyBucket;
    public GameObject filledBucket;
    public NPC npc;

    public Animator fade;
    public GameObject minimenu;

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
        if(sceneNum == 2 && npc.talked == true)
        {
            animator.SetBool("Tired", true);
        }


        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "Scene 5 Official" && npc.talked == true)
        {
            print("enters if cred");
            fade.speed = 0.5f;
            fade.SetTrigger("FadeOut");
            minimenu.SetActive(false);
            StartCoroutine(Pause());
        }
    }


    IEnumerator Pause()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("EndCredits");

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
        if ((sceneNum < 5)|| (sceneNum == 5 && npc.talked == false))
        {
            rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        }

        
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
        if(sceneNum == 4)
        {
            sceneNum++;
        }
        npc.talked = false;
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
            nearbyTrough = other.GetComponent<Trough>(); // Store the specific trough reference
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
                if(emptyBucket != null && filledBucket != null){
                    emptyBucket.SetActive(false);
                    filledBucket.SetActive(true);
                }
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

    public void PauseMovement(float speed)
    {
        speed = 0;
        moveSpeed = speed;
    }

    public void ResumeMovement(float speed)
    {
        speed = .5f;
        moveSpeed = speed;
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}
