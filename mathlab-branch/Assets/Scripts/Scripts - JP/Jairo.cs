using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jairo : Npc {

    private bool playerInRange = false;
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
       
        if (dialogo.text == sentences[index]) {
            botaoContinuar.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange) {
            caixaDialogo.SetActive(true);
            Debug.Log("abre caixa");
            NpcFala();
            if (fimDialogo) {
               caixaDialogo.SetActive(false);
               Debug.Log("acabou aqui");
                //ir para tela da missão se ela ja n tiver acontecido
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Index();
        if (other.CompareTag("Player")) {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        Index();
        if (other.CompareTag("Player")) {
            playerInRange = false;
            caixaDialogo.SetActive(false);
            //se a missão for feita muda o index pra um dialogo diferente
            fimDialogo = false;
        }
    }

    private void Index() {
        if (acabouMissao[1]) {
            index = 0;
        }
        else {
            index = 2;
        }
    }
}
