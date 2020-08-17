using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class infoBook : MonoBehaviour {
    
    public string conteudo;
    public GameObject painel_conteudo;
    public GameObject balao_fala;
    private GameObject _texto_balao_fala;
    private PlayerController _disablekey;
    private bool _playerInRange;
    
    private void Start() {
        _disablekey = FindObjectOfType<PlayerController>();
    }

    private void Update() {
        if (_playerInRange) {
            balao_fala.SetActive(true);
            _texto_balao_fala = balao_fala.transform.GetChild(0).gameObject;
            _texto_balao_fala.GetComponent<Text>().text = "Aperte 'Espaço' para ler sobre " + conteudo;
        }
        else {
            balao_fala.SetActive(false);
        }
        if (!Input.GetKeyDown(KeyCode.Space) || !_playerInRange || !_disablekey.keyboardAble) return;
        //painel_conteudo.SetActive(true);
        _disablekey.keyboardAble = false;
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        //Index();
        if (other.CompareTag("Player")) {
            _playerInRange = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        if (!other.CompareTag("Player")) return;
        _playerInRange = false;
    }

    public void fechaPainel() {
        painel_conteudo.SetActive(false);
        _disablekey.keyboardAble = true;
    }
}
