using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
public class setAddTables : MonoBehaviour { 
    public Text[] colunas;
    protected string pathTable = "Assets/NewAdds/tabelaVerdade.txt", line, expr;
    private string[] resp;
    protected string[] retornaArray = new String[32];
    private int tableNumber = 0, numLinhas;
    static int counter = 0;
    // Ler o arquivo e pegar a tabela - setar o ponteiro no arquivo pra ler a prox tabela 
    public void setaTabela() {
        System.IO.StreamReader file = new System.IO.StreamReader(pathTable);
        while ((line = file.ReadLine()) != null)  {
            if (line == "NOVA TABELA VERDADE" ) { //verifica se é um tabela
                line = file.ReadLine();
                tableNumber = convert(line[0]); // pega o numero dessa tabela e convert p int
                if (tableNumber == counter) { //instruçoes de leitura aqui
                    Debug.Log("Tabela " + tableNumber);
                    expr = file.ReadLine(); //pega expressao
                    line = file.ReadLine(); //pega num de linhas da tabela
                    numLinhas = convert(line[0]);
                    Debug.Log(numLinhas);
                    Debug.Log(expr);
                    double TotalDeLinhas = Math.Pow(2, Convert.ToDouble(numLinhas));
                    Debug.Log("Linhas da tabela");
                    for (int i = 0; i < TotalDeLinhas; i++) { //a partir da conta do total de linhas pega tdas as linhas
                        Debug.Log(file.ReadLine());
                    }
                    file.ReadLine(); //lê o espaço entre a tabela e as respostas
                    line = file.ReadLine();
                    resp = splitString(line); //separa o vetor de respostas p associar aos inputs
                    foreach (var item in resp) {
                        if (item != null) {
                            Debug.Log(item);    
                        }
       
                    }
                    //Debug.Log("tabela numero " + tableNumber + "counter " + counter);
                    counter++;
                    break;
                }
            }
            //Debug.Log("Line while " + line);
            //Debug.Log("não é igual, prox");
        }
       if ((line = file.ReadLine()) == null)
       {
            Debug.Log("não tem prox tabela");
       }
      //  file.Close();  
    }
    public int convert(char numVar){
        int convertido = (int)Char.GetNumericValue(numVar);
        return convertido;
    }

    public String[] splitString(string expr)
    {  //separa a string a partir dos operadores - get variaveis
        int k = 0;
        string[] multiArray = expr.Split(new Char[] { ' ', '^', '|', '(', ')', '[', ']' });
        foreach (string author in multiArray) {
            if (author.Trim() != "")
            {
                //Debug.Log(k + " " + author);
                retornaArray[k] = author;
                k++;
            }
        }
        return retornaArray;
    }

}
