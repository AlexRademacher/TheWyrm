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
            case 3:
                if (!Talking)
                    StartTutorial();
                else
                    EndTutorial();
                break;
            case 4:
                if (!Grabing)
                    StartTutorial();
                else
                    EndTutorial();
                break;
            case 5:
                if (!Searching)
                    StartTutorial();
                else
                    EndTutorial();
                break;
            case 6:
                if (!Placing)
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
        if (Tutorials != null && Tutorials.Length >= StageNum && Tutorials[StageNum] != null)
            Tutorials[StageNum].SetActive(true);
    }

    private void EndTutorial()
    {
        if (Tutorials != null && Tutorials.Length >= StageNum && Tutorials[StageNum] != null && Tutorials[StageNum].activeSelf)
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

    public bool HasTalked()
    {
        return Talking;
    }

    public bool HasGrabbed()
    {
        return Grabing;
    }

    public bool HasSearched()
    {
        return Searching;
    }

    public bool HasPlaced()
    {
        return Placing;
    }

    public IEnumerator IsLooking()
    {
        yield return new WaitForSeconds(3);

        if (!Looking)
            Looking = true;
    }

    public IEnumerator IsMoving()
    {
        yield return new WaitForSeconds(3);

        if (!Moving)
            Moving = true;
    }

    public IEnumerator IsRunning()
    {
        yield return new WaitForSeconds(2);

        if (!Running)
            Running = true;
    }
    public IEnumerator IsTalking()
    {
        yield return new WaitForSeconds(0);

        if (!Talking)
            Talking = true;
    }

    public IEnumerator IsGrabbing()
    {
        yield return new WaitForSeconds(0);

        if (!Grabing)
            Grabing = true;
    }

    public IEnumerator IsSearching()
    {
        yield return new WaitForSeconds(0);

        if (!Searching)
            Searching = true;
    }

    public IEnumerator IsPlacing()
    {
        yield return new WaitForSeconds(0);

        if (!Placing)
            Placing = true;
    }

}