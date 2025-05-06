using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnWyrm : MonoBehaviour
{
    int number;
    [SerializeField] GameObject wyrm;
    bool spawned = false;
    GameObject newWyrm;
    [SerializeField] int wyrmNumber;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9)) //Debug tool to spawn wyrms (Remove for final build)
            StartCoroutine(SpawnTimer());
    }

    public GameObject Spawn()
    {
        number = Random.Range(1, 7); //range in inclusive excluse so this is 1-6 not 1-7 (i think)
        //Debug.Log(number);
        if (number >= 4 && !spawned) //Change the comparison and the number to affect chance of spawning
        {
            StartCoroutine(SpawnTimer());
            spawned = true;
            return newWyrm;
        }

        return null;
    }

    IEnumerator SpawnTimer() 
    {
        yield return new WaitForSeconds(2);
        newWyrm = Instantiate(wyrm, this.transform);
        NavControl wyrmSpawned = GameObject.Find(newWyrm.name).GetComponent<NavControl>();
        wyrmSpawned.giveNumber(1);
    }
}
