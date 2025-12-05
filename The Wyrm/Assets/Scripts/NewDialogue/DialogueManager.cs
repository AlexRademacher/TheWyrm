using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    private SpriteRenderer Renderer;
    private Canvas SpriteCanvas;

    /*
     * Questions in the box
     * progress questions on button presses
     */
    private GameManager GM;
    [SerializeField] public GameObject dialogBox;
    [SerializeField] Text dialogText;
    [SerializeField] float lettersPerSecond = 100;
    [SerializeField] GameObject buttonOne;
    [SerializeField] GameObject buttonTwo;

    GameObject currentNpc;

    public event Action OnShowDialog;
    public event Action OnHideDialog;

    Dialog dialog;
    int currentLine = 0;

    int skipNumFound = 1;

    bool isTyping;

    [SerializeField] bool choiceLine = false;
    [SerializeField] bool endingLine = false;
    [SerializeField] bool branchLine1n2 = false;
    [SerializeField] bool branchLine3n4 = false;
    [SerializeField] bool inBranch = false;
    [SerializeField] int currentBranch = 0;

    public static DialogueManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void ShowButtons() 
    {
        buttonOne.SetActive(true);
        buttonTwo.SetActive(true);
    }

    //Call this function to show a dialog
    public IEnumerator ShowDialog(Dialog dialog, GameObject npc) 
    {
        //Calls up the dialog box taking a Dialog scipt and a Gameobject (only needed for npcs just pass any gameobject otherwise)
        //Waits for the end of the current frame
        yield return new WaitForEndOfFrame();
        //not sure what this does honestly but its needed i think
        OnShowDialog?.Invoke();

        //sets the currentNPC to the passed in gameobject
        currentNpc = npc;
        //Debug.Log(currentNpc.name);

        //sets the dialog to the passed in dialog
        this.dialog = dialog;
        //sets the skip number
        skipNumFound = this.dialog.skipNum;
        //sets the dialog box to be active in the scene
        dialogBox.SetActive(true);
        //Types the dialog
        StartCoroutine(TypeDialog(dialog.Lines[0]));
    }


    //letters show up one by one
    public IEnumerator TypeDialog(string line) 
    {
        isTyping = true;    
        dialogText.text = "";
        if (line.StartsWith("&"))
            choiceLine = true;
        else 
            choiceLine = false;

        if (line.EndsWith("&"))
            endingLine = true; 
        else
            endingLine = false;

        if (line.EndsWith("^"))
            branchLine1n2 = true; 
        else
            branchLine1n2 = false;

        if (line.StartsWith("^"))
            branchLine3n4 = true;
        else
            branchLine3n4 = false;

            foreach (var letter in line.ToCharArray())
            {
                if (letter != '&' && letter != '^') 
                {
                    dialogText.text += letter;
                }
                yield return new WaitForSeconds((1f / lettersPerSecond) * Time.deltaTime);
            }
        isTyping = false;
    }

    private void Update()
    {

        if (choiceLine || branchLine1n2 || branchLine3n4) 
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (inBranch &&  !isTyping && dialogBox.activeInHierarchy == true && Input.GetKeyDown(KeyCode.E))
        {
            currentLine++;
            if (currentBranch == 1 && currentLine < dialog.Branch1.Count)
            {
                StartCoroutine(TypeDialog(dialog.Branch1[currentLine]));
            }
            else if (currentBranch == 2 && currentLine < dialog.Branch2.Count)
            {
                StartCoroutine(TypeDialog(dialog.Branch2[currentLine]));
            }
            else if (currentBranch == 3 && currentLine < dialog.Branch3.Count)
            {
                StartCoroutine(TypeDialog(dialog.Branch3[currentLine]));
            }
            else if (currentBranch == 4 && currentLine < dialog.Branch4.Count)
            {
                StartCoroutine(TypeDialog(dialog.Branch4[currentLine]));
            }
            else
            {
                Debug.Log("We are closing the dialog");


                Debug.Log(currentNpc.name + ": current npc name");
                if (currentNpc.name.Contains("Teacher") || currentNpc.name.Contains("Brother"))
                {
                    Debug.Log("NPC Check");
                    if (currentNpc.TryGetComponent<Item>(out Item itemScript))
                    {
                        Debug.Log("ParticleCheck");
                        itemScript.PickedUp();
                        GM.sacraficed();
                    }
                }

                inBranch = false;
                currentBranch = 0;

                dialogBox.SetActive(false);
                OnHideDialog?.Invoke();
                currentLine = 0;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;

                
            }
        }

        //if its an ending line close and reset the dialouge box
        if (endingLine && !isTyping && dialogBox.activeInHierarchy == true && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("We are closing the dialog");

            dialogBox.SetActive(false);
            OnHideDialog?.Invoke();
            currentLine = 0;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            if (currentNpc.name.Contains("Teacher") || currentNpc.name.Contains("Brother"))
            {
                if (currentNpc.TryGetComponent<Item>(out Item itemScript))
                {
                    itemScript.PickedUp();
                    GM.sacraficed();
                }
            }
        }

        


        //If the current dialog is not a choice progress normally
        if (dialogBox.activeInHierarchy == true && Input.GetKeyDown(KeyCode.E) && !isTyping && !choiceLine && !endingLine)
        {
            
            ++currentLine;
            if (currentLine < dialog.Lines.Count)
            {
                StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
            }
            else
            {
                Debug.Log("We are closing the dialoggggggggggg");
                inBranch = false;
                currentBranch = 0;

                dialogBox.SetActive(false);
                OnHideDialog?.Invoke();
                currentLine = 0;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;

                if (currentNpc.name.Contains("Teacher") || currentNpc.name.Contains("Brother"))
                {
                    if (currentNpc.TryGetComponent<Item>(out Item itemScript))
                    {
                        if (!transform.TryGetComponent<SpriteRenderer>(out Renderer))
                            if (transform.childCount > 0 && transform.GetChild(1).TryGetComponent<Canvas>(out SpriteCanvas))

                                if (Renderer != null)
                                    Renderer.enabled = false;

                        if (SpriteCanvas != null)
                            SpriteCanvas.enabled = false;

                        itemScript.PickedUp();
                        GM.sacraficed();
                    }
                }
            }
        }

        if (choiceLine || branchLine1n2 || branchLine3n4)
            ShowButtons();
    }

    //If the current dialog is a choice progress to the first option. (next line)
    public void choiceOne()
    {
        //if its not typing and its a choice line
        if (dialogBox.activeInHierarchy == true && !isTyping && choiceLine && !endingLine)
        {
            ++currentLine;
            if (currentLine < dialog.Lines.Count)
            {
                StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
            }
            else
            {
                dialogBox.SetActive(false);
                OnHideDialog?.Invoke();
                currentLine = 0;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            buttonOne.SetActive(false);
            buttonTwo.SetActive(false);
        }
        //if its a branch line and its not typing
        if (dialogBox.activeInHierarchy && !isTyping && branchLine1n2 && !endingLine && dialog.Branch1 != null) 
        {
            inBranch = true;
            currentBranch = 1;
            currentLine = 0;
            if (currentLine < dialog.Branch1.Count) 
            {
                StartCoroutine(TypeDialog(dialog.Branch1[currentLine]));
            }
            else
            {
                dialogBox.SetActive(false);
                OnHideDialog?.Invoke();
                currentLine = 0;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            buttonOne.SetActive(false);
            buttonTwo.SetActive(false);
        }

        if (dialogBox.activeInHierarchy && !isTyping && branchLine3n4 && !endingLine && dialog.Branch3 != null)
        {
            inBranch = true;
            currentBranch = 3;
            currentLine = 0;
            if (currentLine < dialog.Branch3.Count)
            {
                StartCoroutine(TypeDialog(dialog.Branch3[currentLine]));
            }
            else
            {
                dialogBox.SetActive(false);
                OnHideDialog?.Invoke();
                currentLine = 0;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            buttonOne.SetActive(false);
            buttonTwo.SetActive(false);
        }
        
    }

    //If the current dialog is a choice progress to the second option. (skipNum lines ahead)
    public void choiceTwo()
    {
        if (dialogBox.activeInHierarchy == true && !isTyping && choiceLine && !endingLine)
        {
            Debug.Log(skipNumFound);
            currentLine = currentLine + skipNumFound;
            Debug.Log(currentLine);
            if (currentLine < dialog.Lines.Count)
            {
                StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
            }
            else
            {
                dialogBox.SetActive(false);
                OnHideDialog?.Invoke();
                currentLine = 0;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            buttonOne.SetActive(false);
            buttonTwo.SetActive(false);
        }
        //if its a branch line and its not typing
        if (dialogBox.activeInHierarchy && !isTyping && branchLine1n2 && !endingLine && dialog.Branch2 != null)
        {
            inBranch = true;
            currentBranch = 2;
            currentLine = 0;
            if (currentLine < dialog.Branch2.Count)
            {
                StartCoroutine(TypeDialog(dialog.Branch2[currentLine]));
            }
            else
            {
                dialogBox.SetActive(false);
                OnHideDialog?.Invoke();
                currentLine = 0;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            buttonOne.SetActive(false);
            buttonTwo.SetActive(false);
        }
        if (dialogBox.activeInHierarchy && !isTyping && branchLine3n4 && !endingLine && dialog.Branch4 != null)
        {
            inBranch = true;
            currentBranch = 4;
            currentLine = 0;
            if (currentLine < dialog.Branch4.Count)
            {
                StartCoroutine(TypeDialog(dialog.Branch4[currentLine]));
            }
            else
            {
                dialogBox.SetActive(false);
                OnHideDialog?.Invoke();
                currentLine = 0;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            buttonOne.SetActive(false);
            buttonTwo.SetActive(false);
        }
        buttonOne.SetActive(false);
        buttonTwo.SetActive(false);
    }
}


