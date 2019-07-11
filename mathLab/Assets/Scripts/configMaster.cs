using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class configMaster : MonoBehaviour {

    public GameObject audioPanel, prefPanel,truthPanel, equivPanel, infPanel; // paineis
    public GameObject addConfirm; //texto de confirmação de adição!
    public InputField[] inputs; /*guarda as respostas recebidas, 
                                cada posição é um input diferente*/
    private static int tableCounter = 0;
    protected string pathTable = "Assets/NewAdds/tabelaVerdade.txt";
    protected string expr, num, resp;

    void CriaArquivo() {
        if (!File.Exists(pathTable))
        {
            File.WriteAllText(pathTable, "LOG FILE - ADICIONAR NOVA TABELA VERDADE " + tableCounter.ToString() + "\n");    
        }else
        {
            File.AppendAllText(pathTable, "LOG FILE - ADICIONAR NOVA TABELA VERDADE " + tableCounter.ToString() + "\n");
        }
        
    }
    public void openPanel(int panelNum) {
         
         switch (panelNum) {
            case (0):
                audioPanel.SetActive(true);
                truthPanel.SetActive(false);
                equivPanel.SetActive(false);
                infPanel.SetActive(false);
                prefPanel.SetActive(false);
                break;
            case (1):
                prefPanel.SetActive(true);
                audioPanel.SetActive(false);
                break;
            case (2):
                truthPanel.SetActive(true);
                equivPanel.SetActive(false);
                infPanel.SetActive(false);
                break;
            case (3): 
                equivPanel.SetActive(true);
                infPanel.SetActive(false);
                truthPanel.SetActive(false);
                break;
            case (4): 
                infPanel.SetActive(true);
                equivPanel.SetActive(false);
                truthPanel.SetActive(false);
                break;
        }
        
    }
    
    public void closePanel(int panelNum) {
        
            switch (panelNum) {
                case (0):
                    audioPanel.SetActive(false);
                    break;
                case (1):
                    prefPanel.SetActive(false);
                    truthPanel.SetActive(false);
                    equivPanel.SetActive(false);
                    infPanel.SetActive(false);
                    break;
                case (2):
                    truthPanel.SetActive(false);
                    break;
                case (3): 
                    equivPanel.SetActive(false);
                    break;
                case (4): 
                    infPanel.SetActive(false);
                    break;
            }
    }

    public void getTruthTable(int inputNum){

        switch (inputNum){
            case (0): //Expressão
                print(inputs[inputNum].text);
                expr = inputs[inputNum].text;
                break;
            case (1): //N° Var
                print(inputs[inputNum].text);
                num = inputs[inputNum].text;
                break;
            case (2): // Vetor Respostas
                print(inputs[inputNum].text);
                resp = inputs[inputNum].text;
                break;
        }
    }

    public void buttonWrite(){
        CriaArquivo();
        File.AppendAllText(pathTable, "Expr: " + expr + "\n");
        File.AppendAllText(pathTable, "Nº Var: " + num + "\n");
        File.AppendAllText(pathTable, "Respotas: " + resp + "\n");
        inputs[0].text = "";
        inputs[1].text = "";
        inputs[2].text = "";
        addConfirm.SetActive(true);
        StartCoroutine(undisplay()); 
        tableCounter += 1;
    }

    IEnumerator undisplay()
    {
        yield return new WaitForSeconds(1);
        addConfirm.SetActive(false);   
    }

}
