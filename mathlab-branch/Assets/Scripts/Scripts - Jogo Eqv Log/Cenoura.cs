using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cenoura : Item {
    // Start is called before the first frame update
    private void Start() {
        myAnim = GetComponent<Animator>();
        playerController = FindObjectOfType<PlayerController>();
    }
}
