using UnityEngine;
using TMPro;

public class focusObject : MonoBehaviour
{
    [SerializeField] private GameObject focusObjectPrefab;
    [SerializeField] private GameObject focusObjectCaption;
    [SerializeField] private string newCaptionText;
    [SerializeField] private Transform focusPoint;
    [SerializeField] private bool toggleCameraState = false;

    private TextMeshPro captionText;
    private GameObject spawnedObject;
    private Camera parentCamera;
    private bool lastCameraState;
    private static Camera activeCamera;
    private GameObject smartphoneObject;

    void Start()
    {
        InitializeCaption();
        SpawnFocusObject();
        CacheCameraReference();
        CacheSmartphoneReference();
        lastCameraState = toggleCameraState;
        UpdateCameraState();
    }

    void Update()
    {
        UpdateCaption();
        if (toggleCameraState != lastCameraState)
        {
            UpdateCameraState();
            lastCameraState = toggleCameraState;
        }
    }

    private void CacheCameraReference()
    {
        if (parentCamera == null && transform.parent != null)
            parentCamera = transform.parent.GetComponentInChildren<Camera>();
    }

    private void InitializeCaption()
    {
        if (focusObjectCaption != null)
            captionText = focusObjectCaption.GetComponent<TextMeshPro>();
    }

    private void SpawnFocusObject()
    {
        if (focusObjectPrefab != null && focusPoint != null)
        {
            spawnedObject = Instantiate(
                focusObjectPrefab,
                focusPoint.position,
                focusPoint.rotation,
                focusPoint
            );
            spawnedObject.transform.localScale = focusPoint.localScale;
        }
    }

    public void UpdateCaption()
    {
        if (captionText != null)
            captionText.text = newCaptionText;
    }

    private void CacheSmartphoneReference()
    {
        smartphoneObject = GameObject.Find("smartphone");
    }

    private void UpdateCameraState()
    {
        if (parentCamera != null)
        {
            if (toggleCameraState)
            {
                foreach (var cam in Camera.allCameras)
                {
                    if (cam != parentCamera)
                        cam.enabled = false;
                }
                if (this.gameObject.tag == "focusCam3")
                {
                    if (smartphoneObject != null)
                        smartphoneObject.SetActive(true);
                }
                else
                {
                    if (smartphoneObject != null)
                        smartphoneObject.SetActive(false);
                }



                parentCamera.enabled = true;
            }
            else
            {
                var mainCam = GameObject.FindGameObjectWithTag("MainCamera")?.GetComponent<Camera>();
                if (mainCam != null)
                    mainCam.enabled = true;

                if (smartphoneObject != null)
                    smartphoneObject.SetActive(true);

                parentCamera.enabled = false;
            }
        }
    }
}
