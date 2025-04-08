using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnJumpPoint : MonoBehaviour
{

    [SerializeField] GameObject linkObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(linkObject, new Vector3(-20, -1, 25), Quaternion.identity);
        }
    }
}
