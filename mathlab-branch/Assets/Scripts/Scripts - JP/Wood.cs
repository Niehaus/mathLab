﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
public class Wood : Npc {
    public Transform target;
    public Transform posInicial;
    public float chaseRaio;
    public float talkRaio;
    private PlayerController _disablekey;
    
    

    private static readonly int MoveY = Animator.StringToHash("moveY");
    private static readonly int MoveX = Animator.StringToHash("moveX");

    // Start is called before the first frame update
    void Start() {
        _disablekey = FindObjectOfType<PlayerController>();
        target = GameObject.FindWithTag("Player").transform;
        MyAnim = GetComponent<Animator>();
        
        _disablekey.KeyboardAble = false;
        _disablekey.faceIt.y = 0.15f;
        caixaDialogo.SetActive(false);
        Debug.Log("index antes" + index);
        NpcFala();
        Debug.Log(index);
    }

    // Update is called once per frame
    void Update() {
        NpcAction();
        if (!_disablekey.KeyboardAble && caixaDialogo && !fimDialogo) { //dentro de um dialogo
            if (dialogo.text == sentences[index]) { //verifica se está okay a frase dita e ativa o botão p/ prox
                botaoContinuar.SetActive(true);        
                if (Input.GetKeyDown(KeyCode.Space)) {
                    NpcFala();
                }
            }
        }
        else {
            SceneManager.LoadScene("Scenes/Jogo Tabela Verdade");
        }
    }

    private void NpcAction() { //npc anda até o player e inicia a conversa automaticamente
        if (Vector3.Distance(target.position, transform.position) <= chaseRaio && Vector3.Distance(target.position, transform.position) > talkRaio) {
            var position = transform.position;
            position = Vector3.MoveTowards(position, target.position, speed* Time.deltaTime);  
            transform.position = position;
            if (position.y <= 0) MyAnim.SetFloat(MoveY, position.y); else MyAnim.SetFloat(MoveY, - position.y);
        } else if (Vector3.Distance(target.position,transform.position) <= talkRaio) {
            MyAnim.SetFloat(MoveY, 0); MyAnim.SetFloat(MoveX, 0);
            caixaDialogo.SetActive(true);
        }
    }
}
