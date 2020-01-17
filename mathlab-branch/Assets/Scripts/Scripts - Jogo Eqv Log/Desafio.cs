using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desafio : MonoBehaviour {
    
    public Desafio(bool resposta) {
        Resposta = resposta;
    }
    public string Expressao { get; set; }

    public bool Resposta { get; set; }
    
}
