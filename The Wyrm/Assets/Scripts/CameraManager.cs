using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private bool firstPerson = false;
    private Vector3 returnPosition;

    // Start is called before the first frame update
    void Start()
    {
        returnPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SetCameraPerspective(!GetCameraPerspective());
        }

        if (GetCameraPerspective() && transform.position != transform.parent.position)
        {
            returnPosition = transform.position;
            transform.position = transform.parent.position;
        }
        else if (!GetCameraPerspective() && transform.position == transform.parent.position)
        {
            transform.position = returnPosition;
        }

    }

    //----------------------------------------------------------------------------------------------------------------------
    // Get and Set

    public bool GetCameraPerspective()
    {
        return firstPerson;
    }

    public void SetCameraPerspective(bool newPerspective)
    {
        firstPerson = newPerspective;
    }

}
