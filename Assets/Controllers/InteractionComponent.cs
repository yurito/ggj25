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
        showUI();
        outline = gameObject.AddComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = Color.yellow;
        outline.OutlineWidth = 5f;

        outline.enabled = false;
    }

    void Update()
    {
        calculateConditions();
        showUI();
        getInput();

        if (canInteract && !wasCollected)
        {
            outline.enabled = true;
        }
    }

    void calculateConditions()
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

    public void setIsPlayerClose(bool value)
    {
        isPlayerClose = value;
        showUI();
    }

    public void showUI()
    {
        bool show = canInteract && isPlayerClose && !wasCollected;
        UI.SetActive(show);
    }

    public void getInput()
    {
        if (Input.GetKeyUp(KeyCode.E) && canInteract && isPlayerClose && !wasCollected)
        {
            wasCollected = true;
            GameManager.instance.OpenSpeech(id);
            showUI();
        }
    }
}
