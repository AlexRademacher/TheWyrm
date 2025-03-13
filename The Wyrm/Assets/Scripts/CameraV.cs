using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LookVertical : MonoBehaviour
{
    private CameraManager CM;

    [Header("Movement")]
    [Tooltip("How easily the camera can look around"), Min(0), SerializeField]
    private float sensitivity = 4.5f;
    private float mouseY;
    private float pitch;

    private Vector3 newRotation;

    // Start is called before the first frame update
    void Start()
    {
        CM = GetComponent<CameraManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Gets the mouses position if mouse is hidden
        if (!Cursor.visible && CM.GetCameraPerspective())
            mouseY = Input.GetAxis("Mouse Y");
        else if (CM == null)
        {
            mouseY = 0;
            Debug.LogWarning("CM for the cameraV is null");
        }
        else
            mouseY = 0;

        // sets the new camera postion
        newRotation = transform.localEulerAngles;

        // stops camera from rotating past 180 and flipping upside down
        pitch -= mouseY * sensitivity;
        pitch = Mathf.Clamp(pitch, -90, 90);

        newRotation.x = pitch;

        transform.localEulerAngles = newRotation;
    }
}
