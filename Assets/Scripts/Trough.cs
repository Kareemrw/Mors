using UnityEngine;

public class Trough : MonoBehaviour
{
    public Animator animator; // Reference to the animator for the trough
    public bool isFull = false; // Tracks if the trough is full

    public GameObject filledBucket;


    public void FillTrough()
    {
        if (!isFull)
        {
            isFull = true;
            animator.SetBool("isFull", isFull); // Trigger the animation
            Debug.Log("Trough filled with water."+ isFull);

            filledBucket.SetActive(false);
        }
    }

    // Method to check if the trough is full
    public bool IsFull()
    {
        return isFull;
    }
}
