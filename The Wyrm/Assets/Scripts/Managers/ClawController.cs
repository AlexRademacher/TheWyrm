using System.Collections;
using System.Collections.Generic;
using UnityEngine.Splines;
using UnityEngine;

public class ClawController : MonoBehaviour
{
    private SplineAnimate SAnimator;

    // Start is called before the first frame update
    void Start()
    {
        SAnimator = transform.GetComponent<SplineAnimate>();
    }

    private void OnEnable()
    {
        if (SAnimator != null)
            SAnimator.ElapsedTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (SAnimator != null && SAnimator.isActiveAndEnabled && SAnimator.IsPlaying)
        {
            SAnimator.Completed += Completed;
        }
    }

    private void Completed()
    {
        if(SAnimator.ElapsedTime >= (SAnimator.Duration * 2))
        {
            gameObject.SetActive(false);
        }
    }
}
