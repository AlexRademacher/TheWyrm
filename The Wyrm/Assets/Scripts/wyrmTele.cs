using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wyrmTele : MonoBehaviour
{
    int number;
    [SerializeField] GameObject wyrm;
    Transform teleSpot;
    // Start is called before the first frame update
    void Start()
    {
        number = Random.Range(1, 6);
        Debug.Log(number);
        //wyrm = GameObject.Find("wyrm");
        teleSpot = GameObject.Find("teleSpot").GetComponent<Transform>();
        if (number >= 4 ) //Change the comparison and the number to affect chance of spawning
        {
            Instantiate(wyrm, teleSpot);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha9))
            Instantiate(wyrm, teleSpot);
    }
}
