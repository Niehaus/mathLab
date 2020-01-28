using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerInf : MonoBehaviour {

    public Alavanca[] alavancas;
    private static readonly int Ativa = Animator.StringToHash("ativa");

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var alavanca in alavancas) {
            if (alavanca.GetComponent<Animator>().GetBool(Ativa)) {
                Debug.Log("Alavanca " + alavanca.identificador + " ativa");
            }
        }
    }
}
