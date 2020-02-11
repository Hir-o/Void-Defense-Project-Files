using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    private Camera camera;
    private float  zoom;

    [BoxGroup("Zoom Values")] [SerializeField]
    private float minZoomValue = 5f, maxZoomValue = 15f;

    [BoxGroup("Scroll Speed")] [SerializeField]
    private float scrollSpeed = 15f;

    void Start()
    {
        camera = ObjectReferenceHolder.Instance.mainCamera;
        zoom   = camera.orthographicSize;
    }

    void Update() { HandleZoom(); }

    private void HandleZoom()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            zoom -= Time.deltaTime * scrollSpeed;
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            zoom += Time.deltaTime * scrollSpeed;
        }

        zoom = Mathf.Clamp(zoom, minZoomValue, maxZoomValue);

        camera.orthographicSize = zoom;
    }
}