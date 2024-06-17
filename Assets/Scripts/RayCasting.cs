using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RayCasting : MonoBehaviour
{
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

        float distance_N = ray.distance;
        float distance_NE = rayNE.distance;
        float distance_NW = rayNW.distance;
        float distance_E = rayE.distance;
        float distance_W = rayW.distance;
        float distance_S = rayS.distance;

        // Draw Sensors for Debug purposes
        Debug.DrawRay(transform.position, Direction*distance_N, Color.black);
        Debug.DrawRay(transform.position, axisRotation_NE*distance_NE, Color.red);
        Debug.DrawRay(transform.position, axisRotation_NW*distance_NW, Color.red);
        Debug.DrawRay(transform.position, axisRotation_E*distance_E, Color.green);
        Debug.DrawRay(transform.position, axisRotation_W*distance_W, Color.green);
        Debug.DrawRay(transform.position, -Direction*distance_S, Color.black);

    }
}
