using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class TimeScript : MonoBehaviour {

    public Text counterText;
    public bool timeCounter = true;
    public string seconds, minutes;
	
    void Awake() {
        counterText = GetComponent<Text> () as Text;	
    } 
	
    // Update is called once per frame
    void Update () {
        if (timeCounter) {
            seconds = Time.timeSinceLevelLoad.ToString("00");
            counterText.text = "Seconds: " + Time.timeSinceLevelLoad.ToString("00");
        }
    }
    public string EndGame() {
        timeCounter = false;
        counterText.color = Color.yellow;
        return seconds;
    }
}