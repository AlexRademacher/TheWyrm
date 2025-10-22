using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog : MonoBehaviour
{
    [SerializeField] List<string> lines;

    [SerializeField] public int skipNum;

    public List<string> Lines 
    {
        get { return lines; }
    }
}
