using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class textFunction : MonoBehaviour {

    public Text textoGame;
    protected string pathTableEqvLog = "Assets/NewAdds/eqvLogica.txt", eqvLogica,resp;
    private Vector3 posInicial;
    void Start() {
        textoGame.text = getEqvLog();
        posInicial = textoGame.transform.position;
    }

   void OnTriggerEnter2D(Collider2D other) {
    if (other.CompareTag("EqvLog")) {
        Debug.Log("EQV LOG AQUI");
        textoGame.transform.position = posInicial;
        textoGame.text = getEqvLog();
       /*posiçao resetada porem arquivo tambem sempre reseta o ponteiro precisa arrumar isso */
    }       
   }

   public string getEqvLog(){
        System.IO.StreamReader file = new System.IO.StreamReader(pathTableEqvLog);
        if (file.ReadLine() == "NOVA EQV LOGICA") {
            Debug.Log("Nova eqv logica aqui");
            Debug.Log(file.ReadLine());
            eqvLogica = file.ReadLine();
            Debug.Log("expr = " + eqvLogica);
            resp = file.ReadLine();
            Debug.Log("resp = " + resp);
        }else{
            Debug.Log("linha vazia?" + file.ReadLine());
            Debug.Log("nova eqv??" + file.ReadLine());
        }
       return eqvLogica;
   }
}
