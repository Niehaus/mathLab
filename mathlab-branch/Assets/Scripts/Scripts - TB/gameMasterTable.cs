using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class GameMasterTable : MonoBehaviour {   
    public GameObject jogarButton, tablePanel, mainText, 
        dicasPanel, proxButton, userPanel;
    public Text textDica;
    protected string pathTable = "Assets/NewAdds/tabelaVerdade.txt";
    
    public void JogarTable() {
        mainText.SetActive(false);
        tablePanel.SetActive(true); // desativa gameobjet e childrens
        jogarButton.SetActive(false); 
    }
    public void OpenDicas() {
        dicasPanel.SetActive(true);
        proxButton.SetActive(true);
    }
    public void CloseDicas() {
        dicasPanel.SetActive(false);
        proxButton.SetActive(false);
        textDica.text = "OTIMAS DICAS AQUI \n CLIQUE EM PROX \n PARA MAIS DICAS!";
    }
    public void CloseTable(){
        tablePanel.SetActive(false);
        dicasPanel.SetActive(false);
    }
    public void ProxDica() {
        textDica.text = " Segunda otima dica aqui!";
    }
    public void NextTabela(){
        if (!File.Exists(pathTable)) {
            Debug.Log("Fim das tabelas");
            Application.Quit();
        }else {
            userPanel.SetActive(false);
            SceneManager.LoadScene("Jogo - Tabela Verdade Next (2)");
        }
    }
    
    public void JogarNovamente(){
        userPanel.SetActive(false);
        
        SetAddTables.counter = 0;
        SceneManager.LoadScene("Jogo - Tabela Verdade Next (2)");
    }

  
}