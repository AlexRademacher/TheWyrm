using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorManager : MonoBehaviour
{
    private CameraManager CM;
    private LoadSceneManager lSM;

    [Header("Door")]
    [Tooltip("Where the door is going"), SerializeField]
    private int buildNum;


    private bool canOpen;
    [Tooltip("If the door starts as locked or open"), SerializeField]
    private bool locked;
    [Tooltip("The key needed to unlock the door"), SerializeField]
    private GameObject keyNeeded;


    // Start is called before the first frame update
    void Start()
    {
        CM = GameObject.Find("Cameras").GetComponent<CameraManager>();
        lSM = GameObject.Find("Scene Manager").GetComponent<LoadSceneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canOpen && !locked)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SendToNewScene();
            }
        }
    }

    public bool GetLockedState()
    {
        return locked;
    }

    public void SetCanOpenState(bool newState)
    {
        canOpen = newState;
    }

    public void UnlockDoor(GameObject key)
    {
        if (key != keyNeeded)
        {
            Debug.Log("Wrong Key");
        }
        else
        {
            SetCanOpenState(true);
        }
    }

    private void SendToNewScene()
    {
        try
        {
            SetCanOpenState(false);
            CM.SetCameraPerspective(!CM.GetCameraPerspective());
            lSM.SendToSetLevel(buildNum);
        }
        catch (ArgumentException)
        {
            Debug.LogWarning("The scene after this one does not exist in Build Settings");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
        }
    }
}

