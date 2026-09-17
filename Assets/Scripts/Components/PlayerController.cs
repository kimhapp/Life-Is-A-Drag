using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

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

    Crossfade crossfade;
    DialogueRunner dialogueRunner;
    Rigidbody characterRb;
    Animator playerAnimator;

    bool isFacingLeft = false;
    bool controllable = true;
    bool isDialogueActive = false;
    Vector2 moveDirection;

    void Awake()
    {
        characterRb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();

        crossfade = Crossfade.Instance;
        if (crossfade == null) Debug.LogError("Crossfade is missing!");

        dialogueRunner = DialogueSystem.Instance.DialogueRunner;
        if (dialogueRunner == null) Debug.LogError("DialogueRunner is missing!");
    }

    void OnEnable()
    {
        if (crossfade != null)
        {
            crossfade.OnBeginCrossfade += cannotControl;
            crossfade.OnEndCrossfade += canControl;
        }

        if (dialogueRunner != null)
        {
            dialogueRunner.onDialogueStart.AddListener(cannotControlOverride);
            dialogueRunner.onDialogueComplete.AddListener(canControlOverride);
        }

        interact.action.started += Interact;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = spawnPoint.transform.position;
    }

    void FixedUpdate()
    {
        if (!controllable)
        {
            characterRb.linearVelocity = Vector2.zero;
            return;
        }
        
        moveDirection = move.action.ReadValue<Vector2>();
        CheckMovement();
        characterRb.linearVelocity = speed * moveDirection;
    }

    void OnDisable()
    {
        if (crossfade != null)
        {
            crossfade.OnBeginCrossfade -= cannotControl;
            crossfade.OnEndCrossfade -= canControl;
        }

        if (dialogueRunner != null)
        {
            dialogueRunner.onDialogueStart.RemoveListener(cannotControlOverride);
            dialogueRunner.onDialogueComplete.RemoveListener(canControlOverride);
        }

        interact.action.started -= Interact;
    }

    void Interact(InputAction.CallbackContext callback)
    {
        if (IsInRangeToInteract && controllable)
        {
            interactable.Interact();
        }
    }

    void cannotControlOverride()
    {
        // Dialogue takes the priority as it cannot be outlived by crossfade
        // If it does, then there is something wrong with the setup
        isDialogueActive = true;
        controllable = false;
        playerAnimator.SetBool("Walk", false); // In case the player was walking when interact with the objects
    }

    void cannotControl()
    {
        if (!isDialogueActive)
        {
            controllable = false;
            playerAnimator.SetBool("Walk", false); // In case the player was walking when interact with the objects
        }
    }

    void canControlOverride()
    {
        isDialogueActive = false;
        controllable = true;
    }


    void canControl()
    {
        if (!isDialogueActive)
        {
            controllable = true;
        }
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
