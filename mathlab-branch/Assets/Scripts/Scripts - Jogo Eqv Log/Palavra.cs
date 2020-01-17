using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palavra : Item  {
    
    private static readonly int BlowUp = Animator.StringToHash("blowUp");

    private Vector3 _originalPosition;
    private Rigidbody2D _rigidbody2D;

    // Start is called before the first frame update
    private void Start() {
        myAnim = GetComponent<Animator>();
        manager = FindObjectOfType<Manager>();
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _originalPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PalavraRoutine() {
        //myAnim.SetBool(BlowUp,true);
        //StartCoroutine(WaitToDestroy());
        //gameObject.GetComponent<Rigidbody2D>().mass = 10;
        gameObject.transform.position = _originalPosition;
        //TODO: prox passo: fazer item caindo, somar e acelerar com o tempo e contar o tempo
    }

    public void MakeMeFaster() {
        //TODO: DEIXAR PALAVRA MAIS RAPIDA NO TEMPO DA CENOURA
        _rigidbody2D.drag -= 0.2f;
    }
}
