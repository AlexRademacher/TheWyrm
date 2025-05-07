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

    [Tooltip("The Hide Prompt"), SerializeField]
    private GameObject HidePrompt;

    [Tooltip("The Map"), SerializeField]
    private GameObject Map;

    [Header("Screens")]
    [Tooltip("The Loading Screen"), SerializeField]
    private GameObject LoadingScreen;

    [Tooltip("The Cutscenes"), SerializeField]
    private GameObject[] Cutscenes;
    [Tooltip("The Cutscene 1 teleport"), SerializeField]
    private GameObject position;

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

            if (Input.GetKeyDown(KeyCode.M) && SceneManager.GetActiveScene().buildIndex == 0 && !GM.GetPauseState() && !GM.GetLoadingState() && !GM.GetDeadState() && !GM.GetTalking())
            {
                MapState();
            }
        }
    }

    //--------------------------------------------------------------------------------------------------------
    // Buttons

    public void MainStartButton()
    {
        Cutscenes[0].SetActive(true);
    }

    public void MainQuitButton()
    {
        GM.CloseGame();
    }

    public void CutSceneIntroContinue()
    {
        GM.SetStartedState(true);
        Cutscenes[0].SetActive(false);
        MainMenu.SetActive(false);
        GM.CursorVisiblity(false);
        GM.SetPauseState(false);
        GM.PlayerKilledState(false);

        if (GameObject.Find("Background Music").transform.GetChild(0).TryGetComponent<AudioSource>(out AudioSource audio))
        {
            audio.Play();
        }
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

        if (GameObject.Find("Background Music").transform.GetChild(0).TryGetComponent<AudioSource>(out AudioSource audio))
        {
            audio.Pause();
        }
    }

    public void RespawnRespawnButton()
    {
        RespawnMenu.SetActive(false);
        GM.PlayerKilledState(false);
        P.Respawn();
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            LoadSceneManager lSM = GameObject.Find("Scene Manager").GetComponent<LoadSceneManager>();
            lSM.Restart();
        }
    }

    public void RespawnExitButton()
    {
        RespawnMenu.SetActive(false);
        MainMenu.SetActive(true);

        if (GameObject.Find("Background Music").transform.GetChild(0).TryGetComponent<AudioSource>(out AudioSource audio))
        {
            audio.Pause();
        }
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

        if (TimerCount != null)
        {
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

    public void HideClock()
    {
        TimerCount.SetActive(false);
    }

    public void swapHideState() 
    {
        HidePrompt.SetActive(!HidePrompt.activeInHierarchy);
    }

    public void MapState()
    {
        if (!Map.activeSelf)
        {
            Map.SetActive(true);
        }
        else
        {
            Map.SetActive(false);
        }
        
    }

    //--------------------------------------------------------------------------------------------------------
    // Screens

    public int LoadingScreenState(bool newState)
    {
        LoadingScreen.SetActive(newState);
        GM.SetLoadingState(newState);

        if (newState && LoadingScreen.TryGetComponent<LoadingScreen>(out LoadingScreen LS))
        {
            return LS.GetCurrentScreenNum();
        }
        else
        {
            return -1;
        }
    }

    public void LoadingScreenState(bool newState, int screenNum)
    {
        if (LoadingScreen.TryGetComponent<LoadingScreen>(out LoadingScreen LS))
        {
            if (screenNum >= 0)
                LS.SetCurrentScreenNum(screenNum);
            else
                Debug.LogWarning("LoadingScreen was sent a -1");
        }

        LoadingScreen.SetActive(newState);
        GM.SetLoadingState(newState);
    }

    public void ShowTeacherCutscene()
    {
        Cutscenes[1].SetActive(true);
        GM.PauseGame();
    }

    public void CutSceneTeacherContinue()
    {
        PauseContinueButton();

        P.GetComponent<CharacterController>().enabled = false;
        P.transform.position = position.transform.position;
        P.GetComponent<CharacterController>().enabled = true;

        Cutscenes[1].SetActive(false);
    }

    public void ShowOutOfTimeEnding()
    {
        Cutscenes[3].SetActive(true);
    }

    public void CutSceneOutOfTimeEndingContinue()
    {
        GM.CloseGame();
    }

    public void ShowVictoryEnding()
    {
        Cutscenes[2].SetActive(true);
    }

    public void CutSceneVictoryEndingContinue()
    {
        GM.CloseGame();
    }
}

