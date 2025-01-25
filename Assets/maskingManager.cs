using UnityEngine;
using System.Collections.Generic;

public class managerMaskAnim : MonoBehaviour
{
    private List<animationBubbleMask> masks = new List<animationBubbleMask>();
    [SerializeField] private string bubbleMaskName = "bubbleMaskPictureFrame";

    void Start()
    {
        GameObject[] maskObjects = GameObject.FindGameObjectsWithTag("mask");
        foreach (GameObject obj in maskObjects)
        {
            animationBubbleMask mask = obj.GetComponent<animationBubbleMask>();
            if (mask != null)
            {
                masks.Add(mask);
            }
        }
    }

    public bool IsMaskOpen(string maskName)
    {
        var mask = masks.Find(m => m.GetComponentName() == maskName);
        return mask != null ? mask.isOpen : false;
    }

    public List<string> GetAllMaskNames()
    {
        List<string> names = new List<string>();
        foreach (var mask in masks)
        {
            names.Add(mask.GetComponentName());
        }
        return names;
    }
 
}