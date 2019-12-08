using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : Npc
{
    public Transform target;
    public Transform posInicial;
    public float chaseRaio;
    public float talkRaio;
    
    private PlayerController _disablekey;
    public string[] sentences;

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
        dialogo.text = sentences[countSentences];
        countSentences++;
    }

    // Update is called once per frame
    void Update() {
        NpcAction();
        if (!_disablekey.KeyboardAble && caixaDialogo) { //dentro de um dialogo
            if (Input.GetKeyDown(KeyCode.Space)) {
                dialogo.text = sentences[countSentences];
                countSentences++;
                if (countSentences == sentences.Length) { // o dialogo acabou
                    Debug.Log("dialogo acabou prox cena pls");
                    //TODO: move para prox cena
                    //TODO: esbloqueia teclado ou desbloqueia só na próxima
                }
            }
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
