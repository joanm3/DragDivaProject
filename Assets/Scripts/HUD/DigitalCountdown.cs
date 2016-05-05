using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class DigitalCountdown : MonoBehaviour {
    private Text textClock;

    
    public CountdownTimer countdownTimer;
    public Color alertColor = new Color (180, 0, 0);
    public Color searchColor = new Color(295, 190, 0); 


    void Start()
    {
        textClock = GetComponent<Text>();
        countdownTimer.ResetTimer(countdownTimer.countdownTimerDuration);

    }

    void Update()
    {
        //default timer finished
        string timerMessage = " ";
        float timeLeft = countdownTimer.GetFractionSecondsRemaining();

        if (timeLeft >= 9.97)
        {

            timerMessage = "ALERT:  9.99";
            textClock.color = alertColor;
        }

        if (timeLeft > 0 && timeLeft <=9.97)
        { 
           
            timerMessage = "SEARCHING:  " + LeadingZero(timeLeft);
            textClock.color = searchColor; 
        }
        textClock.text = timerMessage;

    }


    public string LeadingZero (float n)
    {
        return n.ToString("0.00");
    }


}
