using UnityEngine;
using UnityEngine.UI;

public class ButtonNext : MonoBehaviour
{
    public Button button;

    void Awake()
    {
        button.onClick.AddListener(OnCLick);
    }

    void OnCLick() 
    {
        Debug.Log("Next");
    }
}
