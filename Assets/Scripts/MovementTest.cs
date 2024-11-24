using UnityEngine;

public class MovementTest : MonoBehaviour
{
    Rigidbody2D rb;

    float horizontal;
    float vertical;

    public float runSpeed = 20.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.name);
    }
}
