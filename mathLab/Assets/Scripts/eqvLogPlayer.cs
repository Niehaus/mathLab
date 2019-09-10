using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eqvLogPlayer : MonoBehaviour {
    // Start is called before the first frame update
    public float speed;             //Floating point variable to store the player's movement speed.
    public bool facingRight = false;
    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.
    private Animator anim;
    private SpriteRenderer playerRenderer;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>(); // obtém a animação do hero
        rb2d = GetComponent<Rigidbody2D>(); //Get and store a reference to the Rigidbody2D component so that we can access it.
        playerRenderer = GetComponent<SpriteRenderer>();
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate() {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis("Vertical");

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        
        //VIRA O SPRITE CONFORME O MOVIMENTO - Movimento p/ esqr e sprite p/ dir
        if (moveHorizontal < 0 && facingRight) {
            playerRenderer.flipX = true;
            facingRight = !facingRight;
            anim.SetBool("run", true);
        }else if(moveHorizontal > 0 && !facingRight) { //mov p/ direita sprite p/ esq
            playerRenderer.flipX = false;
            facingRight = !facingRight;
            anim.SetBool("run", true);
        }else if(moveHorizontal == 0) {
            anim.SetBool("run",false);
        }
        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb2d.AddForce(movement * speed);
    }
}
