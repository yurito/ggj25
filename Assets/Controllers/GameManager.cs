using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public InteractionComponent[] allInteractionComponents;
    public Dictionary<string, animationBubbleMask> bubblesDictionary = new Dictionary<string, animationBubbleMask>();
    public Dictionary<string, SpeechManager> speechDictionary = new Dictionary<string, SpeechManager>();
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

        SpeechManager[] speechManagers = FindObjectsOfType<SpeechManager>();
        foreach (var item in speechManagers)
        {
            speechDictionary.Add(item.id, item);
        }
    }

    public void OpenSpeech(string id)
    {
        SpeechManager speech = speechDictionary[id];
        if (speech == null)
        {
            return;
        }

        speech.showText = true;
        activeInput = false;
    }

    public void CloseSpeech(string id)
    {
        SpeechManager speech = speechDictionary[id];
        animationBubbleMask bubble = bubblesDictionary[id];

        if (speech == null || bubble == null)
        {
            return;
        }

        activeInput = true;
        bubble.isOpen = true;
        speech.showText = false;
        IsAllBubblesOpened();
    }

    private void IsAllBubblesOpened()
    {
        if (bubblesDictionary.Values.Count(x => x.isOpen) == (bubblesDictionary.Count - 1))
        {
            bubblesDictionary["final"].isOpen = true;
        }
    }
}
