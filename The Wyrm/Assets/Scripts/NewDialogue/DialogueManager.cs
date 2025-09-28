using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] Text dialogText;
    [SerializeField] int lettersPerSecond;

    public event Action OnShowDialog;
    public event Action OnHideDialog;

    Dialog dialog;
    int currentLine = 0;

    bool isTyping;

    [SerializeField] bool choiceLine = false;

    public static DialogueManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator ShowDialog(Dialog dialog) 
    {
        yield return new WaitForEndOfFrame();
        OnShowDialog?.Invoke();

        this.dialog = dialog;
        dialogBox.SetActive(true);
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

        foreach (var letter in line.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        isTyping = false;
    }

    private void Update()
    {
        if (dialogBox.activeInHierarchy == true && Input.GetKeyDown(KeyCode.Minus) && !isTyping && !choiceLine)
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
            }
        }
        else if (dialogBox.activeInHierarchy == true && !isTyping && choiceLine && Input.GetKeyDown(KeyCode.Alpha1))
        {
            //Do something based on choice here.  
            Debug.Log("triggered");
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
            }
        }
        else if (dialogBox.activeInHierarchy == true && !isTyping && choiceLine && Input.GetKeyDown(KeyCode.Alpha2)) 
        {
            //Do something based on choice here.  
            Debug.Log("triggered");
            currentLine = currentLine + 2;
            if (currentLine < dialog.Lines.Count)
            {
                StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
            }
            else
            {
                dialogBox.SetActive(false);
                OnHideDialog?.Invoke();
                currentLine = 0;
            }
        }
    }
}
