using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaInicial : MonoBehaviour {
    private Rigidbody2D _rigidbody2D;
    private Vector3 _posInicial;
    // Start is called before the first frame update
    private void Start() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _posInicial = gameObject.transform.position;
        _rigidbody2D.velocity = new Vector2(120f,0f);
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Sumidouro")) return;
        //Debug.Log("Reseta posição aqui");
        gameObject.transform.position = _posInicial;
    }
}
