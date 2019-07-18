using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
public class setAddTables : MonoBehaviour { 
    public Text[] colunas;
    public Text expressao;
    protected string pathTable = "Assets/NewAdds/tabelaVerdade.txt", line;
    private string[] resp, expr;
    protected string[] retornaArray = new String[32];
    private int tableNumber = 0, numLinhas;
    private Hashtable convertColunas = new Hashtable();
    static int counter = 0;
    // Ler o arquivo e pegar a tabela - setar o ponteiro no arquivo pra ler a prox tabela 
    void Start() {
        //colunas positivas
        convertColunas.Add("p",0);
        convertColunas.Add("q",1);
        convertColunas.Add("r",2);
        convertColunas.Add("s",3);
        convertColunas.Add("t",4);
        //colunas negadas
        convertColunas.Add("-p", 5);
        convertColunas.Add("-q", 6);
        convertColunas.Add("-r", 7);
        convertColunas.Add("-s", 8);
        convertColunas.Add("-t", 9);
    }
    public void setaTabela() {
        System.IO.StreamReader file = new System.IO.StreamReader(pathTable);
        while ((line = file.ReadLine()) != null)  {
            if (line == "NOVA TABELA VERDADE" ) { //verifica se é um tabela
                line = file.ReadLine();
                tableNumber = convert(line[0]); // pega o numero dessa tabela e convert p int
                if (tableNumber == counter) { //instruçoes de leitura aqui
                    Debug.Log("Tabela " + tableNumber);
                    line = file.ReadLine(); //pega expressao
                    expressao.text = line;
                    expr = splitString(line); //separa expr pra associar uma coluna da tabela a uma variavel
                    foreach (var item in expr) {
                        if (item != null) {
                            Debug.Log("var " + item);    
                            Debug.Log("colunas " + item + "," + convertColunas[item]);
                        }
                    }
                    line = file.ReadLine(); //pega num de linhas da tabela
                    numLinhas = convert(line[0]);
                    Debug.Log(numLinhas);
                    Debug.Log(expr);
                    double TotalDeLinhas = Math.Pow(2, Convert.ToDouble(numLinhas));
                    Debug.Log("Linhas da tabela");
                    for (int i = 0; i < TotalDeLinhas; i++) { //a partir da conta do total de linhas pega tdas as linhas
                        Debug.Log(line = file.ReadLine());
                        Debug.Log("primeiro item da coluna " + line[0]);
                    }
                    file.ReadLine(); //lê o espaço entre a tabela e as respostas
                    line = file.ReadLine();
                    resp = splitString(line); //separa o vetor de respostas p associar aos inputs
                    foreach (var item in resp) {
                        if (item != null) {
                            Debug.Log(item);
                        }
                    }
                    counter++;
                    break;
                }
            }
        }
        if ((line = file.ReadLine()) == null) {
            Debug.Log("não tem prox tabela");
        }
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
