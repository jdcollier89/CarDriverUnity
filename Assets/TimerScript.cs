using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    public TextMeshProUGUI timerTracker;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        int minuteTimer = (int) timer/60;
        float secondTimer = timer%60;
        timerTracker.text = minuteTimer.ToString("0") + ":" + secondTimer.ToString("00.00") + "s";
    }
}
