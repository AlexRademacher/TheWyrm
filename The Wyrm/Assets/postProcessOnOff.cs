using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class postProcessOnOff : MonoBehaviour
{
    [SerializeField] PostProcessVolume postProcessVolume;

    // Start is called before the first frame update
    void Start()
    {
        postProcessVolume.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void changeVignState() 
    {
        postProcessVolume.enabled = !postProcessVolume.enabled;
    }
}
