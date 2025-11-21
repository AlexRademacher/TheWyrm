using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [Tooltip("Where each part of the tutorial is stored"), SerializeField]
    private GameObject[] Tutorials;

    private int StageNum;

    private bool Looking = false;
    private bool Moving = false;
    private bool Running = false;
    private bool Talking = false;
    private bool Grabing = false;
    private bool Searching = false;
    private bool Placing = false;

    [Tooltip("How long you stay on the current tutorial after completing it to move on to the next one"), SerializeField]
    private int WaitTime;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject tutorial in Tutorials)
        {
            tutorial.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        TutorialController();
    }

    private void TutorialController()
    {
        switch (StageNum)
        {
            case 0:
                if (!Looking)
                    StartTutorial();
                else
                    EndTutorial();
                break;
            case 1:
                if (!Moving)
                    StartTutorial();
                else
                    EndTutorial();
                break;
            case 2:
                if (!Running)
                    StartTutorial();
                else
                    EndTutorial();
                break;
            default:
                break;
        }
    }

    private void StartTutorial()
    {
        Tutorials[StageNum].SetActive(true);
    }

    private void EndTutorial()
    {
        Tutorials[StageNum].SetActive(false);
        StageNum++;
    }

    public bool HasLooked()
    {
        return Looking;
    }

    public bool HasMoved()
    {
        return Moving;
    }

    public bool HasRan()
    {
        return Running;
    }

    public IEnumerator IsLooking()
    {
        yield return new WaitForSeconds(WaitTime);

        if (!Looking)
            Looking = true;
    }

    public IEnumerator IsMoving()
    {
        yield return new WaitForSeconds(WaitTime);

        if (!Moving)
            Moving = true;
    }

    public IEnumerator IsRunning()
    {
        yield return new WaitForSeconds(WaitTime);

        if (!Running)
            Running = true;
    }
}
