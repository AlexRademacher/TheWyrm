using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    private SaveDataManager SDM;

    private bool loading = false;

    private int loadingScreenNum = -1;

    [Header("Debugger")]
    [Tooltip("Turns on Debugging"), SerializeField]
    private bool debug;
    //Scene loadedScene;

    // Start is called before the first frame update
    void Start()
    {
        //loadedScene = SceneManager.GetActiveScene();

        if (GameObject.FindGameObjectsWithTag("SceneManager").Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SDM = GetComponent<SaveDataManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendToSetLevel(int buildNum)
    {
        if (buildNum < SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("Sending to " + SceneManager.GetSceneAt(buildNum).name);
            StartCoroutine(ChangeScene(buildNum));
        }
        else
            Debug.Log("Scene " + buildNum + " doesn't exist");
    }

    public void SendToVillage()
    {
        StartCoroutine(ChangeScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void SendToArena()
    {
        StartCoroutine(ChangeScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void Restart()
    {
        int buildNum = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(ChangeScene(buildNum, true));
    }

    private IEnumerator ChangeScene(int buildNum)
    {
        StartCoroutine(ChangeScene(buildNum, false));
        yield return null;
    }

    private IEnumerator ChangeScene(int buildNum, bool restart)
    {
        if (!loading)
        {
            if ((restart || buildNum != SceneManager.GetActiveScene().buildIndex))
            {
                if (buildNum < SceneManager.sceneCountInBuildSettings)
                {
                    loading = true;
                    ShowLoadingScreen(true);

                    if (!restart)
                        SDM.SaveData();

                    yield return new WaitForSeconds(2);

                    LoadScene(buildNum);

                    yield return new WaitForSeconds(2);

                    ShowLoadingScreen(false);
                    loading = false;
                }
                else if (buildNum >= SceneManager.sceneCountInBuildSettings)
                {
                    Debug.LogWarning("Build number is higher than what is listed in the build settings");
                }
            }
            else if (!restart && buildNum == SceneManager.GetActiveScene().buildIndex)
            {
                Debug.LogWarning("Currently within the same scene you are traveling to");
            }
        }
    }

    private void LoadScene(int buildNum)
    {
        DebugChangingActiveScene(SceneManager.GetActiveScene(), SceneManager.GetSceneByBuildIndex(buildNum));

        SceneManager.LoadScene(buildNum);
        
        StartCoroutine(ClearMainMenu());
        SDM.LoadData();
    }

    private IEnumerator ClearMainMenu()
    {
        yield return new WaitForSeconds(.01f);
        ShowLoadingScreen(true);
        if (GameObject.Find("Canvas").TryGetComponent<UIManager>(out UIManager UI))
        {
            //UI.MainStartButton();
            //UI.CutSceneIntroContinue();
            UI.ControlsScreenContinue();
        }
    }

    private void ShowLoadingScreen(bool screenState)
    {
        if (GameObject.Find("Canvas").TryGetComponent<UIManager>(out UIManager UI))
        {
            if (screenState)
            {
                if (loadingScreenNum != -1)
                    UI.LoadingScreenState(screenState, loadingScreenNum);
                else
                    loadingScreenNum = UI.LoadingScreenState(screenState);
            }
            else
            {
                loadingScreenNum = UI.LoadingScreenState(screenState);
            }
        }
        else if (GameObject.Find("Canvas") == null)
            Debug.LogWarning("Couldn't find Canvas");
        else
            Debug.LogWarning("Couldn't find UIManager");
    }

    private void DebugChangingActiveScene(Scene current, Scene next)
    {
        if (debug)
        {
            string currentName = current.name;

            if (currentName == null) // Scene1 has been removed
                Debug.LogWarning("The last scene has been removed");

            Debug.Log("The scene has been changed from " + currentName + " to " + next.name);
        }
    }
}
