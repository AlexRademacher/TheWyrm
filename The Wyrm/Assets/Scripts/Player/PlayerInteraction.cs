using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private CameraManager CM;
    private UIManager UI;

    public Ray rayCast;
    public RaycastHit hitInfo;

    // Start is called before the first frame update
    void Start()
    {
        CM = transform.GetChild(0).GetComponent<CameraManager>();
        UI = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CM.GetCameraPerspective())
        {
            InteractFirstPerson();
        }
    }

    //----------------------------------------------------------------------------------------------------------------------
    // Interactions

    /// Controls the RayCast of the player allowing for interaction
    /// </summary>
    private void InteractFirstPerson()
    {
        Camera firstPersonCamera = transform.GetChild(0).GetChild(0).GetComponent<Camera>();

        // creates and updates the raycast
        rayCast = firstPersonCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)); //50% horiz, 50% vert

        // if hit something get its info and do the rest of the code
        if (Physics.Raycast(rayCast, out hitInfo, 5.0f))
        {
            // if mouse is clicked and the object hit has the tag item
            if (Input.GetKeyDown(KeyCode.E) && hitInfo.transform.gameObject.CompareTag("Item"))
            {
                PickUpItemState1st(hitInfo.transform.gameObject); // specific interactions with item
            }
        }
    }

    private void PickUpItemState1st(GameObject item)
    {
        Item itemScript = item.transform.GetComponent<Item>();
        if (itemScript != null)
        {
            itemScript.FirstPersonInteraction();
        }
        else
            Debug.LogWarning("itemScript missing from item");
    }

    private void PickUpItemState3rd(GameObject item, bool pickUpState)
    {
        Item itemScript = item.transform.GetComponent<Item>();
        if (itemScript != null)
        {
            itemScript.SetPickUpState(true);
        }
        else
            Debug.LogWarning("itemScript missing from item");

    }

    private void DoorInteraction(GameObject door, bool openState)
    {
        DoorManager doorScript = door.transform.GetComponent<DoorManager>();

        if (doorScript != null)
        {
            if (openState)
            {
                if (doorScript.GetLockedState())
                {
                    doorScript.UnlockDoor(door);
                }
                else
                {
                    doorScript.SetCanOpenState(true);
                }
            }
            else
            {
                doorScript.SetCanOpenState(false);
            }
        }
        else
            Debug.LogWarning("doorScript missing from door");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!CM.GetCameraPerspective())
        {
            if (other.gameObject.CompareTag("Item"))
            {
                Debug.Log(other.name + " is seen");
                PickUpItemState3rd(other.gameObject, true);
            }
        }

        if (other.gameObject.CompareTag("Door"))
        {
            DoorInteraction(other.gameObject, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!CM.GetCameraPerspective())
        {
            if (other.gameObject.CompareTag("Item"))
            {
                PickUpItemState3rd(other.gameObject, false);
            }
        }

        if (other.gameObject.CompareTag("Door"))
        {
            DoorInteraction(other.gameObject, false);
        }
    }
}
