﻿using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Npc : MonoBehaviour {
    
    private Rigidbody2D _myRb;
    protected Animator myAnim;
    
    [FormerlySerializedAs("nameNPC")] public string nameNpc;
    private static readonly int MoveX = Animator.StringToHash("moveX");
    private static readonly int MoveY = Animator.StringToHash("moveY");
    
    public GameObject caixaDialogo;
    public GameObject botaoContinuar;
    public GameObject balaodeFala;
    public Text dialogo;
    
    public string[] sentences;
    public int index;
    public int mission;
    protected int countSentences = 0;
    
    public float typingSpeed;
    [SerializeField] protected float speed;
    
    protected bool fimDialogo = false;
    protected bool[] acabouMissao = {false, true, false, false};
    
    // Start is called before the first frame update
    private void Start() {
        _myRb = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("BarX")) {
            //Debug.Log("colide BarX");
            _myRb.velocity = new Vector2(0.2f, 0).normalized * (speed * Time.deltaTime);
            myAnim.SetFloat(MoveX,_myRb.velocity.x);
            myAnim.SetFloat(MoveY,_myRb.velocity.y);
        }else if(other.gameObject.CompareTag("BarY")) {
           // Debug.Log("colide BarY");
            _myRb.velocity = new Vector2(-0.2f, 0).normalized * (speed * Time.deltaTime);
            myAnim.SetFloat(MoveX,_myRb.velocity.x);
            myAnim.SetFloat(MoveY,_myRb.velocity.y);
        }
    }

    private IEnumerator Type(){
        foreach (char letter in sentences[index]) {
            // ReSharper disable once HeapView.BoxingAllocation
            dialogo.text = $"{dialogo.text}{letter}";
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    protected void NpcFala() {
        botaoContinuar.SetActive(false);
        if (index < sentences.Length - 1) {
            index++;
            dialogo.text = "";
            StartCoroutine(Type());
        }else {
            dialogo.text = "";
            botaoContinuar.SetActive(false);
            Debug.Log("acabou?");
            fimDialogo = true;
        }
    }
}
