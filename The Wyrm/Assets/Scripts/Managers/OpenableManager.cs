using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenableManager : MonoBehaviour
{
    private GameObject Closed;
    private GameObject Open;

    // Start is called before the first frame update
    void Start()
    {
        Closed = transform.GetChild(0).gameObject;
        Open = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenControl()
    {
        if (Closed.activeSelf)
        {
            OpenUp();
        }
        else if (Open.activeSelf)
        {
            CloseDown();
        }
    }

    private void OpenUp()
    {
        Closed.SetActive(false);
        Open.SetActive(true);
    }

    private void CloseDown()
    {
        Closed.SetActive(true);
        Open.SetActive(false);
    }
}
