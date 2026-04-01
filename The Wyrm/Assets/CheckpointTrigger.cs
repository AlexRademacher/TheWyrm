using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) 
    {
        
        if (other.GetComponent<Player>() != null)
        {
            Debug.Log("Checkpoint Triggered");
            player.setNewRespawn(this.transform.position);
        }

    }
}
