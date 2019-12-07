using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _myRb;
    private Animator _myAnim;
    [SerializeField]
    private float speed;
    [NonSerialized]
    public Boolean KeyboardAble = true;
    // Start is called before the first frame update
    void Start() {
        _myRb = GetComponent<Rigidbody2D>();
        _myAnim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update() {
        if (KeyboardAble) {
            _myRb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * speed * Time.deltaTime;
            _myAnim.SetFloat("moveX",_myRb.velocity.x);
            _myAnim.SetFloat("moveY",_myRb.velocity.y);

            if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1 ) {
                _myAnim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                _myAnim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
            }    
        }
    }
}
