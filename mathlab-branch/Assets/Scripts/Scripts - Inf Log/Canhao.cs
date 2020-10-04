using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Canhao : MonoBehaviour {
    
    private Animator _myAnim;
    public Municao municao;
    public int identificador;
    
    // Start is called before the first frame update
    private void Start() {
        _myAnim = gameObject.GetComponent<Animator>();
    }

    public void AtivaCanhao() {
        //TODO: ativa objeto da bala
        municao.gameObject.GetComponent<Renderer>().enabled = true;
        municao.Disparo();
    }
}
