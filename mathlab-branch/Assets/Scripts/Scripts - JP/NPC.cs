using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Npc : MonoBehaviour {
    
    protected Rigidbody2D MyRb;
    protected Animator MyAnim;
    
    [FormerlySerializedAs("nameNPC")] public string nameNpc;
    private static readonly int MoveX = Animator.StringToHash("moveX");
    private static readonly int MoveY = Animator.StringToHash("moveY");
    
    [SerializeField] protected float speed;
    public GameObject caixaDialogo;
    public GameObject botaoContinuar;
    public Text dialogo;
    public string[] sentences;
    public int index = 0;
    public int mission;
    public float typingSpeed;
    protected int countSentences = 0;
    protected Boolean fimDialogo = false;
    

    // Start is called before the first frame update
    void Start() {
        MyRb = GetComponent<Rigidbody2D>();
        MyAnim = GetComponent<Animator>();
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("BarX")) {
            //Debug.Log("colide BarX");
            MyRb.velocity = new Vector2(0.2f, 0).normalized * speed * Time.deltaTime;
            MyAnim.SetFloat(MoveX,MyRb.velocity.x);
            MyAnim.SetFloat(MoveY,MyRb.velocity.y);
        } else if(other.gameObject.CompareTag("BarY")) {
           // Debug.Log("colide BarY");
            MyRb.velocity = new Vector2(-0.2f, 0).normalized * speed * Time.deltaTime;
            MyAnim.SetFloat(MoveX,MyRb.velocity.x);
            MyAnim.SetFloat(MoveY,MyRb.velocity.y);
        }
    }

    IEnumerator Type(){
        foreach (char letter in sentences[index].ToCharArray()) {
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
