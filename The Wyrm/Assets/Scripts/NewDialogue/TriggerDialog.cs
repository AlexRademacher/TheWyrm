using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    //Only trigger once
    Dialog thisDialog;
    bool triggered;
    private UIManager UI;


    // Start is called before the first frame update
    void Start()
    {
        UI = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered)
        {
            triggered = true;
            thisDialog = GetComponent<Dialog>();
            other.gameObject.transform.LookAt(this.transform.position);
            StartCoroutine(DialogueManager.Instance.ShowDialog(thisDialog, this.gameObject));
            switch (this.gameObject.GetComponent<NPCManager>().npcID)
            {
                case 0:
                    UI.SetTextNameBox("Teacher");
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
