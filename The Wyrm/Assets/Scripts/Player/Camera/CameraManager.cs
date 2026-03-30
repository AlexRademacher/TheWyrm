using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraManager : MonoBehaviour
{
    private GameManager GM;
    private PlayerAudioManager PAM;
    private CameraFixer CF;

    private bool firstPerson;

    private Camera Camera1stPerson;
    [SerializeField] private Camera MapCam;

    [Header("Debugger")]
    [Tooltip("Turns on Camera Debugging"), SerializeField]
    private bool cameraDebugging;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();
        PAM = transform.GetComponentInParent<PlayerAudioManager>();
        CF = GameObject.Find("Player").GetComponent<CameraFixer>();

        Camera1stPerson = transform.GetChild(0).GetComponent<Camera>();

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            firstPerson = true;
        }
        else
        {
            firstPerson = true;
        }

        if (MapCam)
            MapCam.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab) && (SceneManager.GetActiveScene().buildIndex != 5) && DialogueManager.Instance.inDialouge == false && !Cursor.visible && !GM.GetTalking() && !GM.GetLoadingState())
        {
            if (firstPerson)
            {
                PAM.playMapOpen();
                CF.StorePos();
            }
            else
            {
                PAM.playMapClose();
                CF.LoadPos();
            }
            RenderSettings.fog = !RenderSettings.fog;
            Camera1stPerson.enabled = !Camera1stPerson.enabled;
            MapCam.enabled = !MapCam.enabled;
            firstPerson = !firstPerson;
        }
    }

    //----------------------------------------------------------------------------------------------------------------------
    // Get and Set

    /// <summary>
    /// Get the perspective of the camera
    /// </summary>
    /// <returns> true = first person, false = third person </returns>
    public bool GetCameraPerspective()
    {
        //Debug.Log(firstPerson);
        return firstPerson;
    }

    /// <summary>
    /// Set the perspective of the camera
    /// </summary>
    /// <param name="newPerspective"> true = first person, false = third person </param>
    public void SetCameraPerspective(bool newPerspective)
    {
        firstPerson = newPerspective;
    }

}
