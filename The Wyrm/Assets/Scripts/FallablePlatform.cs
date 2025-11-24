using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallablePlatform : MonoBehaviour
{
    private TutorialManager TM;

    private GameObject standing;
    private GameObject fallen;

    private bool canDrop;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
            TM = GameObject.Find("Tutorial").GetComponent<TutorialManager>();

        standing = transform.GetChild(0).gameObject;
        fallen = transform.GetChild(1).gameObject;

        canDrop = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FallableControl()
    {
        StartCoroutine(MovementControl());
    }

    private IEnumerator MovementControl()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (TM.GetPlaceInTutorial() >= 7)
                StartCoroutine(DroppingControl());
        }
        else
            StartCoroutine(DroppingControl());

        yield return new WaitForSeconds(.1f);

         if (SceneManager.GetActiveScene().buildIndex != 0)
            canDrop = true;
    }

    private IEnumerator DroppingControl()
    {
        if (transform.GetChild(0).gameObject.activeSelf && canDrop)
        {
            canDrop = false;

            FallOver();

            if (TM != null && !TM.HasDropped())
            {
                StartCoroutine(TM.IsDropping());
            }
            else if (TM == null && SceneManager.GetActiveScene().buildIndex == 0)
                Debug.LogError("Tutorial for Dropping couldn't be found");

            yield return new WaitForSeconds(10);

            if (SceneManager.GetActiveScene().buildIndex != 0)
                StandUp();
        }
    }

    private void FallOver()
    {
        standing.SetActive(false);
        fallen.SetActive(true);

        if (fallen.transform.childCount > 0)
        {
            fallen.transform.GetChild(0).transform.Rotate(new Vector3(1, 0, 0), 180);
        }
    }

    private void StandUp()
    {
        standing.SetActive(true);
        fallen.SetActive(false);

        if (fallen.transform.childCount > 0)
        {
            fallen.transform.GetChild(0).transform.Rotate(new Vector3(1, 0, 0), 180);
        }
    }
}
