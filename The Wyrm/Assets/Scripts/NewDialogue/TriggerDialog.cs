using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    //Only trigger once
    Dialog thisDialog;
    bool triggered;
    private UIManager UI;
    private PlayerInteraction PI;
    private LookVertical CV;
    private CameraFixer CF;

    private float storedY;
    private float storedZ;


    // Start is called before the first frame update
    void Start()
    {
        UI = GameObject.Find("Canvas").GetComponent<UIManager>();
        PI = GameObject.Find("Player").GetComponent<PlayerInteraction>();
        CV = GameObject.Find("First Person Camera").GetComponent<LookVertical>();
        CF = GameObject.Find("Player").GetComponent<CameraFixer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        CF.StorePos();
        if (!triggered)
        {
            triggered = true;
            thisDialog = GetComponent<Dialog>();
            other.gameObject.transform.LookAt(this.transform.position);

            //Add if statements to change amount of vertical rotation based on starting rotation (20)
            if (other.gameObject.transform.GetChild(0).GetChild(0).transform.rotation.x >= -20)
            {
                Debug.Log("20");
                other.gameObject.transform.eulerAngles = new Vector3(20, other.transform.rotation.eulerAngles.y, other.transform.rotation.eulerAngles.z);
            }
            else if (other.gameObject.transform.GetChild(0).GetChild(0).transform.rotation.x >= -40) 
            {
                Debug.Log("40");
                other.gameObject.transform.eulerAngles = new Vector3(40, other.transform.rotation.eulerAngles.y, other.transform.rotation.eulerAngles.z);
            }


                //CV.TriggerCamUpdate(this.transform);
                StartCoroutine(DialogueManager.Instance.ShowDialog(thisDialog, this.gameObject));
            switch (this.gameObject.GetComponent<NPCManager>().npcID)
            {
                case 0:
                    UI.SetTextNameBox("Teacher");
                    PI.lastNpcId = 0;
                    break;
                case 1:
                    UI.SetTextNameBox("Merchant");
                    break;
                case 2:
                    UI.SetTextNameBox("Craftsman");
                    break;
                case 3:
                    UI.SetTextNameBox("Wood Sculptor");
                    break;
                case 4:
                    UI.SetTextNameBox("Potter");
                    break;
                case 5:
                    UI.SetTextNameBox("Fisherman");
                    break;
                case 6:
                    UI.SetTextNameBox("Landlord");
                    break;
                case 7:
                    UI.SetTextNameBox("Farmer");
                    break;
                case 8:
                    if (SceneManager.GetActiveScene().buildIndex == 0)
                    {
                        UI.SetTextNameBox("Mysterious Child");
                        PI.lastNpcId = 8;
                        //hitInfo.transform.gameObject;
                    }
                    else
                        UI.SetTextNameBox("Brother");
                    break;
                case 9:
                    UI.SetTextNameBox("Widow");
                    break;
                case 10:
                    UI.SetTextNameBox("Shaman");
                    PI.lastNpcId = 10;
                    break;
                case 11:
                    UI.SetTextNameBox("Landlord's Son");
                    break;
                case 12:
                    UI.SetTextNameBox("Farmer's Son");
                    break;
                case 13:
                    UI.SetTextNameBox("Sculptor's Son");
                    break;
                case 14: //Brother Replacement
                    if (SceneManager.GetActiveScene().buildIndex == 0)
                        UI.SetTextNameBox("Mysterious Child");
                    else
                        UI.SetTextNameBox("Brother");
                    break;
                default:
                    Debug.LogWarning("Unknown ID");
                    break;
            }
        }
    }
}
