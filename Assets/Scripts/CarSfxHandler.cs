using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSfxHandler : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource tireScreechingAudioSource;
    public AudioSource engineAudioSource;
    public AudioSource carHitAudioSource;

    // Local Variable
    float desiredEnginePitch = 0.5f;
    float tireScreechPitch = 1f;

    // Components
    CarController carController;

    void Awake(){
        carController = GetComponentInParent<CarController>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEngineSFX();
        UpdateTireScreechingSFX();
    }

    void UpdateEngineSFX(){
        // Handle engine SFX
        float velocityMagnitude = carController.GetVelocityMagnitude();

        // Increase engine volume as car goes faster
        float desiredEngineVolume = velocityMagnitude * 0.05f;
        desiredEngineVolume = 0.5f  * Mathf.Clamp(desiredEngineVolume, 0.2f, 1.0f);

        engineAudioSource.volume = Mathf.Lerp(engineAudioSource.volume, desiredEngineVolume, Time.deltaTime * 1);

        // Raise pitch with engine speed
        desiredEnginePitch = velocityMagnitude * 1f;
        desiredEnginePitch = Mathf.Clamp(desiredEnginePitch, 0.5f, 2f);
        engineAudioSource.pitch = Mathf.Lerp(engineAudioSource.pitch, desiredEnginePitch, Time.deltaTime * 10f);
    }

    void UpdateTireScreechingSFX(){
        if (carController.IsTireScreeching(out float lateralVelocity, out bool isBraking))
        {
            if (isBraking){
                // Player braking
                tireScreechingAudioSource.volume = Mathf.Lerp(tireScreechingAudioSource.volume, 1.0f, Time.deltaTime * 10);
                //tireScreechPitch = Mathf.Lerp(tireScreechPitch, 0.5f, Time.deltaTime * 10);
                tireScreechingAudioSource.pitch = Mathf.Lerp(tireScreechPitch, 0.5f, Time.deltaTime * 1);
            }
            else {
                // Player drifting
                tireScreechingAudioSource.volume = Mathf.Abs(lateralVelocity) * 0.5f;
                //tireScreechPitch = Mathf.Abs(lateralVelocity) * 0.1f;
                //tireScreechingAudioSource.pitch = Mathf.Abs(lateralVelocity) * 1.2f;
                tireScreechingAudioSource.pitch = Mathf.Lerp(Mathf.Abs(lateralVelocity) * 1.2f, 0.5f, Time.deltaTime * 3);
            }
        }
        else {
            // Fade over time if no screeching
            tireScreechingAudioSource.volume = Mathf.Lerp(tireScreechingAudioSource.volume, 0, Time.deltaTime * 10);
            tireScreechingAudioSource.pitch = Mathf.Lerp(tireScreechPitch, 1f, Time.deltaTime * 1f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision2D){
        float relativeVelocity = collision2D.relativeVelocity.magnitude;

        float volume = relativeVelocity * 0.1f;

        carHitAudioSource.pitch = Random.Range(0.95f, 1.05f);
        carHitAudioSource.volume = volume;

        if (!carHitAudioSource.isPlaying){
            carHitAudioSource.Play();
        }
    }
}
