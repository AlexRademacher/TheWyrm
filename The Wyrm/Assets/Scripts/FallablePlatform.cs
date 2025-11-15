using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallablePlatform : MonoBehaviour
{
    private GameObject standing;
    private GameObject fallen;

    private bool canDrop;

    // Start is called before the first frame update
    void Start()
    {
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
        if (transform.GetChild(0).gameObject.activeSelf && canDrop)
        {
            canDrop = false;

            FallOver();

            yield return new WaitForSeconds(10);

            StandUp();
        }

        yield return new WaitForSeconds(.1f);

        canDrop = true;
    }

    private void FallOver()
    {
        standing.SetActive(false);
        fallen.SetActive(true);
    }

    private void StandUp()
    {
        standing.SetActive(true);
        fallen.SetActive(false);
    }
}
