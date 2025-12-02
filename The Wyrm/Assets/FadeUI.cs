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
        /*Debug.Log(currentColor.a);
        if (currentColor.a >= 10/255f && hiding == true) //if alpha is greater than ten decrease alpha
            currentColor = new Color(currentColor.r, currentColor.g, currentColor.b, currentColor.a - 10 / 255f); //decreases alpha over time
        else if (currentColor.a == 1f) // if alpha is at max allow alpha to go down
            hiding = true;
        else
        {
            hiding = false;
            currentColor = new Color(currentColor.r, currentColor.g, currentColor.b, currentColor.a + 10 / 255f); //increases alpha over time
        }
        GetComponent<Image>().color = currentColor;*/
    }

    public void FadeOut(Color currentAlpha) //Call every frame to fade UI Out
    {
        currentAlpha = new Color(currentAlpha.r, currentAlpha.g, currentAlpha.b, currentAlpha.a - 10 / 255f); //decreases alpha over time
    }

    public void FadeIn(Color currentAlpha) //Call every frame to fade UI In
    {
        currentAlpha = new Color(currentAlpha.r, currentAlpha.g, currentAlpha.b, currentAlpha.a + 10 / 255f); //decreases alpha over time
    }

}
