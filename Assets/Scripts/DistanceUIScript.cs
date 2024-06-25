using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DistanceUIScript : MonoBehaviour
{
    public TextMeshProUGUI checkpointTracker;
    public TextMeshProUGUI lapTracker;

    public TrackCheckpoints trackCheckpoints;

    // Start is called before the first frame update
    void Start()
    {
        checkpointTracker.text = trackCheckpoints.CheckpointCounter();
        lapTracker.text = trackCheckpoints.LapCounter();
    }

    void LateUpdate()
    {
        checkpointTracker.text = trackCheckpoints.CheckpointCounter();
        lapTracker.text = trackCheckpoints.LapCounter();
    }
}
