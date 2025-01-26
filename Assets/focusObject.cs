using UnityEngine;
using TMPro;

public class focusObject : MonoBehaviour
{
    public string id;
    [SerializeField] private GameObject focusObjectPrefab;
    [SerializeField] private GameObject focusObjectCaption;
    [SerializeField] private string newCaptionText;
    [SerializeField] private Transform focusPoint;
    [SerializeField] public bool toggleCameraState = false;
    [SerializeField] private float rotationSpeed = 18.1f;

    private TextMeshPro captionText;
    private GameObject spawnedObject;
    private Camera parentCamera;
    private bool lastCameraState;
    private static Camera activeCamera;
    private GameObject smartphoneObject;
    private GameObject dimmingObj;


    void Start()
    {
        InitializeCaption();
        SpawnFocusObject();
        CacheCameraReference();
        CacheSmartphoneReference();
        lastCameraState = toggleCameraState;
        UpdateCameraState();
        dimmingObj = transform.parent?.Find("dimming")?.gameObject;

        if (dimmingObj != null)
            dimmingObj.GetComponent<SpriteRenderer>().enabled = false;
    }

    void Update()
    {
        if (toggleCameraState != lastCameraState)
        {
            UpdateCameraState();
            lastCameraState = toggleCameraState;
        }

        if (spawnedObject != null)
        {
            spawnedObject.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.Self);
        }

        if (Input.GetKeyUp(KeyCode.E) && toggleCameraState)
        {
            GameManager.instance.CloseObject(id);
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
            foreach (Transform child in this.transform)
            {
                child.gameObject.SetActive(toggleCameraState);
            }
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

                if (dimmingObj != null)
                    dimmingObj.GetComponent<SpriteRenderer>().enabled = true;

                parentCamera.enabled = true;
            }
            else
            {
                var mainCam = GameObject.FindGameObjectWithTag("MainCamera")?.GetComponent<Camera>();
                if (mainCam != null)
                    mainCam.enabled = true;

                if (smartphoneObject != null)
                    smartphoneObject.SetActive(true);

                if (dimmingObj != null)
                    dimmingObj.GetComponent<SpriteRenderer>().enabled = false;



                parentCamera.enabled = false;
            }
        }
    }
}
