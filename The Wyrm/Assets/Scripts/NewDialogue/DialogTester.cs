using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTester : MonoBehaviour
{
    [SerializeField] Dialog dialog;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y)) 
        {
            //StartCoroutine(DialogueManager.Instance.ShowDialog(dialog));
        }
    }
}
