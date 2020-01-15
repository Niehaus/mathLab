using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : Item
{
    // Start is called before the first frame update
    void Start() {
        myAnim = GetComponent<Animator>();
        _manager = FindObjectOfType<Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
}
