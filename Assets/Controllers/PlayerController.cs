using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float groundDistance;
    public LayerMask terrainLayer;
    public Rigidbody rigidBody;
    public SpriteRenderer spriteRenderer;
    public PlayerAnimationController playerAnimationController;

    private string direction = "Front";

    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!GameManager.instance.activeInput)
        {
            PlayIdleAnimation();
            return;
        }

        AdjustPositionToTerrain();
        HandleMovement();
    }

    private void PlayIdleAnimation()
    {
        if (direction == "Front")
        {
            playerAnimationController.PlayAnimation("Idle Front");
        }
        else if (direction == "Back")
        {
            playerAnimationController.PlayAnimation("Idle Back");
        }
    }

    private void AdjustPositionToTerrain()
    {
        Vector3 castPos = transform.position;
        if (Physics.Raycast(castPos, transform.up, out RaycastHit hit, Mathf.Infinity, terrainLayer))
        {
            if (hit.collider != null)
            {
                Vector3 movePos = transform.position;
                movePos.y = hit.point.y + groundDistance;
                transform.position = movePos;
            }
        }
    }

    private void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 moveDir = new Vector3(x, 0, y);
        rigidBody.linearVelocity = moveDir * speed;

        UpdateSpriteDirection(x);
        UpdateAnimation(x, y);
    }

    private void UpdateSpriteDirection(float x)
    {
        if (x != 0)
        {
            spriteRenderer.flipX = x > 0;
        }
    }

    private void UpdateAnimation(float x, float y)
    {
        if (x == 0 && y == 0)
        {
            PlayIdleAnimation();
        }
        else if (y > 0)
        {
            direction = "Back";
            playerAnimationController.PlayAnimation("Walking Back");
        }
        else if (y <= 0)
        {
            direction = "Front";
            playerAnimationController.PlayAnimation("Walking Front");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        InteractionComponent interactionComponent = other.GetComponent<InteractionComponent>();
        if (interactionComponent != null)
        {
            interactionComponent.SetIsPlayerClose(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        InteractionComponent component = other.GetComponent<InteractionComponent>();
        if (component != null)
        {
            component.SetIsPlayerClose(false);
        }
    }
}
