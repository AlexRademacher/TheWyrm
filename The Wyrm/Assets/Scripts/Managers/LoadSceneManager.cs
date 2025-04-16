using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    [Header("Debugger")]
    [Tooltip("Turns on Debugging"), SerializeField]
    private bool debug;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(int buildNum)
    {
        DebugChangingActiveScene(SceneManager.GetActiveScene(), SceneManager.GetSceneByBuildIndex(buildNum));

        SceneManager.LoadScene(buildNum); 
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
