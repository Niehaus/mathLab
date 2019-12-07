using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : NPC
{
    public Transform target;
    public float chaseRaio;
    public Transform posInicial;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void verificaDistancia() {
        if (Vector3.Distance(target.position, transform.position) <= chaseRaio) {
            transform.position = Vector3.MoveTowards(transform.position, target.position);
        }
    }
}
