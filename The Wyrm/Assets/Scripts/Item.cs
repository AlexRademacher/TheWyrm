using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private UIManager UI;

    // Start is called before the first frame update
    void Start()
    {
        UI = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickedUp()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - 30, transform.position.z);
    }
}
