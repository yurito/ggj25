using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GrayscaleEffect : MonoBehaviour
{
    private Material material;
    private SpriteRenderer spriteRenderer;
    
    [Range(0, 1)]
    public float effectAmount = 1f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        material = new Material(Shader.Find("CustomRenderTexture/grayScallingShader"));
        spriteRenderer.material = material;
    }

    void Update()
    {
        material.SetFloat("_EffectAmount", effectAmount);
    }
}