using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Placa : MonoBehaviour  {

    public GameObject caixaDialogo;
    
    public Text dialogoTexto;
    
    public string dialogo;

    public bool playerInRange;
    
    // Start is called before the first frame update
    void Start()  {
        
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange) {
            if (caixaDialogo.activeInHierarchy) {
                caixaDialogo.SetActive(false);
            }
            else {
                caixaDialogo.SetActive(true);
                dialogoTexto.text = dialogo;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
           
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
           playerInRange = false;
           caixaDialogo.SetActive(false);
        }
    }
}
