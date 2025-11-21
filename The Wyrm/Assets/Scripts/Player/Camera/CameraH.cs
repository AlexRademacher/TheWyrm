using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookHorizontal : MonoBehaviour
{
    private CameraManager CM;
    private GameManager GM;

    private TutorialManager TM;

    [Header("Movement")]
    [Tooltip("How easily the camera can look around"), Min(0), SerializeField]
    private float sensitivity = 4.5f;
    private float mouseX;

    [SerializeField] private GameObject dialogBox;

    // Start is called before the first frame update
    void Start()
    {
        CM = transform.GetChild(0).GetComponent<CameraManager>();
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();
        TM = GameObject.Find("Tutorial").GetComponent<TutorialManager>();

    }

    // Update is called once per frame
    void Update()
    {
        // Gets the mouses position if cursor is hidden
        if (!Cursor.visible && CM.GetCameraPerspective() && !GM.GetLoadingState() && dialogBox.activeInHierarchy == false)
        {
            mouseX = Input.GetAxis("Mouse X");

            if (TM != null && !TM.HasLooked() && Input.GetAxis("Mouse X") != 0)
            {
                StartCoroutine(TM.IsLooking());
            }
            else if (TM == null)
                Debug.LogError("Tutorial for Camera Movement couldn't be found");
        }
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
