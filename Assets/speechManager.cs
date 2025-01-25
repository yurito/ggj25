using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class SpeechManager : MonoBehaviour
{
    [SerializeField] private Canvas canvasObject;
    [SerializeField] private string id;
    [SerializeField] private bool showText;

    private bool previousShowState;
    private Canvas targetCanvas;

    void Awake()
    {
        if (canvasObject == null)
        {
            canvasObject = GetComponent<Canvas>();
        }

        targetCanvas = canvasObject;
        previousShowState = showText;
        UpdateCanvasState();
    }

    void Update()
    {
        if (showText != previousShowState)
        {
            UpdateCanvasState();
            previousShowState = showText;
        }
    }

    private void UpdateCanvasState()
    {
        if (targetCanvas != null)
        {
            targetCanvas.enabled = showText;
        }
    }
}