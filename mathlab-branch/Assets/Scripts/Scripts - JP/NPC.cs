using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Npc : MonoBehaviour {
    
    protected Rigidbody2D myRb;
    protected Animator myAnim;
    
    [FormerlySerializedAs("nameNPC")] public string nameNpc;
    private static readonly int MoveX = Animator.StringToHash("moveX");
    private static readonly int MoveY = Animator.StringToHash("moveY");
    
  
    
    public GameObject caixaDialogo;
    public GameObject botaoContinuar;
    public Text dialogo;
    
    public string[] sentences;
    public int index = 0;
    public int mission;
    protected int countSentences = 0;
    
    public float typingSpeed;
    [SerializeField] protected float speed;
    
    protected bool fimDialogo = false;
    protected bool[] acabouMissao = {false, false, false, false};
    

    // Start is called before the first frame update
    void Start() {
        myRb = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        
        
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("BarX")) {
            //Debug.Log("colide BarX");
            myRb.velocity = new Vector2(0.2f, 0).normalized * speed * Time.deltaTime;
            myAnim.SetFloat(MoveX,myRb.velocity.x);
            myAnim.SetFloat(MoveY,myRb.velocity.y);
        } else if(other.gameObject.CompareTag("BarY")) {
           // Debug.Log("colide BarY");
            myRb.velocity = new Vector2(-0.2f, 0).normalized * speed * Time.deltaTime;
            myAnim.SetFloat(MoveX,myRb.velocity.x);
            myAnim.SetFloat(MoveY,myRb.velocity.y);
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
