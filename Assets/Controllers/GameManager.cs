using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public InteractionComponent[] allInteractionComponents;

    void Awake()
    {
        instance = this;
    }

    void Start() 
    {
        allInteractionComponents = FindObjectsOfType<InteractionComponent>();
    }
}
