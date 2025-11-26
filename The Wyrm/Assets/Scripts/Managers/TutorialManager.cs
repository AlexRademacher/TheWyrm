using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private bool Hiding = false;
    private bool Dropping = false;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject tutorial in Tutorials)
        {
            tutorial.SetActive(false);
        }

        if (SceneManager.GetActiveScene().buildIndex == 1)
            StageNum = 8;
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
                if (!Hiding)
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
                if (!Grabing)
                    StartTutorial();
                else
                    EndTutorial();
                break;
                
            case 7:
                if (!Dropping)
                    StartTutorial();
                else
                    EndTutorial();
                break;
            case 8:
                if (!Placing)
                    StartTutorial();
                else
                    EndTutorial();
                break;
            default:
                break;
        }
    }

    public int GetPlaceInTutorial()
    {
        return StageNum;
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

        if (StageNum == 8)
            Hiding = false;
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

    public bool HasHidden()
    {
        return Hiding;
    }

    public bool HasDropped()
    {
        return Dropping;
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

    public IEnumerator IsHiding()
    {
        yield return new WaitForSeconds(0);

        if (!Hiding)
            Hiding = true;
    }

    public IEnumerator IsDropping()
    {
        yield return new WaitForSeconds(0);

        if (!Dropping)
            Dropping = true;
    }

}