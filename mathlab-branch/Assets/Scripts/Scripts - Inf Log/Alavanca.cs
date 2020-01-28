using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Alavanca : MonoBehaviour {


    private Animator _myAnim;
    public Canhao canhoes;
    public int identificador;
    private bool playerInRange;

    private static readonly int Ativa = Animator.StringToHash("ativa");

    // Start is called before the first frame update
    private void Start() {
        _myAnim = gameObject.GetComponent<Animator>();
    }

    private void Update() {
        if (!playerInRange || !Input.GetKeyDown(KeyCode.Space)) return;
        _myAnim.SetBool(Ativa, !_myAnim.GetBool(Ativa));
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (!other.CompareTag("Player")) return;
        playerInRange = false;
    }
}
