using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameManager GM;

    [Header("Menus")]
    [Tooltip("the Main menu"), SerializeField]
    private GameObject MainMenu;
    
    [Tooltip("the Pause menu"), SerializeField]
    private GameObject PauseMenu;

    [Tooltip("the Respawn menu"), SerializeField]
    private GameObject RespawnMenu;

    [Header("Player UI")]
    [Tooltip("The npc text box"), SerializeField]
    private GameObject NPCTextBox;
    private GameObject TextBoxText;
    private GameObject TextBoxName;

    [Tooltip("The amount of relics you have"), SerializeField]
    private GameObject ItemCount;
    private GameObject ItemCounter;
    private int ItemAmount = 0;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!MainMenu.activeSelf)
        {
            if (GM.GetPauseState() && !PauseMenu.activeSelf)
            {
                PauseMenu.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.R) && !PauseMenu.activeSelf)
            {
                NPCTextBox.SetActive(!NPCTextBox.activeSelf);
            }

            if (Input.GetKeyDown(KeyCode.E) && NPCTextBox.activeSelf)
            {
                SetTextBox("Blorg", "blah blah blah blah");
                SetTextBox("blah blah blah blah blah blahb lbahb bhalb hahblahb lhba");
            }
        }
    }

    //--------------------------------------------------------------------------------------------------------
    // Buttons

    public void MainStartButton()
    {
        MainMenu.SetActive(false);
        GM.CursorVisiblity(false);
        GM.SetPauseState(false);
    }

    public void MainQuitButton()
    {
        GM.CloseGame();
    }

    public void PauseContinueButton()
    {
        PauseMenu.SetActive(false);
        GM.CursorVisiblity(false);
        GM.SetPauseState(false);
    }

    public void PauseExitButton()
    {
        PauseMenu.SetActive(false);
        MainMenu.SetActive(true);
    }

    //--------------------------------------------------------------------------------------------------------
    // Player UI

    public void UpdateItemCount(int ItemNum)
    {
        UpdateItemAmount(ItemNum);
    }

    private void UpdateItemAmount(int ItemNum)
    {
        if (ItemCount != null)
        {
            if (ItemCounter == null)
            {
                ItemCounter = ItemCount.transform.GetChild(0).gameObject;
            }

            ItemAmount += ItemNum;

            string count = ItemAmount + "x";

            ItemCounter.GetComponent<TextMeshProUGUI>().text = count;
        }
        else
            Debug.LogWarning("ItemCount not set up correctly");
    }

    private void SetTextBox(string name, string text)
    {
        if (NPCTextBox != null)
        {
            if (TextBoxText == null)
                TextBoxText = NPCTextBox.transform.GetChild(0).GetChild(1).gameObject;
            TextBoxName = NPCTextBox.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
        }
        else
            Debug.LogWarning("Text Box not set up correctly");

        if (TextBoxText != null)
            TextBoxText.GetComponent<TextMeshProUGUI>().text = text;
        else
            Debug.LogWarning("Text Box Text not set up correctly");

        if (TextBoxName != null)
            TextBoxName.GetComponent<TextMeshProUGUI>().text = name;
        else
            Debug.LogWarning("Text Box Name not set up correctly");
    }

    private void SetTextBox(string text)
    {
        if (NPCTextBox != null && TextBoxText == null)
        {
            TextBoxText = NPCTextBox.transform.GetChild(0).GetChild(1).gameObject;
        }
        else if (NPCTextBox == null)
            Debug.LogWarning("Text Box not set up correctly");

        if (TextBoxText != null)
            TextBoxText.GetComponent<TextMeshProUGUI>().text = text;
        else
            Debug.LogWarning("Text Box Text not set up correctly");
    }
}
