using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class button : MonoBehaviour {

    private Animator myAnim;
    public bool buttonlink;
    private Text texto;
    private static readonly int Pressed = Animator.StringToHash("pressed");

    // Start is called before the first frame update
    void Start() {
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            myAnim.SetBool(Pressed, true);
            if (buttonlink) {
                texto = GameObject.FindGameObjectWithTag("Button V").GetComponent<Text>();
                texto.color = Color.green;
            }else {
                texto = GameObject.FindGameObjectWithTag("Button F").GetComponent<Text>();
                texto.color = Color.green;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")){
            myAnim.SetBool(Pressed, false);
            if (buttonlink) {
                texto = GameObject.FindGameObjectWithTag("Button V").GetComponent<Text>();
                texto.color = new Color(0.768627f, 0.768627f, 0.768627f, 1f);
            }else {
                texto = GameObject.FindGameObjectWithTag("Button F").GetComponent<Text>();
                texto.color = new Color(0.768627f, 0.768627f, 0.768627f, 1f);
            }
        }
    }
}

