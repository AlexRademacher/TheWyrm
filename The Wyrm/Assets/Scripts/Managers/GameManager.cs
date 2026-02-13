using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    UIManager UI;

    [Header("Timer")]
    [Tooltip("True is the timer that counts over time, False is the timer that counts over actions"), SerializeField]
    private bool timerType = true;
    [Tooltip("The time we start with"), SerializeField]
    private int startTimer = 0;
    private int timer = 0;
    private int oldTime = 0;
    private bool timerAddRest = false;

    [SerializeField]
    private Material[] shaders;
    [SerializeField]
    private GameObject[] lightings;

    [SerializeField]
    private GameObject FinalRelics;

    private bool started;
    private bool paused;
    private bool dead;

    private bool loading;

    private bool talking;

    private int relicsFound = 0;

    private int sacrificed = 0;

    private bool credits = false;


    // Start is called before the first frame update
    void Start()
    {
        UI = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (SceneManager.GetActiveScene().buildIndex % 2 == 0)
        {
            AddToTimer(startTimer);
            //AddToTimer(1200);
            //timer += 420;
        }
        else
        {
            UI.HideClock();
        }

        //Debug.Log(SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(timer/60 + ":"+ timer%60);
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

        if ( oldTime != timer)
        {
            if (SceneManager.GetActiveScene().buildIndex != 5)
                UI.UpdateClockTimer(timer);
            else
                UI.UpdateClockTimer(300);
            oldTime = timer;
        }


        if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            if (sacrificed >= 2)
            {
                FinalRelics.SetActive(true);

                if (GameObject.Find("Scene Manager").TryGetComponent<LoadSceneManager>(out LoadSceneManager lSM))
                {
                    lSM.SendToArena();
                }

                /*if (GameObject.Find("Scene Manager") != null)
                {
                    if (GameObject.Find("Scene Manager").TryGetComponent<LoadSceneManager>(out LoadSceneManager lSM))
                    {
                        lSM.SendToArena();
                    }
                    else
                        Debug.LogWarning("Scene Manager not set up right");
                }
                else
                    Debug.LogWarning("Scene Manager not found");*/
            }
        }
    }

    public void sacraficed()
    {
        sacrificed = sacrificed + 1;
        Debug.Log("Sacrificed!");
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

        if (SceneManager.GetActiveScene().buildIndex != 5)
        {
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
                CursorVisiblity(true);
                timer = 0;
            }
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

        if (lightings != null)
        {
            foreach (GameObject light in lightings)
            {
                light.SetActive(false);
            }
            lighting.SetActive(true);
        }
        else
            Debug.LogWarning("Missing Sky box and lighting");
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

        //Debug.Log("A Relic found on table!");
        //Debug.Log("Relic placed amount: " + relicsFound);
        //Debug.Log("relicsFound == 3: " + (relicsFound == 3));

        if (relicsFound == 3)
        {
            if (GameObject.Find("Scene Manager") != null)
            {
                if (GameObject.Find("Scene Manager").TryGetComponent<LoadSceneManager>(out LoadSceneManager lSM))
                {
                    //relicsFound = 0;

                    if (SceneManager.GetActiveScene().buildIndex == 6)
                        UI.CutsceneArena3();
                    else
                    {
                        if (SceneManager.GetActiveScene().name.Contains("Level") || SceneManager.GetActiveScene().name.Contains("level"))
                        {
                            lSM.SendToArena();
                        }
                        else if (SceneManager.GetActiveScene().name.Contains("Arena") || SceneManager.GetActiveScene().name.Contains("arena"))
                        {
                            if (SceneManager.GetActiveScene().buildIndex == 1)
                                UI.CutsceneArena1();
                            else if (SceneManager.GetActiveScene().buildIndex == 3)
                                UI.CutsceneArena2();
                        }

                    }
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
