using System.Collections;
using UnityEngine;

public class animationBubbleMask : MonoBehaviour
{
    public string id;
    [SerializeField] private Vector3 targetScale = new Vector3(9f, 9f, 9f);
    [SerializeField] private float scaleRate = 10f;
    private bool hasReachedTarget = false;
    private Vector3 zeroScale = Vector3.zero;
    private Transform cachedTransform;

    void Awake()
    {
        cachedTransform = transform;
    }

    void Start()
    {
        cachedTransform.localScale = zeroScale;
    }

    public bool isOpen = false;

    private void Animate()
    {
        if (!hasReachedTarget)
        {
            if (Vector3.Distance(cachedTransform.localScale, targetScale) < 0.01f)
            {
                cachedTransform.localScale = targetScale;
                hasReachedTarget = true;
                return;
            }
            cachedTransform.localScale = Vector3.MoveTowards(cachedTransform.localScale, targetScale, scaleRate * Time.deltaTime);
        }
    }

    void Update()
    {
        if (id == "letter" && isOpen)
        {
            StartCoroutine("Show");
        }

        if (isOpen)
        {
            Animate();
            return;
        }
        cachedTransform.localScale = zeroScale;
        hasReachedTarget = false;
    }

    public string GetComponentName()
    {
        return gameObject.name;
    }

    private IEnumerator Show()
    {
        Debug.Log("Showing final screen");
        yield return new WaitForSeconds(5f);
        Debug.Log("Final screen shown");
        GameObject.FindGameObjectWithTag("FinalScreen").GetComponent<Canvas>().enabled = true;
        GameManager.instance.activeInput = false;
    }
}
