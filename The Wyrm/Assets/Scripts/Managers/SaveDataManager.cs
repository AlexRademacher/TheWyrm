
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveDataManager : MonoBehaviour
{
    private List<GameObject> playerInventory = new List<GameObject>();
    private List<int> playerInventoryRelicIndex = new List<int>();

    [SerializeField]
    private GameObject relic;
    [SerializeField]
    private GameObject relic1;
    [SerializeField]
    private GameObject relic2;

    private int timer;

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
        //GetPlayerInventory();
        GetPlayerInventoryBAD();
        GetTimer();
    }

    public void LoadData()
    {
        //SetPlayerInventory();
        SetPlayerInventoryBAD();
        SetTimer();
    }


    //--------------------------------------------------------------------------------------------------------
    // Player Inventory

    private void GetPlayerInventory()
    {
        if (GameObject.Find("Player").TryGetComponent<PlayerInventory>(out PlayerInventory PI))
        {
            foreach (GameObject item in PI.GetInventory())
            {
                //if (item.TryGetComponent<>)
                if (item.name.Contains("Necklace"))
                {
                    if (relic != null)
                        playerInventory.Add(relic);
                }
                else if (item.name.Contains("Paper"))
                {
                    if (relic1 != null)
                        playerInventory.Add(relic1);
                }
                else
                {
                    if (relic2 != null)
                        playerInventory.Add(relic2);
                }

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
                    PI.AddToInventory(item);
                }
            }
            else
                Debug.LogWarning("Haven't pull data from player inventory yet");
        }
        else
            Debug.LogWarning("Player couldn't be found in the scene");
    }

    private void GetPlayerInventoryBAD()
    {
        if (TryGetComponent<PlayerInventory>(out PlayerInventory PI))
        {
            foreach (GameObject item in PI.GetInventory())
            {
                if (item != null)
                {
                    if (item.name.Contains("Necklace"))
                    {
                        if (relic != null)
                            playerInventoryRelicIndex.Add(0);
                    }
                    else if (item.name.Contains("Paper"))
                    {
                        if (relic1 != null)
                            playerInventoryRelicIndex.Add(1);
                    }
                    else
                    {
                        if (relic2 != null)
                            playerInventoryRelicIndex.Add(2);
                    }
                }
                else
                {
                    Debug.LogWarning("Item is null");
                    Debug.Log("Within " + SceneManager.GetActiveScene().name);
                }
            }

            PI.ClearInventory();

            foreach (int num in playerInventoryRelicIndex)
            {
                Debug.Log(num);
            }
        }
        else
            Debug.LogWarning("Player couldn't be found in the scene");
    }

    private void SetPlayerInventoryBAD()
    {
        if (TryGetComponent<PlayerInventory>(out PlayerInventory PI))
        {
            if (playerInventory != null)
            {
                foreach (int num in playerInventoryRelicIndex)
                {
                    switch(num)
                    {
                        case 0:
                            if (relic != null)
                            {
                                PI.AddToInventory(relic);
                                Debug.Log("relic Set");
                            }
                            break;
                        case 1:
                            if (relic1 != null)
                            {
                                PI.AddToInventory(relic1);
                                Debug.Log("relic1 Set");
                            }
                            break;
                        case 2:
                            if (relic2 != null)
                            {

                                PI.AddToInventory(relic2);
                                Debug.Log("relic2 Set");
                            }
                            break;
                        default:
                            break;
                    }
                }

                Debug.Log("Inventory Set");
            }
            else
                Debug.LogWarning("Haven't pull data from player inventory yet");
        }
        else
            Debug.LogWarning("Player couldn't be found in the scene");
    }

    private void GetTimer()
    {
        if (GameObject.Find("Game Manager").TryGetComponent<GameManager>(out GameManager GM))
        {
            timer = GM.GetTime();
        }
    }

    private void SetTimer()
    {
        if (GameObject.Find("Game Manager").TryGetComponent<GameManager>(out GameManager GM))
        {
            GM.SetTime(timer);
        }
    }
}



