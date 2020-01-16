using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rene : Npc {

    private bool _playerInRange = false;
    private PlayerController _disablekey; //bloqueador de teclado enquanto NPC fala
    // Start is called before the first frame update
    void Start() {
        _disablekey = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update() {
        if (index > 2 && acabouMissao[1]) {
            fimDialogo = true;
            _disablekey.keyboardAble = true;
            //TODO: ir para tela da missão se ela ja n tiver acontecido
        }
        if (dialogo.text == sentences[index]) {
            botaoContinuar.SetActive(true);
            _disablekey.keyboardAble = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && _playerInRange && _disablekey.keyboardAble) {
            caixaDialogo.SetActive(true);
            _disablekey.keyboardAble = false;
            Debug.Log("desativa teclado");
            NpcFala();
            if (fimDialogo) { 
                _disablekey.keyboardAble = true;
                caixaDialogo.SetActive(false);
                fimDialogo = false;
                Index();
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        Index();
        if (other.CompareTag("Player")) {
            _playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        Index();
        if (other.CompareTag("Player")) {
            _playerInRange = false;
            caixaDialogo.SetActive(false);
            //TODO: se a missão for feita muda o index pra um dialogo diferente
            fimDialogo = false;
        }
    }

    private void Index() {
        if (acabouMissao[1]) {
            index = 0;
        }
        else {
            index = 3;
        }
    }
}
