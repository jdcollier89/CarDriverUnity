using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSingle : MonoBehaviour
{
    private TrackCheckpoints trackCheckpoints;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.TryGetComponent<CarController>(out CarController player)){
            trackCheckpoints.PlayerThroughCheckpoint(this);
        }
    }

    public void SetTrackCheckpoints(TrackCheckpoints trackCheckpoints){
        this.trackCheckpoints = trackCheckpoints;
    }
}
