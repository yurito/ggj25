using UnityEngine;

public class animationBubbleMask : MonoBehaviour
{
    void Start()
    {
        transform.localScale = Vector3.zero;
    }

    [SerializeField] private Vector3 targetScale = new Vector3(50f, 50f, 50f);
    [SerializeField] private float scaleRate = 1f;
    private bool hasReachedTarget = false;


    void Update()
    {
        if (!hasReachedTarget)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, targetScale, scaleRate * Time.deltaTime);
            
            if (transform.localScale == targetScale)
            {
                hasReachedTarget = true;
            }
        }
    }
}
