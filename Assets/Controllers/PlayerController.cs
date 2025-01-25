using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float groundDistance;
    public LayerMask terrainLayer;
    public Rigidbody rb;
    public SpriteRenderer spriteRenderer;

    public PlayerAnimationController playerAnimationController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
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


        Debug.Log("x: " + x + " y: " + y);

        if (x == 0 && y == 0) {
            playerAnimationController.PlayAnimation("Idle");
        } else if (y > 0) {
            playerAnimationController.PlayAnimation("Walking Back");
        } else if (y <= 0) {
            playerAnimationController.PlayAnimation("Walking");
        }

        // if (x == 0 && y == 0)
        // {
        //     Debug.Log("");
        //     playerAnimationController.PlayAnimation("Idle");
        // }
        // else if (y < 0)
        // {
        //     playerAnimationController.PlayAnimation("Walking");
        // }
        // else if (y > 0)
        // {
        //     playerAnimationController.PlayAnimation("Walking Back");
        // }

    }
}
