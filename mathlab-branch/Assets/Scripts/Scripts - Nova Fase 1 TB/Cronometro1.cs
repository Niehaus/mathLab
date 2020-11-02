using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cronometro1 : MonoBehaviour
{
    public float timeStart;
    public float timeWrite;
    public Text textBox;

    public bool timerActive;
    
    // Use this for initialization
    private void Start () {
        textBox.text = timeStart.ToString("00:00");
        //timerActive = true;
    }
	
    // Update is called once per frame
    private void Update () {
        if (!timerActive) return;
        timeStart += Time.deltaTime;
        textBox.text = (timeWrite + timeStart).ToString("00:00");
        if (!(Math.Abs(timeStart) >= 60)) return;
        // StopTimer();
        timeStart = 0;
        timeWrite += 100.0f;
    }

    public float WhatTimeisIt() {
        return timeWrite + timeStart;
    }
    
    public void ControlTimer(){
        timerActive = !timerActive;
        //startBtnText.text = timerActive ? "Pause" : "Start";
    }
}
