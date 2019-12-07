using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Npc : MonoBehaviour {
    
    protected Rigidbody2D MyRb;
    protected Animator MyAnim;
    
    [FormerlySerializedAs("nameNPC")] public string nameNpc;
    public int mission;
    [SerializeField] protected float speed;
    
    public GameObject caixaDialogo;
    public Text dialogo;
    protected int countSentences = 0;
    
    // Start is called before the first frame update
    void Start() {
        MyRb = GetComponent<Rigidbody2D>();
        MyAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("BarX")) {
            Debug.Log("colide BarX");
            MyRb.velocity = new Vector2(0.2f, 0).normalized * speed * Time.deltaTime;
            MyAnim.SetFloat("moveX",MyRb.velocity.x);
            MyAnim.SetFloat("moveY",MyRb.velocity.y);
        } else if(other.gameObject.CompareTag("BarY")) {
            Debug.Log("colide BarY");
            MyRb.velocity = new Vector2(-0.2f, 0).normalized * speed * Time.deltaTime;
            MyAnim.SetFloat("moveX",MyRb.velocity.x);
            MyAnim.SetFloat("moveY",MyRb.velocity.y);
        }
    }


}
