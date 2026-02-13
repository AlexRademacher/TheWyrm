using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class postProcessOnOff : MonoBehaviour
{
    [SerializeField] PostProcessVolume postProcessVolume;

    private Vignette vignette = null;

    // Start is called before the first frame update
    void Start()
    {
        postProcessVolume.profile.TryGetSettings(out vignette);
        vignette.enabled.value = false;
        //postProcessVolume.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void changeVignState() 
    {
        vignette.enabled.value = !vignette.enabled.value;
    }
}
