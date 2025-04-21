using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private UIManager UI;

    private bool pickUp = false;

    // Start is called before the first frame update
    void Start()
    {
        UI = GameObject.Find("Canvas").GetComponent<UIManager>();

        SetPickUpState(false);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (pickUp)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Can pick up " + gameObject.name + " now");
                ItemInteraction();
            }
        }*/
    }

    public void SetPickUpState(bool newState)
    {
        pickUp = newState;
    }

    public void PickedUp()
    {
        Destroy(gameObject);
    }

    /*public void FirstPersonInteraction()
    {
        ItemInteraction();
    }

    private void ItemInteraction()
    {
        Player p = GameObject.Find("Player").transform.GetComponent<Player>();
        GameObject newSelf = gameObject;


        if (p != null)
            p.AddToInventory(newSelf);
        else
            Debug.Log("YPlayer not seen for item interaction");

        transform.position = new Vector3(transform.position.x, transform.position.y - 30, transform.position.z);
    }*/
}
