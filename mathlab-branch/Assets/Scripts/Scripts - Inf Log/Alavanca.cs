using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Alavanca : MonoBehaviour {


    private Animator _myAnim;
    public Canhao canhoes;
    public int identificador;
    public AudioClip[] onOff;
    private bool _playerInRange;
    private AudioSource _audioSource;
    private static readonly int Ativa = Animator.StringToHash("ativa");

    // Start is called before the first frame update
    private void Start() {
        _myAnim = gameObject.GetComponent<Animator>();
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void Update() {
        if (!_playerInRange || !Input.GetKeyDown(KeyCode.Space)) return;
        _myAnim.SetBool(Ativa, !_myAnim.GetBool(Ativa));
        AlavancasSound(_myAnim.GetBool((Ativa)));
    }

    public void AlavancasSound(bool alavanca_status) {
        if (!alavanca_status) {
            _audioSource.clip = onOff[0];
            _audioSource.Play();
        }
        else {
            _audioSource.clip = onOff[1];
            _audioSource.Play();
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
}
