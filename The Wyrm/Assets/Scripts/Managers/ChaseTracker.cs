using System.Collections.Generic;
using UnityEngine;

public class ChaseTracker : MonoBehaviour
{
    public static ChaseTracker Instance;

    private HashSet<WyrmManager> activeChasers = new HashSet<WyrmManager>();

    private void Awake()
    {
        Instance = this;
    }

    public void StartChasing(WyrmManager wyrm)
    {
        activeChasers.Add(wyrm);
        UpdateMusic();
    }

    public void StopChasing(WyrmManager wyrm)
    {
        activeChasers.Remove(wyrm);
        UpdateMusic();
    }

    private void UpdateMusic()
    {
        bool isAnyChasing = activeChasers.Count > 0;

        if (MusicManager.Instance != null)
            MusicManager.Instance.SetChaseState(isAnyChasing);
    }
}