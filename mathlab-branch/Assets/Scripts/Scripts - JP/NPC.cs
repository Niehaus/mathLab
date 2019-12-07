using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWalk : MonoBehaviour {

    private Rigidbody2D myRB;
    private Animator myAnim;
    [SerializeField]
    private float speed;

    public string nameNPC;
    public int mission;

    // Start is called before the first frame update
    void Start() {
        myRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
      /*  myRB.velocity = new Vector2(0.2f, 0).normalized * speed * Time.deltaTime;
        myAnim.SetFloat("moveX",myRB.velocity.x);
        myAnim.SetFloat("moveY",myRB.velocity.y);*/
   }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("BarX")) {
            Debug.Log("colide BarX");
            myRB.velocity = new Vector2(0.2f, 0).normalized * speed * Time.deltaTime;
            myAnim.SetFloat("moveX",myRB.velocity.x);
            myAnim.SetFloat("moveY",myRB.velocity.y);
        } else if(other.gameObject.CompareTag("BarY")) {
            Debug.Log("colide BarY");
            myRB.velocity = new Vector2(-0.2f, 0).normalized * speed * Time.deltaTime;
            myAnim.SetFloat("moveX",myRB.velocity.x);
            myAnim.SetFloat("moveY",myRB.velocity.y);
        }
    }


}
