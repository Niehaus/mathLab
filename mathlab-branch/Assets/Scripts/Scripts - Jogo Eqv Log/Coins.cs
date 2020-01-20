using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : Item
{
    // Start is called before the first frame update
    private void Start() {
        myAnim = GetComponent<Animator>();
        manager = FindObjectOfType<Manager>();
    }
    
}
