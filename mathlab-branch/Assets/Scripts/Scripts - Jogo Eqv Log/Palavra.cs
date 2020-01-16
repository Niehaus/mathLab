using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palavra : Item  {
    private static readonly int BlowUp = Animator.StringToHash("blowUp");

    // Start is called before the first frame update
    void Start() {
        myAnim = GetComponent<Animator>();
        manager = FindObjectOfType<Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PalavraRoutine() {
        myAnim.SetBool(BlowUp,true);
        StartCoroutine(WaitToDestroy());
        //gameObject.GetComponent<Rigidbody2D>().mass = 10;
    }
}
