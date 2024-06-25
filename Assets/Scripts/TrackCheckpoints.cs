using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class TrackCheckpoints : MonoBehaviour
{
    private List<CheckpointSingle> checkpointSingleList;
    private int nextCheckpointIndex;
    private int totalCheckpoints;
    private int lapCount;

    private void Awake(){
        Transform checkpointsTransform = transform.Find("CheckPoints");

        checkpointSingleList = new List<CheckpointSingle>();

        foreach (Transform checkpointSingleTransform in checkpointsTransform){
            CheckpointSingle checkpointSingle = checkpointSingleTransform.GetComponent<CheckpointSingle>();
            checkpointSingle.SetTrackCheckpoints(this);
            checkpointSingleList.Add(checkpointSingle);
        }

        nextCheckpointIndex = 0;
        totalCheckpoints = checkpointSingleList.Count;
        lapCount = 0;
    }

    public void PlayerThroughCheckpoint(CheckpointSingle checkpointSingle){
        if(checkpointSingleList.IndexOf(checkpointSingle) == nextCheckpointIndex){
            // Correct checkpoint
            nextCheckpointIndex += 1;
            if (nextCheckpointIndex >= totalCheckpoints){
                nextCheckpointIndex %= totalCheckpoints;
                lapCount += 1;
            }
        }
        // Else "Wrong Way" message
    }

    public string LapCounter(){
        return lapCount.ToString();
    }

    public string CheckpointCounter(){
        string output = nextCheckpointIndex.ToString() + "/" + totalCheckpoints.ToString();
        return output;
    }
}
