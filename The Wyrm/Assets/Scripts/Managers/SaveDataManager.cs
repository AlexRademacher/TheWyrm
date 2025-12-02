
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveDataManager : MonoBehaviour
{
    private List<GameObject> playerInventory = new List<GameObject>();

    [SerializeField]
    private List<int> playerInventoryRelicIndex = new List<int>();

    [SerializeField]
    private GameObject relic;
    [SerializeField]
    private GameObject relic1;
    [SerializeField]
    private GameObject relic2;
    [SerializeField]
    private GameObject relic3;
    [SerializeField]
    private GameObject relic4;
    [SerializeField]
    private GameObject relic5;
    [SerializeField]
    private GameObject relic6;
    [SerializeField]
    private GameObject relic7;
    [SerializeField]
    private GameObject relic8;

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

    public void ClearData()
    {
        ClearDataPlayerInventory();
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
                    else
                        Debug.LogError("Relic not given to SaveDataManager");
                }
                else if (item.name.Contains("Paper"))
                {
                    if (relic1 != null)
                        playerInventory.Add(relic1);
                    else
                        Debug.LogError("Relic not given to SaveDataManager");
                }
                else if (item.name.Contains("Bag"))
                {
                    if (relic2 != null)
                        playerInventory.Add(relic2);
                    else
                        Debug.LogError("Relic not given to SaveDataManager");
                }
                else if (item.name.Contains("Feather"))
                {
                    if (relic3 != null)
                        playerInventory.Add(relic3);
                    else
                        Debug.LogError("Relic not given to SaveDataManager");
                }
                else if (item.name.Contains("Scaler"))
                {
                    if (relic4 != null)
                        playerInventory.Add(relic4);
                    else
                        Debug.LogError("Relic not given to SaveDataManager");
                }
                else if (item.name.Contains("Net"))
                {
                    if (relic5 != null)
                        playerInventory.Add(relic5);
                    else
                        Debug.LogError("Relic not given to SaveDataManager");
                }
                else if (item.name.Contains("Scythe"))
                {
                    if (relic6 != null)
                        playerInventory.Add(relic6);
                    else
                        Debug.LogError("Relic not given to SaveDataManager");
                }
                else if (item.name.Contains("Hat"))
                {
                    if (relic7 != null)
                        playerInventory.Add(relic7);
                    else
                        Debug.LogError("Relic not given to SaveDataManager");
                }
                else if (item.name.Contains("Doll"))
                {
                    if (relic8 != null)
                        playerInventory.Add(relic8);
                    else
                        Debug.LogError("Relic not given to SaveDataManager");
                }
                else
                {
                    Debug.LogError("Relic collected has an incorrect name or was not set correctly within the code");
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
                    //Debug.Log("Relic saved: " + item.name);

                    if (item.name.Contains("Necklace"))
                    {
                        if (relic != null)
                            playerInventoryRelicIndex.Add(0);
                        else
                            Debug.LogError("Relic not given to SaveDataManager");
                    }
                    else if (item.name.Contains("Paper"))
                    {
                        if (relic1 != null)
                            playerInventoryRelicIndex.Add(1);
                        else
                            Debug.LogError("Relic not given to SaveDataManager");
                    }
                    else if (item.name.Contains("Bag"))
                    {
                        if (relic2 != null)
                            playerInventoryRelicIndex.Add(2);
                        else
                            Debug.LogError("Relic not given to SaveDataManager");
                    }
                    else if (item.name.Contains("Feather"))
                    {
                        if (relic3 != null)
                            playerInventoryRelicIndex.Add(3);
                        else
                            Debug.LogError("Relic not given to SaveDataManager");
                    }
                    else if (item.name.Contains("Scaler"))
                    {
                        if (relic4 != null)
                            playerInventoryRelicIndex.Add(4);
                        else
                            Debug.LogError("Relic not given to SaveDataManager");
                    }
                    else if (item.name.Contains("Net"))
                    {
                        if (relic5 != null)
                            playerInventoryRelicIndex.Add(5);
                        else
                            Debug.LogError("Relic not given to SaveDataManager");
                    }
                    else if (item.name.Contains("Scythe"))
                    {
                        if (relic6 != null)
                            playerInventoryRelicIndex.Add(6);
                        else
                            Debug.LogError("Relic not given to SaveDataManager");
                    }
                    else if (item.name.Contains("Hat"))
                    {
                        if (relic7 != null)
                            playerInventoryRelicIndex.Add(7);
                        else
                            Debug.LogError("Relic not given to SaveDataManager");
                    }
                    else if (item.name.Contains("Doll"))
                    {
                        if (relic8 != null)
                            playerInventoryRelicIndex.Add(8);
                        else
                            Debug.LogError("Relic not given to SaveDataManager");
                    }
                    else
                    {
                        Debug.LogError("Relic collected has an incorrect name or was not set correctly within the code");
                    }
                }
                else
                {
                    Debug.LogWarning("Item is null");
                    Debug.Log("Within " + SceneManager.GetActiveScene().name);
                }
            }

            PI.ClearInventory();

            /*foreach (int num in playerInventoryRelicIndex)
            {
                Debug.Log(num);
            }*/
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
                PI.ClearInventory();

                if (GameObject.Find("Canvas").TryGetComponent<UIManager>(out UIManager UI))
                    UI.UpdateItemCount(3);

                foreach (int num in playerInventoryRelicIndex)
                {
                    //Debug.Log("Relic number loaded: " + num);

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
                        case 3:
                            if (relic3 != null)
                            {
                                PI.AddToInventory(relic3);
                                Debug.Log("relic3 Set");
                            }
                            break;
                        case 4:
                            if (relic4 != null)
                            {
                                PI.AddToInventory(relic4);
                                Debug.Log("relic4 Set");
                            }
                            break;
                        case 5:
                            if (relic5 != null)
                            {
                                PI.AddToInventory(relic5);
                                Debug.Log("relic5 Set");
                            }
                            break;
                        case 6:
                            if (relic6 != null)
                            {
                                PI.AddToInventory(relic6);
                                Debug.Log("relic6 Set");
                            }
                            break;
                        case 7:
                            if (relic7 != null)
                            {
                                PI.AddToInventory(relic7);
                                Debug.Log("relic7 Set");
                            }
                            break;
                        case 8:
                            if (relic8 != null)
                            {
                                PI.AddToInventory(relic8);
                                Debug.Log("relic8 Set");
                            }
                            break;
                        default:
                            break;
                    }
                }

                //Debug.Log("Inventory Set");
            }
            else
                Debug.LogWarning("Haven't pull data from player inventory yet");
        }
        else
            Debug.LogWarning("Player couldn't be found in the scene");
    }

    private void ClearDataPlayerInventory()
    {
        playerInventory.Clear();
        playerInventoryRelicIndex.Clear();
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



