using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWalk : MonoBehaviour {

    private Rigidbody2D myRB;
    private Animator myAnim;
    [SerializeField]
    private float speed;

    // Start is called before the first frame update
    void Start() {
        myRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()     {
        myRB.velocity = new Vector2(0.2f, 0.2f).normalized * speed * Time.deltaTime;
        myAnim.setFloat("moveX",myRB.velocity.x);
        myAnim.setFloat("moveY",myRB.velocity.y);
        
    }
}
