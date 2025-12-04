using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeUI : MonoBehaviour
{
    private Image image;
    private Material material;

    bool hiding = true;
    //bool showing = false;
    Color currentColor;

    [Tooltip("Fades in the image when active"), SerializeField]
    private bool StartFadeIn;
    [Tooltip("Fades in the image when active"), SerializeField]
    private bool StartTempFadeIn;


    private void OnEnable()
    {
        if (StartFadeIn)
        {
            if (TryGetComponent<Image>(out image))
            {
                image.color = new (image.color.r, image.color.b, image.color.g, 0);
                FadeIn(image.color);
            }
            else if (TryGetComponent<Material>(out material))
            {
                material.color = new(image.color.r, image.color.b, image.color.g, 0);
                FadeIn(material.color);
            }
        }

        if (StartTempFadeIn)
        {
            currentColor = GetComponent<Image>().color;
            FadeInTemporary(currentColor, 1);
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
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

    public bool FadeIn(Color color)
    {
        return FadeInImage(color);
    }

    public bool FadeInTemporary(Color color, float seconds)
    {
        try
        {
            StartCoroutine(TempFadeInImage(color, seconds));

            return true;
        }
        catch (MissingReferenceException)
        {
            return false;
        }
    }

    public bool FadeOut(Color color)
    {
        return FadeOutImage(color);
    }

    public bool FadeInImage(Color currentAlpha)
    {
        if (currentAlpha != null)
        {
            while (currentAlpha.a < 1)
            {
                currentAlpha = new Color(currentAlpha.r, currentAlpha.g, currentAlpha.b, currentAlpha.a + 1 / 255f); //decreases alpha over time
                Debug.Log(currentAlpha.a);

                if (image != null)
                    image.color = currentAlpha;
                else if (material != null)
                    material.color = currentAlpha;
            }
                
            return true;
        }
        else
            Debug.LogError("Color was null, could not Fade in");

        return false;
    }

    public bool FadeOutImage(Color currentAlpha)
    {
        if (currentAlpha != null)
        {
            while (currentAlpha.a > 0)
            {
                currentAlpha = new Color(currentAlpha.r, currentAlpha.g, currentAlpha.b, currentAlpha.a - 1 / 255f); //decreases alpha over time
                Debug.Log(currentAlpha.a);

                if (image != null)
                    image.color = currentAlpha;
                else if (material != null)
                    material.color = currentAlpha;
            }
                
            return true;
        }
        else
            Debug.LogError("Color was null, could not Fade out");

        return false;
    }

    public IEnumerator TempFadeInImage(Color currentAlpha, float seconds)
    {
        FadeIn(currentAlpha);

        yield return new WaitForSeconds(seconds);

        FadeOut(currentAlpha);
    }
}
