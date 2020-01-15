using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pao : Item {
    
    private static readonly int Picked = Animator.StringToHash("picked");

    
    // Start is called before the first frame update
    void Start() {
        myAnim = GetComponent<Animator>();
        _manager = FindObjectOfType<Manager>();

        
    }

    // Update is called once per frame
    void Update() {
        
        
    }
    
   
}
