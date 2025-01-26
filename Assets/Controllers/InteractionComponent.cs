using System;
using UnityEngine;

public class InteractionComponent : MonoBehaviour
{
    public string id;
    public bool canInteract = false;
    public bool wasCollected = false;
    private bool isPlayerClose = false;
    private Outline outline;

    public InteractionComponent[] dependences;
    public GameObject UI;

    void Start()
    {
        ShowUI();
        ShowOutline();
    }

    void Update()
    {
        CalculateConditions();
        ShowUI();
        GetInput();

        outline.enabled = canInteract && !wasCollected;
    }

    private void ShowOutline()
    {
        outline = gameObject.AddComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = Color.yellow;
        outline.OutlineWidth = 2f;

        outline.enabled = false;
    }

    void CalculateConditions()
    {
        canInteract = false;
        foreach (InteractionComponent dependence in dependences)
        {
            if (!dependence.wasCollected)
            {
                return;
            }
        }

        canInteract = true;
    }

    public void SetIsPlayerClose(bool value)
    {
        isPlayerClose = value;
        ShowUI();
    }

    public void ShowUI()
    {
        bool show = canInteract && isPlayerClose && !wasCollected;
        UI.SetActive(show);
    }

    public void GetInput()
    {
        if (Input.GetKeyUp(KeyCode.E) && canInteract && isPlayerClose && !wasCollected)
        {
            wasCollected = true;
            GameManager.instance.OpenSpeech(id);
            ShowUI();
        }
    }
}
