using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog : MonoBehaviour
{
    [SerializeField] List<string> lines;
    [SerializeField] List<string> branch1;
    [SerializeField] List<string> branch2;
    [SerializeField] List<string> branch3;
    [SerializeField] List<string> branch4;

    [SerializeField] public int skipNum;

    public List<string> Lines 
    {
        get { return lines; }
    }
    public List<string> Branch1 { get { return branch1; } }
    public List<string> Branch2 { get { return branch2; } }
    public List<string> Branch3 { get { return branch3; } }
    public List<string> Branch4 { get { return branch4; } }

}
