using UnityEngine;

public class PickUpExample : MonoBehaviour
{
    public string interactionMessage = "Press E to pick up the bucket";
    public MovementTest1 player;
    private bool isPlayerNearby = false;

    public GameObject emptyBucket;

    void Start()
    {
        emptyBucket.SetActive(false);
    }
    void Update()
    {
        // Check for player input
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            PickUp();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ActivateFKey(true);
            isPlayerNearby = true;
            // Display interaction message (e.g., UI or dialogue)
            Debug.Log(interactionMessage);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ActivateFKey(false);
            isPlayerNearby = false;
            // Hide interaction message
            Debug.Log("Player left the interaction zone.");
        }
    }

    void PickUp()
    {
        // Handle bucket pickup logic (e.g., add to inventory)
        Debug.Log("Bucket picked up!");
        gameObject.SetActive(false); // Hide the bucket
        //player = FindObjectOfType<MovementTest>();
        //player.hasItem = true;

        emptyBucket.SetActive(true);
    }
    private void ActivateFKey(bool isActive)
    {
        Transform eKeyTransform = transform.Find("fKey");
        if (eKeyTransform != null)
        {
            eKeyTransform.gameObject.SetActive(isActive);
        }
    }
}
