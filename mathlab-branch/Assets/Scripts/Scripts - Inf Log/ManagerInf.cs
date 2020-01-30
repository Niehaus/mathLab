using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerInf : MonoBehaviour {

    public Alavanca[] alavancas;
    public Text[] textoResposta;
    public Text[] contadores;
    public Text comandos;
    private int _hearts = 3;
    private int _coins;
    private static readonly int Ativa = Animator.StringToHash("ativa");

    // Start is called before the first frame update
    private void Start() {
        StartCoroutine(VerificaCanhoes());
        contadores[1].text = _hearts + "x";
        contadores[0].text = _coins + "x";
    }
    
    private IEnumerator VerificaCanhoes() { //seta animações tempos etc
        
         Debug.Log(message: "começa a contar ");
        yield return new WaitForSeconds(seconds: 5);
        foreach (var alavanca in alavancas) {
            if (alavanca.GetComponent<Animator>().GetBool(id: Ativa)) {
                Debug.Log(message: "Alavanca " + alavanca.identificador + " ativa");
                alavanca.canhoes.AtivaCanhao();
            }
            else {
                Debug.Log(message: "Alavanca " + alavanca.identificador + " desligada");
            }
        }
    }


    
    // Update is called once per frame
    private void Update()
    {
        
    }
}
