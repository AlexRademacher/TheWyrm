using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private CameraManager CM;
    private UIManager UI;
    private PlayerInventory PInv;

    public Ray rayCast;
    public RaycastHit hitInfo;

    // Start is called before the first frame update
    void Start()
    {
        CM = transform.GetChild(0).GetComponent<CameraManager>();
        UI = GameObject.Find("Canvas").GetComponent<UIManager>();
        PInv = transform.GetComponent<PlayerInventory>();
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
        if (Physics.Raycast(rayCast, out hitInfo, 8.0f))
        {
            // if E is clicked
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (hitInfo.transform.gameObject != null)
                {
                    if (hitInfo.transform.gameObject.CompareTag("Item")) // Picking things up
                    {
                        //Debug.LogWarning("the Object trying to be picked up is " + hitInfo.transform.gameObject.name);
                        PickUpItem(hitInfo.transform.gameObject); // specific interactions with item
                    }

                    if (hitInfo.transform.gameObject.CompareTag("NPC"))
                    {
                        hitInfo.transform.GetComponent<NPCManager>().SetInTalkingState(true);
                        Debug.LogWarning("the NPC trying to be Talked to is " + hitInfo.transform.gameObject.name);
                    }

                    if (hitInfo.transform.gameObject.CompareTag("Door"))
                    {
                        DoorInteraction(hitInfo.transform.gameObject, true);
                        DoorInteraction(hitInfo.transform.gameObject, false);
                    }
                }
                else
                    Debug.LogWarning("Object being interacted with is null");
            }

            // if R is clicked
            if (Input.GetKeyDown(KeyCode.R)) {
                DropItem();
            }
        }
    }

    //----------------------------------------------------------------------------------------------------------------------
    // Pick Up/Drop Interactions

    public void PickUpItem(GameObject item)
    {
        if (item != null)
        {
            if (item.TryGetComponent<Item>(out Item itemScript))
            {
                if (CM != null)
                {
                    if (CM.GetCameraPerspective())
                    {
                        GameObject clone = item;

                        if (PInv != null)
                            PInv.AddToInventory(clone);
                        else
                            Debug.LogWarning("Inventory script not set up correctly for picking up item");

                        itemScript.PickedUp();
                    }
                    else
                    {
                        Debug.LogWarning("third person pick up not finished yet");
                    }
                }
                else
                    Debug.LogWarning("Camera manger not set up correctly for picking up item");
            }
            else
                Debug.LogWarning("item script not avalible");
        }
        else
            Debug.LogWarning("item being picked up is null");
        
    }

    public void DropItem()
    {
        GameObject item = PInv.RemoveInventory();

        if (item != null)
        {
            if (CM != null)
            {
                if (CM.GetCameraPerspective())
                {
                    Instantiate(item, hitInfo.point, transform.rotation);
                    Destroy(item);
                }
                else
                {
                    Instantiate(item, new Vector3(transform.position.x + 1, transform.position.y - (transform.position.y / 2) - .25f, transform.position.z), transform.rotation);
                }
            }
            else
                Debug.LogWarning("Camera manger not set up correctly for dropping item");
        }
        else
            Debug.LogWarning("No items to drop");
    }

    //----------------------------------------------------------------------------------------------------------------------
    // Door Interactions

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
                //Debug.Log(other.name + " is seen");
                //PickUpItemState3rd(other.gameObject, true);
            }

            if (other.gameObject.CompareTag("Door"))
            {
                DoorInteraction(other.gameObject, true);
            }

            if (other.gameObject.CompareTag("NPC"))
            {
                other.transform.GetComponent<NPCManager>().SetInTalkingState(true);
            }
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (!CM.GetCameraPerspective())
        {
            if (other.gameObject.CompareTag("Item"))
            {
                //PickUpItemState3rd(other.gameObject, false);
            }

            if (other.gameObject.CompareTag("Door"))
            {
                DoorInteraction(other.gameObject, false);
            }

            if (other.gameObject.CompareTag("NPC"))
            {
                other.transform.GetComponent<NPCManager>().SetInTalkingState(false);
            }
        }
    }
}
