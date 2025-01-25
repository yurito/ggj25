using UnityEngine;
using UnityEngine.UI;

public class closeScript : MonoBehaviour
{
    [SerializeField] private GameObject canvasObject;

    void Awake()
    {
        if (canvasObject == null)
        {
            Debug.LogError("Canvas GameObject not assigned on " + gameObject.name);
            enabled = false;
        }

        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogError("Button component not found on " + gameObject.name);
        }
    }

    private void OnButtonClick()
    {
      //TODO: Implement close functionality
    }
}
