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
    [NonSerialized]
    public Vector2 faceIt;
    public VectorValue startingPosition;

    private static readonly int MoveX = Animator.StringToHash("moveX");
    private static readonly int MoveY = Animator.StringToHash("moveY");
    private static readonly int LastMoveX = Animator.StringToHash("lastMoveX");
    private static readonly int LastMoveY = Animator.StringToHash("lastMoveY");

    // Start is called before the first frame update
    void Start() { //TODO: não esquecer de fazer a volta da cena para qual posição o player deve ficar**
        _myRb = GetComponent<Rigidbody2D>();
        _myAnim = GetComponent<Animator>();
        transform.position = startingPosition.valorInicial;
        _myAnim.SetFloat(LastMoveX, value: faceIt.x);
        _myAnim.SetFloat(LastMoveY, value: faceIt.y);
    }
    // Update is called once per frame
    void Update() {
        if (KeyboardAble) {
            _myRb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * speed * Time.deltaTime;
            _myAnim.SetFloat(MoveX,_myRb.velocity.x);
            _myAnim.SetFloat(MoveY,_myRb.velocity.y);

            if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1 ) {
                _myAnim.SetFloat(LastMoveX, Input.GetAxisRaw("Horizontal"));
                _myAnim.SetFloat(LastMoveY, Input.GetAxisRaw("Vertical"));
            }    
        }
    }
}
