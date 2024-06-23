using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RayCasting : MonoBehaviour
{
    private float distance_N, distance_NE, distance_NW, distance_E;
    private float distance_W, distance_S;

    public SpriteRenderer track;
    private float normalizer;

    private void Start(){
        // Normalize sensors by the height of the track
        normalizer = track.size.y;
    }

    private void FixedUpdate(){
        Vector2 Direction = transform.up;

        //Rotate around the z-axis (e.g. angle perpendicular to our POV)
        Vector3 axis = new Vector3(0,0,1);
        Vector2 axisRotation_NE = Quaternion.AngleAxis(-30f, axis) * Direction;
        Vector2 axisRotation_NW = Quaternion.AngleAxis(30f, axis) * Direction;
        Vector2 axisRotation_E = Quaternion.AngleAxis(-90f, axis) * Direction;
        Vector2 axisRotation_W = Quaternion.AngleAxis(90f, axis) * Direction;

        // Create directional LiDars
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Direction);
        RaycastHit2D rayNE = Physics2D.Raycast(transform.position, axisRotation_NE);
        RaycastHit2D rayNW = Physics2D.Raycast(transform.position, axisRotation_NW);
        RaycastHit2D rayE = Physics2D.Raycast(transform.position, axisRotation_E);
        RaycastHit2D rayW = Physics2D.Raycast(transform.position, axisRotation_W);
        RaycastHit2D rayS = Physics2D.Raycast(transform.position, -Direction);

        distance_N = ray.distance;
        distance_NE = rayNE.distance;
        distance_NW = rayNW.distance;
        distance_E = rayE.distance;
        distance_W = rayW.distance;
        distance_S = rayS.distance;

        // Draw Sensors for Debug purposes
        Debug.DrawRay(transform.position, Direction*distance_N, Color.black);
        Debug.DrawRay(transform.position, axisRotation_NE*distance_NE, Color.red);
        Debug.DrawRay(transform.position, axisRotation_NW*distance_NW, Color.red);
        Debug.DrawRay(transform.position, axisRotation_E*distance_E, Color.green);
        Debug.DrawRay(transform.position, axisRotation_W*distance_W, Color.green);
        Debug.DrawRay(transform.position, -Direction*distance_S, Color.black);

    }

    public float N_sensor(){
        return distance_N/normalizer;
    }

    public float NE_sensor(){
        return distance_NE/normalizer;
    }

    public float NW_sensor(){
        return distance_NW/normalizer;
    }

        public float E_sensor(){
        return distance_E/normalizer;
    }

        public float W_sensor(){
        return distance_W/normalizer;
    }

        public float S_sensor(){
        return distance_S/normalizer;
    }
}
