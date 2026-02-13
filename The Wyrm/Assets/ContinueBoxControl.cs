using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContinueBoxControl : MonoBehaviour
{
    TextMeshProUGUI text;
    

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogueManager.Instance.choiceLine || DialogueManager.Instance.branchLine1n2 || DialogueManager.Instance.branchLine3n4)
            text.text = "1 or 2 to continue";
        else
            text.text = "Press E to continue";
    }
}
