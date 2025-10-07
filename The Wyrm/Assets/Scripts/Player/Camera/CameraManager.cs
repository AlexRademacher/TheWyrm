using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraManager : MonoBehaviour
{
    private bool firstPerson;

    private Camera Camera3rdPerson;
    private Camera Camera1stPerson;
    [SerializeField] private Camera MapCam;

    [Header("Debugger")]
    [Tooltip("Turns on Camera Debugging"), SerializeField]
    private bool cameraDebugging;

    // Start is called before the first frame update
    void Start()
    {
        Camera1stPerson = transform.GetChild(0).GetComponent<Camera>();
        Camera3rdPerson = transform.GetChild(1).GetComponent<Camera>();

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            firstPerson = true;
        }
        else
        {
            firstPerson = true;
        }
        MapCam.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraDebugging && Input.GetKeyDown(KeyCode.P))
        {
            SetCameraPerspective(!GetCameraPerspective());
        }

        if (GetCameraPerspective() && Camera3rdPerson.enabled)
        {
            Camera3rdPerson.enabled = false;
            Camera1stPerson.enabled = true;
        }
        else if (!GetCameraPerspective() && Camera1stPerson.enabled)
        {
            Camera3rdPerson.enabled = true;
            Camera1stPerson.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
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
        Debug.Log(firstPerson);
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
