using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Municao : MonoBehaviour {

    private Animator _animator;

    private Rigidbody2D _rigidbody2D;
    private Vector3 _posInicial;
    private Renderer _renderer;


    
    // Start is called before the first frame update
    private void Start() {
        _animator = gameObject.GetComponent<Animator>();
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _renderer = gameObject.GetComponent<Renderer>();
        _renderer.enabled = false;
        _rigidbody2D.bodyType = RigidbodyType2D.Static;
        _posInicial = gameObject.transform.position;
    }
    
    public void RecarregaBala() {
        _rigidbody2D.bodyType = RigidbodyType2D.Static;
        Debug.Log("Para a bala");
        gameObject.transform.position = _posInicial;
        _renderer.enabled = false;
    }
    public void Disparo() {
        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        //_rigidbody2D.gravityScale = 1;
    }
}
