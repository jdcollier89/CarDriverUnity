using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Car settings")]
    public float accelerationFactor = 15.0f;
    public float turnFactor = 7.5f;
    public float maxSpeed = 5f;
    // Local Variables
    float accelerationInput = 0;
    
    float steeringInput = 0;
    float rotationAngle = 0;
    float velocityVsUp = 0f;

    // Components
    public Rigidbody2D carRigidBody2D;


    void FixedUpdate(){
        ApplyEngineForce();
        ApplySteering();
    }

    void ApplyEngineForce(){
        velocityVsUp = Vector2.Dot(transform.up, carRigidBody2D.velocity);
        if (velocityVsUp > maxSpeed && accelerationInput > 0){
            return;
        }
        if (velocityVsUp < -maxSpeed * 0.5f && accelerationInput < 0){
            return;
        }
        // Create a force for the engine
        Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;
        carRigidBody2D.AddForce(engineForceVector, ForceMode2D.Force);
    }

    void ApplySteering(){

        float minSpeedBeforeAllowTurningFactor = (carRigidBody2D.velocity.magnitude / 8);
        minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);

        // Update the rotation angle based on input
        rotationAngle -= steeringInput * turnFactor * minSpeedBeforeAllowTurningFactor;
        carRigidBody2D.MoveRotation(rotationAngle);
    }


    float GetLateralVelocity(){
        return Vector2.Dot(transform.right, carRigidBody2D.velocity);
    }

    public bool IsTireScreeching(out float lateralVelocity, out bool isBraking){
        lateralVelocity = GetLateralVelocity();
        isBraking = false;

        // Check if we are moving forwards and hitting the brakes
        if (accelerationInput < 0 && velocityVsUp > 0){
            isBraking = true;
            return true;
        }

        // Check if we are moving backwards and hitting the brakes
        if (accelerationInput > 0 && velocityVsUp < 0){
            isBraking = true;
            return true;
        }

        // Check for skidding
        if (Mathf.Abs(GetLateralVelocity()) > 0.5f){
            return true;
        }

        return false;
    }

    public void SetInputVector(Vector2 inputVector){
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }

    public float GetVelocityMagnitude()
    {
        return carRigidBody2D.velocity.magnitude;
    } 
}
