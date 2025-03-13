using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // lock cursor
        CursorVisiblity(false);
    }

    // Update is called once per frame
    void Update()
    {
        // allows player to free cursor by pressing escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CursorVisiblity(!Cursor.visible);
        }
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
}
