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

    // Start is called before the first frame update
    void Start() {
        _myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            _myAnim.SetBool(Pressed, true);
            if (buttonlink) {
                _texto = GameObject.FindGameObjectWithTag("Button V").GetComponent<Text>();
                _texto.color = Color.green;
            }else {
                _texto = GameObject.FindGameObjectWithTag("Button F").GetComponent<Text>();
                _texto.color = Color.green;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")){
            _myAnim.SetBool(Pressed, false);
            if (buttonlink) {
                _texto = GameObject.FindGameObjectWithTag("Button V").GetComponent<Text>();
                _texto.color = new Color(0.768627f, 0.768627f, 0.768627f, 1f);
            }else {
                _texto = GameObject.FindGameObjectWithTag("Button F").GetComponent<Text>();
                _texto.color = new Color(0.768627f, 0.768627f, 0.768627f, 1f);
            }
        }
    }
}

