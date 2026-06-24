using UnityEngine;
using UnityEngine.InputSystem;

public interface IInteractable
{
    public void Interact();
}

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] InputActionReference move;
    [SerializeField] InputActionReference interact;

    [SerializeField] float speed = 5.0f;
    [SerializeField] GameObject spawnPoint;

    [HideInInspector] public bool IsInRangeToInteract = false;
    [HideInInspector] public IInteractable interactable;

    Rigidbody characterRb;
    Animator playerAnimator;

    bool isFacingLeft = false;
    bool controllable = true;
    Vector2 moveDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterRb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();

        transform.position = spawnPoint.transform.position;
    }

    private void FixedUpdate()
    {
        if (controllable)
        {
            moveDirection = move.action.ReadValue<Vector2>();
            CheckMovement();
            characterRb.linearVelocity = speed * moveDirection;
        }
    }

    private void OnEnable()
    {
        interact.action.started += Interact;
    }

    void Interact(InputAction.CallbackContext callback)
    {
        if (IsInRangeToInteract && controllable)
        {
            interactable.Interact();
        }
    }

    public void cannotControl()
    {
        controllable = false;
        playerAnimator.SetBool("Walk", false); // In case the player was walking when interact with the objects
    }

    public void canControl()
    {
        controllable = true;
    }

    void CheckMovement()
    {
        if (moveDirection != Vector2.zero)
        {
            if (moveDirection.x < 0 && !isFacingLeft)
            {
                isFacingLeft = true;
                transform.rotation = Quaternion.Euler(0, -90, 0);
            }
            else if (moveDirection.x > 0 && isFacingLeft)
            {
                isFacingLeft = false;
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }

            playerAnimator.SetBool("Walk", true);
        }
        else
        {
            playerAnimator.SetBool("Walk", false);
        }
    }
}
