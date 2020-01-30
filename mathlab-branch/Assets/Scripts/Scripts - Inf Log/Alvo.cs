using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alvo : MonoBehaviour {

    public Municao municao;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Municao")) {
            municao.RecarregaBala();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
