
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rbCharacter;


    //cushion for border
    [SerializeField]
    private float screenCushion;

    private Camera _camera;



    void Start()
    {
        rbCharacter = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
    }



    void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        rbCharacter.linearVelocity = new Vector2(horizontalMovement, verticalMovement) * 8;

        PreventGoingOffScreen();
    }



    private void PreventGoingOffScreen()
    {
        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);

        if ((screenPosition.x < screenCushion && rbCharacter.linearVelocity.x < 0) ||
            (screenPosition.x > _camera.pixelWidth - screenCushion && rbCharacter.linearVelocity.x > 0))
        {
            rbCharacter.linearVelocity = new Vector2(0, rbCharacter.linearVelocity.y);
        }

        if ((screenPosition.y < screenCushion && rbCharacter.linearVelocity.y < 0) ||
            (screenPosition.y > _camera.pixelHeight - screenCushion && rbCharacter.linearVelocity.y > 0))
        {
            rbCharacter.linearVelocity = new Vector2(rbCharacter.linearVelocity.x, 0);
        }
    }
}
