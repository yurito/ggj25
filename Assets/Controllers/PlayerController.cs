using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float groundDistance;
    public LayerMask terrainLayer;
    public Rigidbody rb;
    public SpriteRenderer spriteRenderer;

    public PlayerAnimationController playerAnimationController;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        RaycastHit hit;
        Vector3 castPos = transform.position;
        if (Physics.Raycast(castPos, transform.up, out hit, Mathf.Infinity, terrainLayer))
        {
            if (hit.collider != null)
            {
                Vector3 movePos = transform.position;
                movePos.y = hit.point.y + groundDistance;
                transform.position = movePos;
            }
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 moveDir = new Vector3(x, 0, y);
        rb.linearVelocity = moveDir * speed;

        if (x != 0 && x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (x != 0 && x > 0)
        {
            spriteRenderer.flipX = false;
        }


        if (x == 0 && y == 0)
        {
            playerAnimationController.PlayAnimation("Idle");
        }
        else if (y > 0)
        {
            playerAnimationController.PlayAnimation("Walking Back");
        }
        else if (y <= 0)
        {
            playerAnimationController.PlayAnimation("Walking");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        InteractionComponent component = other.GetComponent<InteractionComponent>();
        if (component == null)
        {
            return;
        }

        Debug.Log("Interacting with " + component.name);
        component.setIsPlayerClose(true);

        if (component.canInteract && !component.wasCollected)
        {
            interact(component);
        }

    }

    void OnTriggerExit(Collider other)
    {
        InteractionComponent component = other.GetComponent<InteractionComponent>();
        if (component == null)
        {
            return;
        }

        component.setIsPlayerClose(false);
    }

    void interact(InteractionComponent component)
    {
        if (component == null) return;
        component.wasCollected = true;
        component.canInteract = false;
        Debug.Log("Can Interact " + component.name);
    }
}
