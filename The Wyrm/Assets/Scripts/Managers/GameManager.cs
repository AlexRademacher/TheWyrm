using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    UIManager UI;

    private int timer = 420;
    private int oldTime = 0;

    private bool paused;
    private bool dead;

    private int relicsFound = 0;

    // Start is called before the first frame update
    void Start()
    {
        UI = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // allows player to free cursor by pressing escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                PauseGame();
            }
        }

        if (oldTime != timer)
        {
            UI.UpdateClockTimer(timer);
            oldTime = timer;
        }

    }

    //--------------------------------------------------------------------------------------------------------
    // Get and set

    public int GetTime()
    {
        return timer;
    }

    public void AddToTimer(int addedTime)
    {
        timer += addedTime;

        if (timer >= 1440)
        {

        }

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

    //----------------------------------------------------------------------------------------------------------------------
    // Game States

    private void PauseGame()
    {
        paused = !GetPauseState();
        CursorVisiblity(GetPauseState());
    }

    public void PlayerKilledState(bool newState)
    {
        dead = newState;
        CursorVisiblity(dead);
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

        Debug.Log("WE DID IT WE FOUND IT LETS GOOOO");
        Debug.Log(relicsFound);

        if (relicsFound == 3)
        {
            CloseGame();
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
