using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelParticleHandler : MonoBehaviour
{
    float particleEmissionRate = 0;

    CarController carController;
    ParticleSystem particleSmokeSystem;
    ParticleSystem.EmissionModule particleSystemEmissionModule;

    void Awake(){
        carController = GetComponentInParent<CarController>();
        particleSmokeSystem = GetComponent<ParticleSystem>();
        particleSystemEmissionModule = particleSmokeSystem.emission;
        particleSystemEmissionModule.rateOverTime = 0;
    }


    // Update is called once per frame
    void Update()
    {
        // Reduce emission rate over time (if any particles already being produced)
        particleEmissionRate = Mathf.Lerp(particleEmissionRate, 0, Time.deltaTime * 5);

        // Begin smoke trail if tired skidding
        if (carController.IsTireScreeching(out float lateralVelocity, out bool isBraking))
        {
            if (isBraking){
                particleEmissionRate = 20;
            }
            else {
                particleEmissionRate = Mathf.Abs(lateralVelocity) * 10;
            }
        }

        // Update the rate over
        particleSystemEmissionModule.rateOverTime = particleEmissionRate;
    }
}
