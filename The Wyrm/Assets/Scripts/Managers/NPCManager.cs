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
        if (Input.GetKeyDown(KeyCode.E) && npcID == 5)
        {
            //Debug.Log("canNeverSpeak State: " + inTalking);
            //Debug.Log("Talking State: " + inTalking);
            //Debug.Log("ID is: " + npcID);
        }

        if (!canNeverSpeak && inTalking && npcID != -1)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Debug.Log("Talking State: " + inTalking);
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
            Debug.Log("new dialogue " + npcID);
            maxLineNum = dialogue.Length;
        }

        if (dialogue != null && dialogue.Length > 0)
        {
            if (lineNum < maxLineNum)
            {
                GM.SetTalking(true);
                UI.SetTextBox(gameObject.name.Substring(3), dialogue[lineNum]);
                lineNum++;
            }
            else
            {
                Debug.Log("Talking State after talking: " + inTalking);
                SetInTalkingState(false);
                lineNum = 0;
                dialogue = null;
                //canNeverSpeak = true;

                UI.SetTextBox("New Text", "New Text");
                UI.SetTextBoxActiveState(false);

                GM.SetTalking(false);
                //GM.AddToTimer(20);
                Debug.Log("Talking State cleaning up: " + inTalking);
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
            case 0: // Teacher
                dialogue[0] = "Think about what I said in the lesson today, my boy.";
                dialogue[1] = "You’ll do well to understand it more, it is quite the important subject…";
                break;
            case 1: // Merchant
                dialogue[0] = "Oi kiddo, how are ya?";
                dialogue[1] = "I'll tell ya what, that potter holds on to some strange items...";
                dialogue[2] = "Personally, I wouldn't keep that vile jade pendant around...";
                dialogue[3] = "Wouldn't wanna catch any of the karma that comes with it…";
                break;
            case 2: // Craftman
                dialogue[0] = "Why has that greedy landlord has been getting rid of all of his kids' possessions I wonder..?";
                dialogue[1] = "...";
                dialogue[2] = "No idea, but there’s no way he's suddenly become some minimalist, I’ll tell you that much!";
                break;
            case 3: // Sculptor
                dialogue[0] = "Don’t go near the Pagoda my little fellow. That Shaman bastard seems to have taken it for his own.";
                dialogue[1] = "I’ll tell you, he is a nasty character. All he does nowadays is pray and demand pity.";
                dialogue[2] = "He never cared about the village after all…";
                break;
            case 4: // Potter
                dialogue[0] = "Hey you, boy!";
                dialogue[1] = "Don't listen to what people say about me!";
                dialogue[2] = "Especially that slanderous merchant, calling me crazy!";
                dialogue[3] = "I'm not crazy... I'm not...";
                break;
            case 5: // Fisherman
                dialogue[0] = "Ay littlun, don’t be playin’ too close to this ‘ere river! ";
                dialogue[1] = "You littluns need ought to be safer.";
                dialogue[2] = "Aren’t many of ye left…";
                break;
            case 6: // Landlord
                dialogue[0] = "H-hey! Steer clear of my abode, rat!";
                dialogue[1] = "Nothings wrong and I don’t need a reason, just get lost!";
                dialogue[2] = "Children are nothing but trouble here…";
                break;
            case 7: // Farmer
                dialogue[0] = "A sorry sight, that collapsed house is...";
                dialogue[1] = "It was that landlord's other home, wasn't it?";
                dialogue[2] = "Odd, he doesn't seem too bothered by it, I wonder why...";
                break;
            case 8: // Mysterious Child
                dialogue[0] = "Hey friend, you gotta come to class, you’ll be late!";
                dialogue[1] = "Come on, I’ll take you there!”";
                break;
            case 9: // Widow
                dialogue[0] = "...";
                dialogue[1] = "You-";
                dialogue[2] = "You aren’t real…";
                break;
            case 10: // Shaman
                dialogue[0] = "Now what do you think you are doing in here, boy?";
                dialogue[1] = "Don’t give me that look, how disrespectful!";
                dialogue[2] = "Leave at once! This is no place for children";
                break;
            default:
                Debug.LogWarning("Unknown ID");
                break;
        }

        return dialogue;
    }
}
