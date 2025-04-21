using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInventory : MonoBehaviour
{
    private GameManager GM;
    private UIManager UI;

    [Header("Inventory")]
    [SerializeField]
    private GameObject[] inventory = new GameObject[3];
    private int inventoryIndex = 0;
    private int relicCount = 0;

    [Header("Debugger")]
    [Tooltip("Turns on inventory Debugging"), SerializeField]
    private bool inventoryDebugging;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();
        UI = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //----------------------------------------------------------------------------------------------------------------------
    // Inventory

    /// <summary>
    /// Gets item from players inventory using a specific slot
    /// </summary>
    /// <param name="index"> slot number being checked </param>
    /// <returns> GameObject from inventory. Returns null if index is out of bounds, or the slot requested is empty </returns>
    private GameObject GetFromInventory(int index)
    {
        if (index < 0)
        {
            Debug.LogWarning(index + " is less than 0");
            return null;
        }
        
        if (index >= inventory.Length)
        {
            Debug.LogWarning(index + " is greater than the current index the inventory has: " + (inventory.Length - 1));
            return null;
        }
        
        if (inventory[index] == null)
        {
            Debug.LogWarning("inventory at index: " + index + " is empty");
            return null;
        }

        return inventory[index];
    }

    /// <summary>
    /// Gets item from players inventory by searching for its name
    /// </summary>
    /// <param name="itemName"> name being search for </param>
    /// <returns> GameObject from inventory, returns null if not found </returns>
    private GameObject GetFromInventory(string itemName)
    {
        foreach (GameObject item in inventory)
        {
            if (item != null && item.name.Equals(itemName))
                return item;
        }

        if (inventoryDebugging)
            Debug.LogWarning(itemName + " was not found with in the players inventory");
        return null;
    }

    /// <summary>
    /// Gets item from players inventory by searching for it specifically
    /// </summary>
    /// <param name="itemWanted"> GameObject being search for </param>
    /// <returns> GameObject from inventory, returns null if not found </returns>
    private GameObject GetFromInventory(GameObject itemWanted)
    {
        foreach (GameObject item in inventory)
        {
            if (item != null && item == itemWanted)
                return item;
        }

        if (inventoryDebugging)
            Debug.LogWarning(itemWanted.name + " was not found with in the players inventory");
        return null;
    }

    /// <summary>
    /// Sets an item into the inventory
    /// </summary>
    /// <param name="item"> The item being set into the inventory </param>
    /// <returns> Wether the item was successfully added into the inventory or not </returns>
    private bool SetToInventory(GameObject item)
    {
        bool placedIn = false;
        int slot = 0;

        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = item;
                placedIn = true;
                slot = i;
                break;
            }
        }

        if (inventoryDebugging)
        {
            if (placedIn)
            {
                Debug.Log(item.name + " placed in player's inventory at slot: " + slot);
                //Debug.LogWarning(inventory[slot]);
            }
            else
            {
                Debug.LogWarning("Could not place item within the player's inventory");
            }
        }
        
        return placedIn;
    }

    private GameObject SetInvSlotToNull(int index)
    {
        if (index < 0)
        {
            Debug.LogWarning(index + " is less than 0");
            return null;
        }

        if (index >= inventory.Length)
        {
            Debug.LogWarning(index + " is greater than the current index the inventory has: " + (inventory.Length - 1));
            return null;
        }

        if (inventory[index] == null)
        {
            Debug.LogWarning("inventory at index: " + index + " is empty");
            return null;
        }

        GameObject oldSlot = inventory[index];
        inventory[index] = null;

        if (inventoryDebugging)
            Debug.Log("Set inventory at index: " + index + " to null");
        return oldSlot;
    }

    /// <summary>
    /// Starts adding an item to the inventory
    /// </summary>
    /// <param name="item"> The item being added to the inventory </param>
    public void AddToInventory(GameObject item)
    {
        if (item != null)
        {
            if (GetFromInventory(item) == null)
            {
                if (SetToInventory(item))
                {
                    if (GM != null)
                        GM.AddToTimer(60);

                    inventoryIndex++;

                    if (item.name.Contains("Relic"))
                    {
                        if (UI != null)
                            UI.UpdateItemCount(1);

                        relicCount++;
                    }

                    if (relicCount == 3)
                    {
                        if (GameObject.Find("Game Manager").TryGetComponent<LoadSceneManager>(out LoadSceneManager lSM))
                             lSM.SendToArena();
                    }
                }
            }
            else
            {
                Debug.LogWarning("Item was already placed into the inventory");
            }
        }
        else
        {
            Debug.LogWarning("Adding null into inventory isn't allowed");
        }
    }

    /// <summary>
    /// Starts removing an item from the inventory
    /// </summary>
    /// <returns> The GameObject that was removed </returns>
    public GameObject RemoveInventory()
    {
        GameObject item = SetInvSlotToNull(inventoryIndex - 1);

        if (item == null)
        {
            Debug.LogWarning("Inventory slot is empty");
            return null;
        }
        
        if (item.name.Contains("Relic"))
        {
            if (UI != null)
                UI.UpdateItemCount(-1);
        }

        inventoryIndex--;

        return item;
    }

    public void ListInventory()
    {
        int num = 0;
        Debug.Log("---------Inventory--------");
        foreach (GameObject item in inventory)
        {
            if (item != null)
            {
                Debug.Log("Inventory slot " + num + " holds " + item.name);
            }
            else
            {
                Debug.Log("Inventory slot " + num + " holds nothing");
            }
            num++;
        }
    }
}
