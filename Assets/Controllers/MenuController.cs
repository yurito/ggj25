using UnityEngine;

public class MenuController : MonoBehaviour
{
    void Start()
    {
        GameManager.instance.activeInput = false;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            gameObject.SetActive(false);
            GameManager.instance.activeInput = true;
        }
    }
}
