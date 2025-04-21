using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class spawnWyrm : MonoBehaviour
{
    int number;
    [SerializeField] GameObject wyrm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9)) //Debug tool to spawn wyrms (Remove for final build)
            Instantiate(wyrm, this.transform);
    }

    public void spawn()
    {
        number = Random.Range(1, 7); //range in inclusive excluse so this is 1-6 not 1-7 (i think)
        Debug.Log(number);
        if (number >= 4) //Change the comparison and the number to affect chance of spawning
        {
            Instantiate(wyrm, this.transform);
        }
    }
}
