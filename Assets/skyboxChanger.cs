using UnityEngine;

public class skyboxChanger : MonoBehaviour
{
    [SerializeField] private float transitionSpeed = 0.5f;
    [SerializeField] private Color targetColor = Color.white;
    private Material skyboxMaterial;
    private float currentLerpTime;
    private Color initialColor = Color.black;
    private bool isAnimating = true;

    void Start()
    {
        if (RenderSettings.skybox != null)
        {
            skyboxMaterial = new Material(RenderSettings.skybox);
            RenderSettings.skybox = skyboxMaterial;
            skyboxMaterial.SetColor("_Tint", initialColor);
            DynamicGI.UpdateEnvironment();
        }
        else
        {
            Debug.LogError("No skybox assigned in Render Settings");
        }
    }

    void Update()
    {
        if (isAnimating)
        {
            AnimateSkybox();

            isAnimating = false;
        }
    }
}

private void AnimateSkybox()
{
    if (skyboxMaterial.GetColor("_Tint") == targetColor)
    {
        currentLerpTime += Time.deltaTime * transitionSpeed;
        float t = Mathf.PingPong(currentLerpTime, 1f);
        Color newColor = Color.Lerp(initialColor, targetColor, t);
        skyboxMaterial.SetColor("_Tint", newColor);
        DynamicGI.UpdateEnvironment();
    }

}
}
