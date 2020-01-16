using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Somidouro : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Word")) {
            Debug.Log("palavra aqui");
            var palavra = FindObjectOfType<Palavra>();
            
            palavra.PalavraRoutine(); 
           
        }
    }
}
