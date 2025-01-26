using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public InteractionComponent[] allInteractionComponents;
    public Dictionary<string, animationBubbleMask> bubblesDictionary = new Dictionary<string, animationBubbleMask>();
    public Dictionary<string, SpeechManager> speechDictionary = new Dictionary<string, SpeechManager>();

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

        SpeechManager[] speechManagers = FindObjectsOfType<SpeechManager>();
        foreach (var item in speechManagers)
        {
            speechDictionary.Add(item.id, item);
        }

    }
}
