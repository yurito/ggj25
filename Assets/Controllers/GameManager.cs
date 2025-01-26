using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public InteractionComponent[] allInteractionComponents;
    public Dictionary<string, animationBubbleMask> bubblesDictionary = new Dictionary<string, animationBubbleMask>();
    public Dictionary<string, focusObject> objectDictionary = new Dictionary<string, focusObject>();
    public bool activeInput = true;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        allInteractionComponents = FindObjectsOfType<InteractionComponent>();

        animationBubbleMask[] animationBubbleMask = FindObjectsOfType<animationBubbleMask>();
        foreach (var item in animationBubbleMask)
        {
            bubblesDictionary.Add(item.id, item);
        }

        focusObject[] objectManagers = FindObjectsOfType<focusObject>();
        foreach (var item in objectManagers)
        {
            objectDictionary.Add(item.id, item);
        }
    }

    public void OpenObject(string id)
    {
        focusObject @object = objectDictionary[id];
        if (@object == null)
        {
            return;
        }

        @object.toggleCameraState = true;
        activeInput = false;
    }

    public void CloseObject(string id)
    {
        focusObject @object = objectDictionary[id];
        animationBubbleMask bubble = bubblesDictionary[id];

        if (@object == null || bubble == null)
        {
            return;
        }

        activeInput = true;
        bubble.isOpen = true;
        @object.toggleCameraState = false;
    }
}
