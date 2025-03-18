using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorManager : MonoBehaviour
{
    private LoadSceneManager lSM;

    [Header("Door")]
    [Tooltip("Where the door is going"), SerializeField]
    private int buildNum;

    // Start is called before the first frame update
    void Start()
    {
        lSM = GameObject.Find("Game Manager").GetComponent<LoadSceneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            try {
                if (buildNum <= SceneManager.sceneCountInBuildSettings && lSM != null)
                    lSM.LoadScene(buildNum);
            }
            catch (ArgumentException)
            {
                Debug.LogWarning("The scene after this one does not exist in Build Settings");
            }
        }
    }
}

