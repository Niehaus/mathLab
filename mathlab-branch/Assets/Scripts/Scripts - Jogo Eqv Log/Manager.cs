using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

    public Text[] contador;

     internal static int _coins;
     internal static int _hearts = 3;
    
    // Start is called before the first frame update
    void Start() {
        contador[0].text = _coins + "x";
        contador[1].text =  _hearts + "x" ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
