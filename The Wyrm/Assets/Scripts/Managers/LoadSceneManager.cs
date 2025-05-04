using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    SaveDataManager SDM;

    [Header("Debugger")]
    [Tooltip("Turns on Debugging"), SerializeField]
    private bool debug;

    // Start is called before the first frame update
    void Start()
    {
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
            ChangeScene(buildNum);
        }
        else
            Debug.Log("Scene " + buildNum + " doesn't exist");
    }

    public void SendToVillage()
    {
        ChangeScene(0);
    }

    public void SendToArena()
    {
        ChangeScene(1);
    }

    private void ChangeScene(int buildNum)
    {
        if (buildNum != SceneManager.GetActiveScene().buildIndex && buildNum < SceneManager.sceneCountInBuildSettings)
        {
            SDM.SaveData();
            LoadScene(buildNum);
        }
        else if (buildNum == SceneManager.GetActiveScene().buildIndex)
        {
            Debug.LogWarning("Currently within the same scene you are traveling to");
        }
        else if (buildNum >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogWarning("Build number is higher than what is listed in the build settings");
        }
    }

    private void LoadScene(int buildNum)
    {
        DebugChangingActiveScene(SceneManager.GetActiveScene(), SceneManager.GetSceneByBuildIndex(buildNum));

        SceneManager.LoadScene(buildNum); 

        SDM.LoadData();
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
