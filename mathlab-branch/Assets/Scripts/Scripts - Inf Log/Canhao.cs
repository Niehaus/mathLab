using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Canhao : MonoBehaviour {
    
    private Animator _myAnim;
    public GameObject municao;
    public int identificador;
    // Start is called before the first frame update
    private void Start() {
        _myAnim = gameObject.GetComponent<Animator>();
    }

    public void Atira() {
        //ativa animação de tiro 
    }
}
