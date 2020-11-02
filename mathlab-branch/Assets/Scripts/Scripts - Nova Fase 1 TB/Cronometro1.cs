using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cronometro1 : MonoBehaviour
{
    public float timeStart;
    public Text textBox;
    public Text startBtnText;

    public bool timerActive = false;
    
    // Use this for initialization
    private void Start () {
        textBox.text = timeStart.ToString("00:00");
        //timerActive = true;
    }
	
    // Update is called once per frame
    private void Update () {
        if (!timerActive) return;
        timeStart += Time.deltaTime;
        textBox.text = timeStart.ToString("00:00");
        if (Math.Abs(timeStart) < 1) {
            StopTimer();
        }
    }

    private void StopTimer(){
        timerActive = !timerActive;
        //startBtnText.text = timerActive ? "Pause" : "Start";
    }
}
