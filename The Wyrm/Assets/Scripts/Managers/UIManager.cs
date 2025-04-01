using System.Collections;
using System.Collections.Generic;
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

}
