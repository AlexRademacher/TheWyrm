using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    private GameObject[] playerInventory;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveData()
    {
        GetPlayerInventory();
    }

    public void LoadData()
    {
        SetPlayerInventory();
    }


    //--------------------------------------------------------------------------------------------------------
    // Player Inventory

    private void GetPlayerInventory()
    {
        if (GameObject.Find("Player").TryGetComponent<PlayerInventory>(out PlayerInventory PI))
        {
            playerInventory = new GameObject[PI.GetInventory().Length];

            int i = 0;

            foreach (GameObject item in PI.GetInventory())
            {
                playerInventory[i] = item;
                i++;
            }
        }
        else
            Debug.LogWarning("Player couldn't be found in the scene");
    }

    private void SetPlayerInventory()
    {
        if (GameObject.Find("Player").TryGetComponent<PlayerInventory>(out PlayerInventory PI))
        {
            if (playerInventory != null)
            {
                foreach (GameObject item in playerInventory)
                {
                    //PI.AddToInventory(item);
                }
            }
            else
                Debug.LogWarning("Haven't pull data from player inventory yet");
        }
        else
            Debug.LogWarning("Player couldn't be found in the scene");
    }
}
