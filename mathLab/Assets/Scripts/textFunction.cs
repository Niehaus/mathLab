using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class textFunction : MonoBehaviour {

    public Text textoGame;
    protected string pathTableEqvLog = "Assets/NewAdds/eqvLogica.txt", eqvLogica;
    void Start() {
        textoGame.text = getEqvLog();
    }

   void OnTriggerEnter2D(Collider2D other) {
    if (other.CompareTag("EqvLog")) {
        Debug.Log("EQV LOG AQUI");
    }       
   }

   public string getEqvLog(){
        System.IO.StreamReader file = new System.IO.StreamReader(pathTableEqvLog);
      
        if (file.ReadLine() == "NOVA EQV LOGICA")
        {
            Debug.Log("Nova eqv logica aqui");
            Debug.Log(file.ReadLine());
            eqvLogica = file.ReadLine();
            Debug.Log("expr = " + eqvLogica);
        }

       return eqvLogica;
   }
}
