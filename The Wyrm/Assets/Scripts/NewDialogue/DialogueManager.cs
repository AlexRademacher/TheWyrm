using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
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
        yield return new WaitForEndOfFrame();
        OnShowDialog?.Invoke();

        currentNpc = npc;
        Debug.Log(currentNpc.name);

        this.dialog = dialog;
        skipNumFound = this.dialog.skipNum;
        dialogBox.SetActive(true);
        StartCoroutine(TypeDialog(dialog.Lines[0]));
    }


    //letters show up one by one
    public IEnumerator TypeDialog(string line) 
    {
        //change to something easily hidden
        isTyping = true;    
        dialogText.text = "";
        if (line.StartsWith("&"))
            choiceLine = true;
        else 
            choiceLine = false;

        //change to something easily hidden
        if (line.EndsWith("&"))
            endingLine = true; 
        else
            endingLine = false;

        foreach (var letter in line.ToCharArray())
        {
            if (letter != '&')
            {
                dialogText.text += letter;
            }
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        isTyping = false;
    }

    private void Update()
    {
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
        }

        if (choiceLine)
            ShowButtons();
    }

    //If the current dialog is a choice progress to the first option. (next line)
    public void choiceOne()
    {
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
    }
}


