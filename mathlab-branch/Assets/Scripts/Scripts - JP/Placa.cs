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
    
    // Update is called once per frame
    private void Update() {
        if (!Input.GetKeyDown(KeyCode.Space) || !playerInRange) return;
        if (caixaDialogo.activeInHierarchy) {
            caixaDialogo.SetActive(false);
        }
        else {
            caixaDialogo.SetActive(true);
            dialogoTexto.text = dialogo;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (!other.CompareTag("Player")) return;
        playerInRange = false;
        caixaDialogo.SetActive(false);
    }
}
