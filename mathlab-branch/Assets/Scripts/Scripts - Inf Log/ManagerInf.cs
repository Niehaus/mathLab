using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerInf : MonoBehaviour {

    public Alavanca[] alavancas;
    public Text[] textoResposta;
    public Text comandos;
    private static readonly int Ativa = Animator.StringToHash("ativa");

    // Start is called before the first frame update
    private void Start() {
        StartCoroutine(VerificaCanhoes());
    }
    
    private IEnumerator VerificaCanhoes() { //seta animações tempos etc
        
         Debug.Log("começa a contar ");
        yield return new WaitForSeconds(5);
        foreach (var alavanca in alavancas) {
            if (alavanca.GetComponent<Animator>().GetBool(Ativa)) {
                Debug.Log("Alavanca " + alavanca.identificador + " ativa");
                alavanca.canhoes.AtivaCanhao();
            }
            else {
                Debug.Log("Alavanca " + alavanca.identificador + " desligada");
            }
        }
    }
    
    // Update is called once per frame
    private void Update()
    {
        
    }
}
