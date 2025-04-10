using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Item : MonoBehaviour
{
    private UIManager UI;

    private bool pickUp = false;

    // Start is called before the first frame update
    void Start()
    {
        UI = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pickUp)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Can pick up " + gameObject.name + " now");
                ItemInteraction();
            }
        }
    }

    public void SetPickUpState(bool newState)
    {
        pickUp = newState;
    }

    public void FirstPersonInteraction()
    {
        ItemInteraction();
    }

    private void ItemInteraction()
    {
        if (gameObject.name.Contains("Relic"))
        {
            GameObject.Find("Player").transform.GetComponent<Player>().AddToInventory(gameObject);
            UI.UpdateItemCount(1);
            transform.position = new Vector3(transform.position.x, transform.position.y - 30, transform.position.z);
        }
    }
}
