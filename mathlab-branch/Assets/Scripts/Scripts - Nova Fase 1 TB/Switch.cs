using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Switch : MonoBehaviour {
    
    public Text textoTv;
    public Text variavelRef;
    public GameObject conjuntoTV;
    [NonSerialized] public string minhaResposta;
    private Animator _myAnim; 
    private bool _playerInRange;
    public bool switchAtivo = true;
    
    private static readonly int On = Animator.StringToHash("on");
    private static readonly int Colorful = Animator.StringToHash("colorful");
    private static readonly int GreenScreen = Animator.StringToHash("greenScreen");
    private static readonly int RedScreen = Animator.StringToHash("redScreen");

    // Start is called before the first frame update
    void Start() {
        _myAnim = GetComponent<Animator>();
    }
    
    
    private void Update() {
        if (!_playerInRange || !Input.GetKeyDown(KeyCode.Space)) return;
        _myAnim.SetBool(On, !_myAnim.GetBool(On));
        if (_myAnim.GetBool(On)) {
            textoTv.text = "V";    
        }
        else {
            textoTv.text = "F";
        }

        if (!switchAtivo) {
            _myAnim.SetBool(On, false);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            _playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (!other.CompareTag("Player")) return;
        _playerInRange = false;
    }

    public void ConjuntoSwitchAction(string instrucao, bool conjuntoTVAction, bool textoTVAction) {
        conjuntoTV.GetComponent<Animator>().SetBool(instrucao, conjuntoTVAction);
        textoTv.gameObject.SetActive(textoTVAction);
    }
}
