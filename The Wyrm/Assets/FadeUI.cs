using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeUI : MonoBehaviour
{
    bool hiding = true;
    //bool showing = false;
    Color currentColor;
    // Start is called before the first frame update
    void Start()
    {
        currentColor = GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentColor.a);
        if (currentColor.a >= 10/255f && hiding == true) //if alpha is greater than ten decrease alpha
            currentColor = new Color (currentColor.r, currentColor.g, currentColor.b, currentColor.a - 10/255f);
        else if (currentColor.a == 1f) // if alpha is at max allow alpha to go down
            hiding = true;
        else
        {
            hiding = false;
            currentColor = new Color(currentColor.r, currentColor.g, currentColor.b, currentColor.a + 10 / 255f);
        }
        GetComponent<Image>().color = currentColor;
    }
}
