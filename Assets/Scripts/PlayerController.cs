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
    Vector2 moveDirection;

    void Awake()
    {
        characterRb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();

        crossfade = GameObject.FindWithTag("Crossfade").GetComponent<Crossfade>();
        if (crossfade == null) Debug.LogError("Crossfade is missing!");

        dialogueRunner = GameObject.FindWithTag("DialogueSystem").GetComponent<DialogueRunner>();
        if (dialogueRunner == null) Debug.LogError("DialogueRunner is missing!");
    }

    void OnEnable()
    {
        if (crossfade != null)
        {
            crossfade.onBeginCrossfade += cannotControl;
            crossfade.onEndCrossfade += canControl;
        }

        if (dialogueRunner != null)
        {
            dialogueRunner.onDialogueStart.AddListener(cannotControl);
            dialogueRunner.onDialogueComplete.AddListener(canControl);
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
        if (controllable)
        {
            moveDirection = move.action.ReadValue<Vector2>();
            CheckMovement();
            characterRb.linearVelocity = speed * moveDirection;
        }
    }

    void OnDisable()
    {
        if (crossfade != null)
        {
            crossfade.onBeginCrossfade -= cannotControl;
            crossfade.onEndCrossfade -= canControl;
        }

        if (dialogueRunner != null)
        {
            dialogueRunner.onDialogueStart.RemoveListener(cannotControl);
            dialogueRunner.onDialogueComplete.RemoveListener(canControl);
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

    void cannotControl()
    {
        controllable = false;
        playerAnimator.SetBool("Walk", false); // In case the player was walking when interact with the objects
    }

    void canControl()
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
