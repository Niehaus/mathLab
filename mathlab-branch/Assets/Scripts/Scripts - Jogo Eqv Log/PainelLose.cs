﻿using System.Collections;
using System.Collections.Generic;
using RestSupport;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PainelLose : MonoBehaviour {
    private Manager _manager;

    public Text[] pontos;
    private int total;
    private int coins;
    private int hearts;
    public CadastroUser managerHTTP;
    
    // Start is called before the first frame update
    private void Start() {
        _manager = FindObjectOfType<Manager>();
    }

    public void AtualizaTexto() {
        total = Manager.coins + (Manager.coins/10) + (Manager.hearts * 5);
        coins = Manager.coins;
        hearts = Manager.hearts;
        pontos[0].text = "Moedas: " + coins + "x";
        pontos[1].text = "Vidas: " + hearts + "x"; 
        pontos[2].text = "Total: " + total + "x";
        ManagerGeral.totalPontosFase2 = total;
    }
    public void EnviaDados() {
     ManagerGeral.totalPontosFase2 = total;
     managerHTTP.AttUser(ManagerGeral.totalTempoFase1, ManagerGeral.totalPontosFase2, ManagerGeral.totalPontosFase3);   
    }
}
