using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    UIManager UI;

    private int timer = 0;
    private int oldTime = 0;
    private bool timerAddRest = false;
    [Header("Timer")]
    [Tooltip("True is the timer that counts over time, False is the timer that counts over actions"), SerializeField]
    private bool timerType = true;

    [SerializeField]
    private Material[] shaders;
    [SerializeField]
    private GameObject[] lightings;

    private bool started;
    private bool paused;
    private bool dead;

    private bool loading;

    private bool talking;

    private int relicsFound = 0;

    private bool credits = false;

    // Start is called before the first frame update
    void Start()
    {
        UI = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            AddToTimer(300);
            //AddToTimer(1200);
            //timer += 420;
        }
        else
        {
            UI.HideClock();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // allows player to free cursor by pressing escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused && !GetLoadingState() && !GetCreditsState())
            {
                PauseGame();
            }
        }

        if (GetTimerType() && GetStartedState() && !timerAddRest && !talking && !paused && !dead && !loading)
            StartCoroutine(goingPastTimer());

        if (oldTime != timer)
        {
            UI.UpdateClockTimer(timer);
            oldTime = timer;
        }

    }

    //--------------------------------------------------------------------------------------------------------
    // Get and set

    public bool GetTimerType()
    {
        return timerType;
    }

    public int GetTime()
    {
        return timer;
    }

    public void SetTime(int newtime)
    {
        timer = newtime;
    }

    private IEnumerator goingPastTimer()
    {
        timerAddRest = true;
        yield return new WaitForSeconds(1);
        AddToTimer(1);
        timerAddRest = false;
    }

    public void AddToTimer(int addedTime)
    {
        timer += addedTime;

        if (timer >= 300 && timer <= 540)
        {
            UpdateDayLook(true, new Color(141, 67, 67), shaders[0], lightings[0]);
        }
        else if (timer > 540 && timer <= 960)
        { 
            UpdateDayLook(false, new Color(241, 188, 117), shaders[1], lightings[1]);
        } 
        else if (timer > 960 && timer <= 1200)
        {
            UpdateDayLook(true, new Color(141, 67, 67), shaders[2], lightings[0]);
        }
        else if (timer > 1200 && timer < 1440)
        {
            UpdateDayLook(true, new Color(50, 12, 15), shaders[3], lightings[2]);
        }

        if (timer >= 1440)
        {
            //CloseGame();
            UI.ShowOutOfTimeEnding();
            timer = 0;
        }

    }

    public bool GetStartedState()
    {
        return started;
    }

    public void SetStartedState(bool NewState)
    {
        started = NewState;
    }

    public bool GetPauseState()
    {
        return paused;
    }

    public void SetPauseState(bool NewState)
    {
        paused = NewState;
    }

    public bool GetDeadState()
    {
        return dead;
    }

    public bool GetLoadingState()
    {
        return loading;
    }

    public void SetLoadingState(bool talkingState)
    {
        loading = talkingState;
    }

    public bool GetTalking()
    {
        return talking;
    }

    public void SetTalking(bool talkingState)
    {
        talking = talkingState;
    }

    public bool GetCreditsState()
    {
        return credits;
    }

    public void SetCreditsState(bool newState)
    {
        credits = newState;
    }

    //----------------------------------------------------------------------------------------------------------------------
    // Game States

    public void PauseGame()
    {
        paused = !GetPauseState();
        CursorVisiblity(GetPauseState());
    }

    public void PlayerKilledState(bool newState)
    {
        dead = newState;
        CursorVisiblity(dead);
    }

    //----------------------------------------------------------------------------------------------
    // Look of the day

    private void UpdateDayLook(bool fogState, Color fogColor, Material skyLook, GameObject lighting)
    {
        //RenderSettings.fog = fogState;
        //RenderSettings.fogColor = fogColor;

        RenderSettings.skybox = skyLook;

        foreach (GameObject light in lightings)
        {
            light.SetActive(false);
        }
        lighting.SetActive(true);

    }

    //----------------------------------------------------------------------------------------------------------------------
    // Cursor

    /// <summary>
    /// Call to hide or show the cursor
    /// </summary>
    public void CursorVisiblity(bool visibleState)
    { 
        if (!visibleState)
        {
            Time.timeScale = 1;
            HideCursor();
        }
        else
        {
            Time.timeScale = 0;
            ShowCursor();
        }
    }

    /// <summary>
    /// Hides and locks the cursor
    /// </summary>
    private void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    /// <summary>
    /// Shows and frees the cursor
    /// </summary>
    private void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    //----------------------------------------------------------------------------------------------
    // Relics ending

    public void RelicFound()
    {
        relicsFound = relicsFound + 1;

        if (relicsFound == 3)
        {
            if (GameObject.Find("Scene Manager") != null)
            {
                if (GameObject.Find("Scene Manager").TryGetComponent<LoadSceneManager>(out LoadSceneManager lSM))
                {
                    relicsFound = 0;
                    lSM.SendToVillage();
                }
                else
                    Debug.LogWarning("Scene Manager not set up right");
            }
            else
                Debug.LogWarning("Scene Manager not found");
        }
    }

    //----------------------------------------------------------------------------------------------
    // Close Game

    public void CloseGame()
    {
        QuitGame();
    }

    private void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }
}
