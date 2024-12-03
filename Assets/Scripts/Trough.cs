using UnityEngine;

public class Trough : MonoBehaviour
{
    public Animator animator; // Reference to the animator for the trough
    public bool isFull = false; // Tracks if the trough is full
    

    public void FillTrough()
    {
        if (!isFull)
        {
            isFull = true;
            animator.SetBool("isFull", isFull); // Trigger the animation
            Debug.Log("Trough filled with water."+ isFull);
        }
    }

    // Method to check if the trough is full
    public bool IsFull()
    {
        return isFull;
    }
}
