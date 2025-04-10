using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    private GameManager GM;
    private UIManager UI;

    private bool canNeverSpeak = false;
    private bool inTalking = false;

    [Header("NPC Stats")]
    [Tooltip("The ID that goes with the dialogue"), SerializeField]
    private int npcID;
    [Tooltip("The length of dialogue"), SerializeField]
    private int lengthNum;
    private int lineNum = 0;
    private int maxLineNum = 0;

    private string[] dialogue;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();
        UI = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canNeverSpeak && inTalking && npcID != -1)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!UI.GetTextBoxActiveState())
                {
                    UI.SetTextBoxActiveState(true);
                }

                if (UI.GetTextBoxActiveState())
                {
                    ReadDialogue();
                }
                    
            }
        }
    }

    public void SetInTalkingState(bool newState)
    {
        inTalking = newState;
    }

    private void ReadDialogue()
    {
        if (dialogue == null)
        {
            dialogue = GetDialogue(npcID, lengthNum);

            maxLineNum = dialogue.Length;
        }

        if (dialogue != null && dialogue.Length > 0)
        {
            if (lineNum < maxLineNum)
            {
                UI.SetTextBox(gameObject.name.Substring(3), dialogue[lineNum]);
                lineNum++;
                GM.AddToTimer(20);
            }
            else
            {
                UI.SetTextBox("New Text", "New Text");
                lineNum = 0;
                UI.SetTextBoxActiveState(false);
                canNeverSpeak = true;
            }
        }


    }

    private string[] GetDialogue(int ID, int lengthNum)
    {
        if (lengthNum <= 0)
        {
            Debug.LogWarning("lengthNum is less than 1");
            return null;
        }

        Debug.Log(gameObject.name + " has Id of: " + ID);



        string[] dialogue = new string[lengthNum];

        switch(ID)
        {
            case 0:
                dialogue[0] = "It is said that to bind a wyrm, 3 sacred relics must be collected and combined.";
                dialogue[1] = "Relics can come in many forms, but traditionally they take the form of artwork.";
                break;
            case 1:
                dialogue[0] = "The wood sculptor who lives in the south east of the village finally started selling off his old antiques.";
                dialogue[1] = "I was lucky enough to buy this dragon statue.";
                break;
            case 2:
                dialogue[0] = "I accidentally left a bag of my crafts in the commune...";
                dialogue[1] = "I think I left that neat bronze engraving there too...";
                break;
            case 3:
                dialogue[0] = "I sold my dragon statue to that damn merchant for too cheap! I've been had!";
                break;
            case 4:
                dialogue[0] = "That painting at the shrine in the pagoda is nice, isn't it? I should know, I used to own it!";
                break;
            case 5:
                dialogue[0] = "I go to the river to fish everyday.";
                dialogue[1] = "Its not the same without my son...";
                break;
            case 6:
                dialogue[0] = "Hey brat!";
                dialogue[1] = "Steer clear from my house if you know whats good for ya!";
                dialogue[2] = "Theres something making a ruckus in there.";
                break;
            case 7:
                dialogue[0] = "If these storms keep up, the rice paddies will be destroyed.";
                dialogue[1] = "We're already far too close to another famine, too...";
                break;
            case 8:
                dialogue[0] = "Hey friend! If you need help with classes, the teacher lives just left up ahead, he'll help you out.";
                break;
            default:
                Debug.LogWarning("Unknown ID");
                break;
        }

        return dialogue;
    }
}
