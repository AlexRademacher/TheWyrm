using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [Header("Loading Screen")]
    [Tooltip("The different loading screen backgrounds that can show up while loading"), SerializeField]
    private Sprite[] loadingScreens;
    private int screenNum = -1;

    private void OnEnable()
    {
        UpdateRespawnBackground();
    }

    public int GetCurrentScreenNum()
    {
        return screenNum;
    }

    public void SetCurrentScreenNum(int newNum)
    {
        screenNum = newNum;
    }

    public void UpdateRespawnBackground()
    {
        if (loadingScreens != null)
        {
            Image background = GetComponent<Image>();

            if (loadingScreens != null && loadingScreens.Length > 0)
            {
                if (screenNum < 0)
                {
                    Debug.LogWarning(screenNum);
                    screenNum = Random.Range(0, loadingScreens.Length);
                    background.sprite = loadingScreens[screenNum];
                    Debug.LogWarning(screenNum);
                }
                else
                {
                    background.sprite = loadingScreens[screenNum];
                    Debug.LogWarning(screenNum);
                }
            }
            else if (loadingScreens.Length == 0)
            {
                Debug.LogWarning("no sprites within loadingScreens");
            }
            else
            {
                Debug.LogWarning("loadingScreens not set up correctly");
            }
        }
    }
}
