using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SensorUI : MonoBehaviour
{
    public RayCasting sensors;
    public SpriteRenderer track;
    public TextMeshProUGUI NSensor;
    public TextMeshProUGUI NESensor;
    public TextMeshProUGUI NWSensor;
    public TextMeshProUGUI ESensor;
    public TextMeshProUGUI WSensor;
    public TextMeshProUGUI SSensor;

    // Scale the sensor outputs to be up to 10
    float scaleFactor = 10f;

    void FixedUpdate()
    {
        NSensor.text = (sensors.N_sensor() * scaleFactor).ToString("0.0");
        NWSensor.text = (sensors.NW_sensor() * scaleFactor).ToString("0.0");
        NESensor.text = (sensors.NE_sensor() * scaleFactor).ToString("0.0");
        WSensor.text = (sensors.W_sensor() * scaleFactor).ToString("0.0");
        ESensor.text = (sensors.E_sensor() * scaleFactor).ToString("0.0");
        SSensor.text = (sensors.S_sensor() * scaleFactor).ToString("0.0");

        if ((sensors.N_sensor() * scaleFactor) <= 0.7f){
            NSensor.color = Color.red;
        }
        else {
            NSensor.color = Color.white;
        }

        if ((sensors.NW_sensor() * scaleFactor) <= 0.7f){
            NWSensor.color = Color.red;
        }
        else {
            NWSensor.color = Color.white;
        }

        if ((sensors.NE_sensor() * scaleFactor) <= 0.5f){
            NESensor.color = Color.red;
        }
        else {
            NESensor.color = Color.white;
        }

        if ((sensors.S_sensor() * scaleFactor) <= 0.5f){
            SSensor.color = Color.red;
        }
        else {
            SSensor.color = Color.white;
        }

        if ((sensors.E_sensor() * scaleFactor) <= 0.35f){
            ESensor.color = Color.red;
        }
        else {
            ESensor.color = Color.white;
        }

        if ((sensors.W_sensor() * scaleFactor) <= 0.35f){
            WSensor.color = Color.red;
        }
        else {
            WSensor.color = Color.white;
        }

    }

}

