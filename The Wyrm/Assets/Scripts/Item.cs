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
        
    }

    public void SetPickUpState(bool newState)
    {
        pickUp = newState;
    }

    public void PickedUp()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - 30, transform.position.z);
    }
}
