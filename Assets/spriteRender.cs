using UnityEngine;

public class RenderSprite : MonoBehaviour
{
    [SerializeField] private Sprite spriteToRender;
    [SerializeField] private Color spriteColor = Color.white;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }

        if (spriteToRender != null)
        {
            spriteRenderer.sprite = spriteToRender;
            spriteRenderer.color = spriteColor;
        }
        else
        {
            Debug.LogWarning("No sprite assigned to render!");
        }
    }
}