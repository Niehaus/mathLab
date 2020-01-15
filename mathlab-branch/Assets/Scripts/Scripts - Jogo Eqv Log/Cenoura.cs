using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cenoura : Item {
    
  
    // Start is called before the first frame update
    void Start() {
        myAnim = GetComponent<Animator>();
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
