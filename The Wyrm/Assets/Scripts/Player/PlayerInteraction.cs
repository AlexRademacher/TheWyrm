using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    private CameraManager CM;
    private UIManager UI;
    private PlayerInventory PInv;

    public Ray rayCast;
    public RaycastHit hitInfo;
    
    private int lastNpcId;

    [SerializeField] private GameObject dialogBox;


    // Start is called before the first frame update
    void Start()
    {
        CM = transform.GetChild(0).GetComponent<CameraManager>();
        UI = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (GameObject.Find("Scene Manager") != null)
            PInv = GameObject.Find("Scene Manager").GetComponent<PlayerInventory>();
        else
            Debug.LogWarning("Scene Manager not within the scene (can be ignored if testing)");
    }

    // Update is called once per frame
    void Update()
    {
        if (CM.GetCameraPerspective())
        {
            InteractFirstPerson();
        }

        if (lastNpcId == 8 && Cursor.visible == false && SceneManager.GetActiveScene().buildIndex == 0)
        {
            lastNpcId = 0;
            UI.ShowTeacherCutscene();

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
            Crosshair();

            // if E is clicked
            if (Input.GetKeyDown(KeyCode.E) && Cursor.visible == false)
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
                        TalkingToNPC();
                    }

                    if (hitInfo.transform.gameObject.CompareTag("Door"))
                    {
                        DoorInteraction(hitInfo.transform.gameObject, true);
                        DoorInteraction(hitInfo.transform.gameObject, false);
                    }

                    if (hitInfo.transform.gameObject.CompareTag("Openable"))
                    {
                        OpenObject();
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


    private void Crosshair()
    {
        if (hitInfo.transform.gameObject.CompareTag("Item") || hitInfo.transform.gameObject.CompareTag("NPC") || hitInfo.transform.gameObject.CompareTag("Openable"))
        {
            UI.CrosshairEToggle(true);
        }
        else if (hitInfo.transform.gameObject.CompareTag("Table"))
        {
            UI.CrosshairRToggle(true);
        }
        else if (hitInfo.transform.gameObject.CompareTag("drop"))
        {
            UI.CrosshairFToggle(true);
        }
        else
        {
            UI.CrosshairToggle(true);
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
        if (hitInfo.collider.name.Contains("DropOff"))
        {
            GameObject item = PInv.RemoveInventory();

            if (item != null)
            {
                if (CM != null)
                {
                    if (CM.GetCameraPerspective())
                    {
                        Debug.LogWarning("Placed relic");
                        GameObject newRelic = Instantiate(item, hitInfo.point, transform.rotation);
                        newRelic.SetActive(true);
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

    private void OpenObject()
    {
        if (hitInfo.transform.parent != null)
        {
            if (hitInfo.transform.parent.TryGetComponent<OpenableManager>(out OpenableManager OMScript))
            {
                OMScript.OpenControl();
            }
            else
                Debug.LogError("Openable not correctly set up missing OpenableManager in parent object");
        }
        else
            Debug.LogError("Openable not correctly set up missing parent object");
    }


    private void TalkingToNPC()
    {
        if (!hitInfo.transform.GetComponent<NPCManager>().canNeverSpeak)
        {
            Dialog dialog = hitInfo.transform.GetComponent<Dialog>();

            switch (hitInfo.transform.gameObject.GetComponent<NPCManager>().npcID)
            {
                case 0:
                    UI.SetTextNameBox("Teacher");
                    lastNpcId = 0;
                    break;
                case 1:
                    UI.SetTextNameBox("Merchant");
                    lastNpcId = 1;
                    break;
                case 2:
                    UI.SetTextNameBox("Craftsman");
                    lastNpcId = 2;
                    break;
                case 3:
                    UI.SetTextNameBox("Wood Sculptor");
                    lastNpcId = 3;
                    break;
                case 4:
                    UI.SetTextNameBox("Potter");
                    lastNpcId = 4;
                    break;
                case 5:
                    UI.SetTextNameBox("Fisherman");
                    lastNpcId = 5;
                    break;
                case 6:
                    UI.SetTextNameBox("Landlord");
                    lastNpcId = 6;
                    break;
                case 7:
                    UI.SetTextNameBox("Farmer");
                    lastNpcId = 7;
                    break;
                case 8:
                    if (SceneManager.GetActiveScene().buildIndex == 0)
                        UI.SetTextNameBox("Mysterious Child");
                    else
                        UI.SetTextNameBox("Brother");
                    lastNpcId = 8;
                    break;
                case 9:
                    UI.SetTextNameBox("Widow");
                    lastNpcId = 9;
                    break;
                case 10:
                    UI.SetTextNameBox("Shaman");
                    lastNpcId = 10;
                    break;
                case 11:
                    UI.SetTextNameBox("Landlord's Son");
                    lastNpcId = 11;
                    break;
                case 12:
                    UI.SetTextNameBox("Farmer's Son");
                    lastNpcId = 12;
                    break;
                case 13:
                    UI.SetTextNameBox("Sculptor's Son");
                    lastNpcId = 13;
                    break;
                default:
                    Debug.LogWarning("Unknown ID");
                    break;
            }

            if (hitInfo.transform.gameObject.GetComponent<NPCManager>().npcID == 8)
                lastNpcId = 8;
            if (dialog != null && dialogBox.activeInHierarchy == false)
            {
                //Debug.Log("found Dialog");
                StartCoroutine(DialogueManager.Instance.ShowDialog(dialog, hitInfo.transform.gameObject));
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }



        }
        else
            hitInfo.transform.GetComponent<NPCManager>().SetInTalkingState(false);
        //Debug.LogWarning("the NPC trying to be Talked to is " + hitInfo.transform.gameObject.name);
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
