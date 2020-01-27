using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Button : MonoBehaviour {

    private Animator _myAnim;
    public bool buttonlink;
    private Text _texto;
    private static readonly int Pressed = Animator.StringToHash("pressed");

    [NonSerialized] public bool pressedV, pressedF, pressed;
    // Start is called before the first frame update
    private void Start() {
        _myAnim = GetComponent<Animator>();
        
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Player")) return;
        _myAnim.SetBool(Pressed, true);
        if (buttonlink) {
            _texto = GameObject.FindGameObjectWithTag("Button V").GetComponent<Text>();
            pressed = true; pressedV = true;
            _texto.color = Color.green;
        }else {
            _texto = GameObject.FindGameObjectWithTag("Button F").GetComponent<Text>();
            pressed = true;  pressedF = false;
            _texto.color = Color.green;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (!other.CompareTag("Player")) return;
        _myAnim.SetBool(Pressed, false);
        if (buttonlink) {
            _texto = GameObject.FindGameObjectWithTag("Button V").GetComponent<Text>();
            _texto.color = new Color(0.768627f, 0.768627f, 0.768627f, 1f);
            pressed = false; pressedV = false;
        }else {
            _texto = GameObject.FindGameObjectWithTag("Button F").GetComponent<Text>();
            _texto.color = new Color(0.768627f, 0.768627f, 0.768627f, 1f);
            pressed = false; pressedF = false;
        }
    }
}

