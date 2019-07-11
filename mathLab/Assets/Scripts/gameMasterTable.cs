using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameMasterTable : MonoBehaviour
{

    
    public GameObject jogarButton, tablePanel, mainText, dicasPanel, proxButton;
    public Text textDica;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void jogarTable() {
        mainText.SetActive(false);
        tablePanel.SetActive(true); // desativa gameobjet e childrens
        jogarButton.SetActive(false); 
    }
    public void openDicas() {
        dicasPanel.SetActive(true);
        proxButton.SetActive(true);
    }
    public void closeDicas() {
        dicasPanel.SetActive(false);
        proxButton.SetActive(false);
        textDica.text = "OTIMAS DICAS AQUI \n CLIQUE EM PROX \n PARA MAIS DICAS!";
    }
    public void closeTable(){
        tablePanel.SetActive(false);
        dicasPanel.SetActive(false);
    }
    public void proxDica() {
        textDica.text = " Segunda otima dica aqui!";
    }
}
