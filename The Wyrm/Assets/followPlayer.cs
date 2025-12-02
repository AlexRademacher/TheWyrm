using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{

    [SerializeField] GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position =  new Vector3(player.transform.position.x + 1090.01f, this.transform.position.y, player.transform.position.z + 2186.8199f);
    }
}
