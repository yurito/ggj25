using System;
using UnityEngine;

public class InteractionComponent : MonoBehaviour
{
    public bool canInteract = false;
    public bool wasCollected = false;
    private bool isPlayerClose = false;

    public InteractionComponent[] dependences;
    public GameObject UI;

    void Start()
    {
        showUI();

    }

    void Update()
    {
        calculateConditions();
        showUI();
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
}
