using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LookVertical : MonoBehaviour
{
    private CameraManager CM;
    private GameManager GM;

    private TutorialManager TM;

    [Header("Movement")]
    [Tooltip("How easily the camera can look around"), Min(0), SerializeField]
    private float sensitivity = 4.5f;
    private float mouseY;
    private float pitch;

    private Vector3 newRotation;

    [SerializeField] private GameObject dialogBox;

    // Start is called before the first frame update
    void Start()
    {
        CM = transform.parent.GetComponent<CameraManager>();
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();

        if (SceneManager.GetActiveScene().buildIndex == 0)
            TM = GameObject.Find("Tutorial").GetComponent<TutorialManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Gets the mouses position if mouse is hidden
        if (!Cursor.visible && CM.GetCameraPerspective() && !GM.GetLoadingState() && dialogBox.activeInHierarchy == false)
        {
            mouseY = Input.GetAxis("Mouse Y");

            if (TM != null && !TM.HasLooked() && Input.GetAxis("Mouse Y") != 0)
            {
                StartCoroutine(TM.IsLooking());
            }
            else if (TM == null && SceneManager.GetActiveScene().buildIndex == 0)
                Debug.LogError("Tutorial for Camera Movement couldn't be found");
        }
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
