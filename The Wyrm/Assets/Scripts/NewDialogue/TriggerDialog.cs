using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    //Only trigger once
    Dialog thisDialog;
    bool triggered;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered)
        {
            triggered = true;
            thisDialog = GetComponent<Dialog>();
            other.gameObject.transform.LookAt(this.transform.position);
            StartCoroutine(DialogueManager.Instance.ShowDialog(thisDialog, this.gameObject));
        }
    }
}
