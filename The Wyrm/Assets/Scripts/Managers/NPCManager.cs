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

                if (lineNum >= maxLineNum)
                {
                    Debug.Log("Talking State talking: " + inTalking);
                    SetInTalkingState(false);
                }
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
                GM.AddToTimer(20);
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
                dialogue[0] = "It is said that to bind a wyrm, 3 sacred relics must be collected and combined.";
                dialogue[1] = "Relics can come in many forms, but traditionally they take the form of artwork.";
                dialogue[2] = "Think about what I said in the lesson today, my boy.";
                dialogue[3] = "You’ll do well to understand it more, it is quite the important subject…";
                break;
            case 1: // Merchant
                dialogue[0] = "Oi kiddo, how are ya?";
                dialogue[1] = "I have a little something’ in my house, you can have it";
                dialogue[2] = "That wretched landlord gave it to me and I want nothin’ to do with it!";
                dialogue[3] = "I don’t want to catch any of that karma comin’ his way…";
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
                dialogue[0] = "Heeey, you look familiar, but I still can't recognize you…";
                dialogue[1] = "Eh, whatever. I’m just tired, that's all.";
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
                dialogue[0] = "I remember when I had to chase all you rascals out of my fields ha ha!";
                dialogue[1] = "...";
                dialogue[2] = "Not so much anymore, though…";
                break;
            case 8: // Mysterious Child
                dialogue[0] = "Hey friend… that ruined house by the village entrance is quite the sight isn't it?";
                dialogue[1] = "I’d go check it out but my mom said it's dangerous";
                break;
            case 9: // Widdow
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
