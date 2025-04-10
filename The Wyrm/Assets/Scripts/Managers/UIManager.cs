using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private GameManager GM;
    private Player P;

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

    [Tooltip("The amount of relics you have"), SerializeField]
    private GameObject TimerCount;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();
        P = GameObject.Find("Player").GetComponent<Player>();
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

            if (GM.GetDeadState() && !RespawnMenu.activeSelf)
            {
                RespawnMenu.SetActive(true);
            }

            /*if (Input.GetKeyDown(KeyCode.R) && !PauseMenu.activeSelf)
            {
                NPCTextBox.SetActive(!NPCTextBox.activeSelf);
            }*/

            /*if (Input.GetKeyDown(KeyCode.E) && NPCTextBox.activeSelf)
            {
                SetTextBox("Blorg", "blah blah blah blah");
                SetTextBox("blah blah blah blah blah blahb lbahb bhalb hahblahb lhba");
            }*/
        }
    }

    //--------------------------------------------------------------------------------------------------------
    // Buttons

    public void MainStartButton()
    {
        MainMenu.SetActive(false);
        GM.CursorVisiblity(false);
        GM.SetPauseState(false);
        GM.PlayerKilledState(false);
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

    public void RespawnRespawnButton()
    {
        RespawnMenu.SetActive(false);
        GM.PlayerKilledState(false);
        P.Respawn();
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            LoadSceneManager lSM = GameObject.Find("Game Manager").GetComponent<LoadSceneManager>();
            if (0 <= SceneManager.sceneCountInBuildSettings && lSM != null)
                lSM.LoadScene(0);
        }
    }

    public void RespawnExitButton()
    {
        RespawnMenu.SetActive(false);
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

    public void SetTextBox(string name, string text)
    {
        if (NPCTextBox != null)
        {
            if (TextBoxText == null)
                TextBoxText = NPCTextBox.transform.GetChild(0).GetChild(1).gameObject;
            TextBoxName = NPCTextBox.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;

            if (!TextBoxText.activeSelf)
                TextBoxName.SetActive(true);
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

    public void SetTextBox(string text)
    {
        if (NPCTextBox != null && TextBoxText == null)
        {
            TextBoxText = NPCTextBox.transform.GetChild(0).GetChild(1).gameObject;
            TextBoxName = NPCTextBox.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;

            if (TextBoxText.activeSelf)
                TextBoxName.SetActive(false);
        }
        else if (NPCTextBox == null)
            Debug.LogWarning("Text Box not set up correctly");

        if (TextBoxText != null)
            TextBoxText.GetComponent<TextMeshProUGUI>().text = text;
        else
            Debug.LogWarning("Text Box Text not set up correctly");
    }

    public bool GetTextBoxActiveState()
    {
        return NPCTextBox.activeSelf;
    }

    public void SetTextBoxActiveState(bool newState)
    {
        NPCTextBox.SetActive(newState);
    }



    public void UpdateClockTimer(int time)
    {
        int hours = time / 60;
        int minutes = time % 60;

        if (hours < 10 && minutes < 10)
        {
            TimerCount.transform.GetComponent<TextMeshProUGUI>().text = "0" + hours + ":0" + minutes;
        }
        else if (hours < 10 && minutes >= 10)
        {
            TimerCount.transform.GetComponent<TextMeshProUGUI>().text = "0" + hours + ":" + minutes;
        }
        else if (hours >= 10 && minutes < 10)
        {
            TimerCount.transform.GetComponent<TextMeshProUGUI>().text = hours + ":0" + minutes;
        }
        else if (hours >= 10 && minutes >= 10)
        {
            TimerCount.transform.GetComponent<TextMeshProUGUI>().text = hours + ":" + minutes;
        }

    }

}

