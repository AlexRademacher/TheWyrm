using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookHorizontal : MonoBehaviour
{
    private CameraManager CM;

    [Header("Movement")]
    [Tooltip("How easily the camera can look around"), Min(0), SerializeField]
    private float sensitivity = 4.5f;
    private float mouseX;

    // Start is called before the first frame update
    void Start()
    {
        CM = transform.GetChild(0).GetComponent<CameraManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Gets the mouses position if cursor is hidden
        if (!Cursor.visible && CM.GetCameraPerspective())
            mouseX = Input.GetAxis("Mouse X");
        else if (CM == null)
        {
            mouseX = 0;
            Debug.LogWarning("CM for the cameraH is null");
        }
        else
            mouseX = 0;

        // sets the new camera postion
        Vector3 newRotation = transform.localEulerAngles;
        newRotation.y += (mouseX * sensitivity);
        transform.localEulerAngles = newRotation;
    }
}
