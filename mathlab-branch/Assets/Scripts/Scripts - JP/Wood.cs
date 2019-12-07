using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : NPC
{
    public Transform target;
    public Transform posInicial;
    public float chaseRaio;
    public float talkRaio;
    
    // Start is called before the first frame update
    void Start() {
        target = GameObject.FindWithTag("Player").transform;
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        verificaDistancia();
    }
    
    void verificaDistancia() {
        if (Vector3.Distance(target.position, transform.position) <= chaseRaio && Vector3.Distance(target.position, transform.position) > talkRaio) {
            var position = transform.position;
            position = Vector3.MoveTowards(position, target.position, speed* Time.deltaTime);  
            transform.position = position;
            if (position.y <= 0) myAnim.SetFloat("moveY", position.y); else myAnim.SetFloat("moveY", - position.y);
        } else if (Vector3.Distance(target.position,transform.position) <= talkRaio) {
            myAnim.SetFloat("moveY", 0);
            myAnim.SetFloat("moveX", 0);
            //ativar fala aqui
        }
    }
}
