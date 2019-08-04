using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class setAddTables : MonoBehaviour { 
    public Text[] colunas;
    public Text expressao;
    public InputField inputPrefab;
    public InputField[] inputs;
    public GameObject endGame;
    protected string pathTable = "Assets/NewAdds/tabelaVerdade.txt", line, resps;
    protected string[] resp, expr;
    private int tableNumber = 0, numLinhas, count = 0, vars = 0;
    private Hashtable convertColunas = new Hashtable();
    private Transform ResultadoTransform;
    private int[] acertos;
    static int counter = 0, certo;
    
  //  private GameObject inputScript;
    void Start() { // Ler o arquivo e pegar a tabela - setar o ponteiro no arquivo pra ler a prox tabela 
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
        //transform p/ setar inputs como Childs de Respostas na hierarquia 
        ResultadoTransform = GameObject.FindWithTag("Respostas").transform;
        //inputs =
    }
    public void setaTabela() {
        System.IO.StreamReader file = new System.IO.StreamReader(pathTable);
        for (int i = 0; i < colunas.Length; i++) {   
            colunas[i].text = "";
        }
        while ((line = file.ReadLine()) != null)  {
            if (line == "NOVA TABELA VERDADE" ) { //verifica se é um tabela
                line = file.ReadLine();
                tableNumber = convert(line[0]); // pega o numero dessa tabela e convert p int
                if (tableNumber == counter) { //instruçoes de leitura aqui
                    Debug.Log("Tabela " + tableNumber);
                    line = file.ReadLine(); //pega expressao
                    expressao.text = line;
                    expr = splitString(line); //separa expr pra associar uma coluna da tabela a uma variavel
                    line = file.ReadLine(); //pega num de linhas da tabela
                    numLinhas = convert(line[0]);
                    Debug.Log(numLinhas);
                    Debug.Log(expr);
                    double TotalDeLinhas = Math.Pow(2, Convert.ToDouble(numLinhas));
                    acertos = new int[Convert.ToInt32(TotalDeLinhas)];
                    Debug.Log("Linhas da tabela");
                    for (int i = 0; i < TotalDeLinhas; i++) { //a partir da conta do total de linhas pega tdas as linhas
                        line = file.ReadLine(); //linha da tabela
                        inputs[i] = Instantiate(inputPrefab, new Vector3(0, 194.7f - (46f * i), 0), Quaternion.identity); //seta novo input em sua coordenada
                        inputs[i].transform.SetParent(ResultadoTransform, false); //seta como child de Resultados
                        setId inputScript = inputs[i].GetComponent<setId>();
                        inputScript.Id = i;
                        Debug.Log("NUMB DE VARS " + vars);
                        Debug.Log("numLinhas " + numLinhas);
                        if (vars > numLinhas) {
                            //comandos aqui
                            Debug.Log("to aqui vars > numlinhas");
                        }else {
                            foreach (var item in expr) { //associa cada coluna da tabela a uma variavel e printa na tela
                                if (item != null) {
                                    //Debug.Log("ITEM aqui = " + item);
                                    //Debug.Log("+COUNT VALUE " + count + "i value " + i);
                                    if (item.Contains("-")) {
                                        //Debug.Log("ITEM2 = " + item + "code: " + convertColunas[item].GetHashCode());
                                        //Debug.Log("-COUNT VALUE" + count);
                                        colunas[(convertColunas[item].GetHashCode() - 5)].text += line[count] + "\n"; //escreve a linha da tabela no postivo
                                        if (line[count] == 'V') { // nega a linha da tabela e escreve no negativo    
                                            colunas[convertColunas[item].GetHashCode()].text += 'F' + "\n";
                                        }else {
                                            colunas[convertColunas[item].GetHashCode()].text += 'V' + "\n";
                                        }
                                        count += 2;
                                    }
                                    else {
                                        colunas[convertColunas[item].GetHashCode()].text += line[count] + "\n";
                                        count += 2;
                                    }
                                }
                            }
                            count = 0;
                        }
                        
                    }
                    file.ReadLine(); //lê o espaço entre a tabela e as respostas
                    resps = file.ReadLine();
                    resp = splitString(resps); //separa o vetor de respostas p associar aos inputs
                    counter++;
                    break;
                }
            }
        }
        if ((line = file.ReadLine()) == null) {
            Debug.Log("não tem prox tabela");
        }
    }
    public int convert(char numVar) {
        int convertido = (int)Char.GetNumericValue(numVar);
        return convertido;
    }

    public String[] splitString(string expr) { //separa a string a partir dos operadores - get variaveis
        int k = 0;
        vars = 0;
        string[] retornaArray = new String[32];
        string[] multiArray = expr.Split(new Char[] { ' ', '^', '|', '(', ')', '[', ']' });
        foreach (string author in multiArray) {
            if (author.Trim() != "") {
                retornaArray[k] = author;
                k++;
            }
        }
        vars = k;
        return retornaArray;
    }

    public void verificaInput(InputField input) {
        setId inputAtual = input.GetComponent<setId>();
        Debug.Log("MEU ID É = " + inputAtual.Id);
        // Debug.Log("entrada: " + inputs[inputAtual.Id].text); Debug.Log("reposta: " + resp[inputAtual.Id]); 
        if (resp[inputAtual.Id].Equals(inputs[inputAtual.Id].text)) {
            //Debug.Log("resposta certa 1");
            inputs[inputAtual.Id].image.color = new Color32(69, 202, 35, 255);
            acertos[inputAtual.Id] = 1;
        }else {
            //Debug.Log("resposta errada");
            inputs[inputAtual.Id].image.color = new Color32(202, 41, 49, 255);
            acertos[inputAtual.Id] = 0;
        }
        verficaAcertos();
    }

    public void verficaAcertos() {
        certo = 0;
        for (int i = 0; i <= acertos.Length; i++) {
            Debug.Log("indice = " + i);
            if (acertos[i] == 1) {
                certo += 1;
                Debug.Log("acerto: " + certo);
                if (certo == acertos.Length)
                {
                    Debug.Log("fim de jogo");
                    endGame.SetActive(true);
                }
            }else {
                Debug.Log("Ainda não acabou");
                break;
            }
        }
    }

}
