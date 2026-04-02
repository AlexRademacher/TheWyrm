using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLevelTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameObject.Find("Scene Manager").TryGetComponent<LoadSceneManager>(out LoadSceneManager lSM))
                lSM.SendToArena();
        }
    }
}
