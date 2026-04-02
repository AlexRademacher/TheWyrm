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

    private float CameraX;


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

            //Debug.Log(other.gameObject.transform.GetChild(0).GetChild(0).transform.rotation.x);  

            /*if (other.gameObject.transform.GetChild(0).GetChild(0).transform.rotation.x >= -0.17 && other.gameObject.transform.GetChild(0).GetChild(0).transform.rotation.x < 0)  Reads the X rotation of the first person camera
             * and if its in the range of 0 - 20 (0 to -0.17) sets the player rotation temporarily to 20.  
            {
                Debug.Log("0 - 20");
                CameraX = 20;
            }
            else if (other.gameObject.transform.GetChild(0).GetChild(0).transform.rotation.x >= -0.25 && other.gameObject.transform.GetChild(0).GetChild(0).transform.rotation.x < -0.17)
            {
                Debug.Log("20 - 30");
                CameraX = 40;
            }
            else if (other.gameObject.transform.GetChild(0).GetChild(0).transform.rotation.x >= -0.34 && other.gameObject.transform.GetChild(0).GetChild(0).transform.rotation.x < -0.25)
            {
                Debug.Log("30 - 40");
                CameraX = 40;
            }
            else if (other.gameObject.transform.GetChild(0).GetChild(0).transform.rotation.x >= -0.45 && other.gameObject.transform.GetChild(0).GetChild(0).transform.rotation.x < -0.34)
            {
                Debug.Log("40 - 50");
                CameraX = 50;
            }
            else if (other.gameObject.transform.GetChild(0).GetChild(0).transform.rotation.x >= -0.49 && other.gameObject.transform.GetChild(0).GetChild(0).transform.rotation.x < -0.45)
            {
                Debug.Log("50 - 60");
                CameraX = 60;
            }
            else if (other.gameObject.transform.GetChild(0).GetChild(0).transform.rotation.x >= -0.56 && other.gameObject.transform.GetChild(0).GetChild(0).transform.rotation.x < -0.49)
            {
                Debug.Log("60 - 70");
                CameraX = 70;
            }
            else if (other.gameObject.transform.GetChild(0).GetChild(0).transform.rotation.x >= -0.63 && other.gameObject.transform.GetChild(0).GetChild(0).transform.rotation.x < -0.56)
            {
                Debug.Log("70 - 80");
                CameraX = 80;
            }
            else if (other.gameObject.transform.GetChild(0).GetChild(0).transform.rotation.x >= -0.7 && other.gameObject.transform.GetChild(0).GetChild(0).transform.rotation.x < -0.63)
            {
                Debug.Log("80 - 90");
                CameraX = 90;
            }
            else 
            {
                Debug.Log("Else");
                CameraX = 0;
            }*/

            other.gameObject.transform.LookAt(this.transform.position);

            Debug.Log(other.gameObject.transform.GetChild(0).GetChild(0).name);
            //Add if statements to change amount of vertical rotation based on starting rotation (20)

            //other.gameObject.transform.eulerAngles = new Vector3(CameraX, other.transform.rotation.eulerAngles.y, other.transform.rotation.eulerAngles.z);

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
