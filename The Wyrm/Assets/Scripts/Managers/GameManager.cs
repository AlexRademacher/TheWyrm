using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool paused;

    // Start is called before the first frame update
    void Start()
    {

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
    }

    //--------------------------------------------------------------------------------------------------------
    // Get and set

    public bool GetPauseState()
    {
        return paused;
    }

    public void SetPauseState(bool NewState)
    {
        paused = NewState;
    }

    //----------------------------------------------------------------------------------------------------------------------
    // Game States

    private void PauseGame()
    {
        paused = !GetPauseState();
        CursorVisiblity(GetPauseState());
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
