using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Somidouro : MonoBehaviour {

    public Button buttonV;
    public Button buttonF;
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Word")) return;
        var palavra = FindObjectOfType<Palavra>();
        if (buttonV.pressed && buttonV.pressedV == palavra.TurnMeBool(palavra.respostaAtual)) {
            //Debug.Log("resposta certa");
            palavra.PalavraRoutine(true);
        }
        else if (buttonF.pressed && buttonF.pressedF == palavra.TurnMeBool(palavra.respostaAtual)) {
            //Debug.Log("resposta certa");
            palavra.PalavraRoutine(true);
        }
        else {
            //Debug.Log("resposta errada");
            palavra.PalavraRoutine(false);
        }
    }
}
