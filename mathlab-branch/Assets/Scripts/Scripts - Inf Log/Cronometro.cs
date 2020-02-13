using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cronometro : MonoBehaviour
{
    public float timeStart;
    public Text textBox;
    public Text startBtnText;

    public bool timerActive = false;

    public ManagerInf manager;
    // Use this for initialization
    private void Start () {
        textBox.text = timeStart.ToString("0");
        //timerActive = true;
    }
	
    // Update is called once per frame
    private void Update () {
        if(timerActive) {
            timeStart -= Time.deltaTime;
            textBox.text = timeStart.ToString("0");
            if (Math.Abs(timeStart) < 1) {
                StopTimer();
                manager.VerificaCanhoes();
            }
        }
    }

    private void StopTimer(){
        timerActive = !timerActive;
        //startBtnText.text = timerActive ? "Pause" : "Start";
    }
}
