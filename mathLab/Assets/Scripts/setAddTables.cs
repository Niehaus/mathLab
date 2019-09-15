using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class setAddTables : MonoBehaviour { 
    public Text[] colunas;
    public Text expressao,time, userTable;
    public InputField inputPrefab, userInput;
    public InputField[] inputs;
    public GameObject endGame, endFile;
    protected string pathTable = "/Assets/NewAdds/tabelaVerdade.txt", line, resps;
    protected string[] resp, expr, exprComposta, expr2;
    private int tableNumber = 0, numLinhas, count = 0, vars = 0;
    private Hashtable convertColunas = new Hashtable();
    private Transform ResultadoTransform;
    private int[] acertos;
    static float[] tempos = new float[32];
    static string[] users = new String[32];
    public static int counter = 0, certo, userCount = 0;
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
        string path = Directory.GetCurrentDirectory();
        pathTable = path + pathTable;
        setaTabela();
    }
    public void setaTabela() {
        System.IO.StreamReader file = new System.IO.StreamReader(pathTable);
        for (int i = 0; i < colunas.Length; i++) {   
            colunas[i].text = "";
        }
        while ((line = file.ReadLine()) != null)  {
            if (endGame) {
                endGame.SetActive(false);
            }
            Debug.Log("oque?" + line);
            if (line == "NOVA TABELA VERDADE" ) { //verifica se é um tabela
                Debug.Log("aqui" + line);
                line = file.ReadLine();
                Debug.Log("aqui tb?" + line);
                tableNumber = convert(line[0]); // pega o numero dessa tabela e convert p int
                Debug.Log("num = " + tableNumber + "contador = " + counter);
                if (tableNumber == counter) { //instruçoes de leitura aqui
                    Debug.Log("Tabela " + tableNumber);
                    line = file.ReadLine(); //pega expressao
                    expressao.text = line;
                    expr = splitString(line); //separa expr pra associar uma coluna da tabela a uma variavel
                    line = file.ReadLine(); //pega num de linhas da tabela
                    numLinhas = convert(line[0]);//Debug.Log(numLinhas); Debug.Log(expr);
                    double TotalDeLinhas = Math.Pow(2, Convert.ToDouble(numLinhas));
                    acertos = new int[Convert.ToInt32(TotalDeLinhas)];
                    Debug.Log("Linhas da tabela");
                    for (int i = 0; i < TotalDeLinhas; i++) { //a partir da conta do total de linhas pega tdas as linhas
                        line = file.ReadLine(); //linha da tabela
                        inputs[i] = Instantiate(inputPrefab, new Vector3(0, 191.1f - (46f * i), 0), Quaternion.identity); //seta novo input em sua coordenada
                        inputs[i].transform.SetParent(ResultadoTransform, false); //seta como child de Resultados
                        setId inputScript = inputs[i].GetComponent<setId>(); //busca script pra setar o id de cada input
                        inputScript.Id = i;
                        if (vars > numLinhas) {
                            Debug.Log("to aqui vars > numlinhas");
                            expr2 = knowWhatIterate(expr,i);
                            foreach (var item in expr2) {
                                if (item.Contains("-")) {
                                    colunas[(convertColunas[item].GetHashCode() - 5)].text += line[count] + "\n"; //escreve a linha da tabela no postivo
                                    if (line[count] == 'V') { // nega a linha da tabela e escreve no negativo    
                                        colunas[convertColunas[item].GetHashCode()].text += 'F' + "\n";
                                    }else {
                                        colunas[convertColunas[item].GetHashCode()].text += 'V' + "\n";
                                    }
                                    count += 2;
                                }else{
                                    colunas[convertColunas[item].GetHashCode()].text += line[count] + "\n";
                                    count += 2;
                                }  
                            }
                            count = 0;
                        }else {
                            foreach (var item in expr) { //associa cada coluna da tabela a uma variavel e printa na tela
                                if (item != null) {
                                    if (item.Contains("-")) {
                                        colunas[(convertColunas[item].GetHashCode() - 5)].text += line[count] + "\n"; //escreve a linha da tabela no postivo
                                        if (line[count] == 'V') { // nega a linha da tabela e escreve no negativo    
                                            colunas[convertColunas[item].GetHashCode()].text += 'F' + "\n";
                                        }else {
                                            colunas[convertColunas[item].GetHashCode()].text += 'V' + "\n";
                                        }
                                        count += 2;
                                    }else {
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
            if ((line = file.ReadLine()) == null) { //vai para algum lugar, fim do arquivo de tabelas**
                timeScript timer = time.GetComponent<timeScript>();
                tempos[userCount] = timer.endGame();
                endFile.SetActive(true);
                Debug.Log("não tem mais tabela");
                file.DiscardBufferedData();
                line = file.ReadLine();
                Debug.Log(line);
               // file.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
            }
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

    private int findMyIndex(string[] expr, string item){ //encontra o index do item procurado 
        int index = 0;
        foreach (var it in expr) {
                if (it == item && index != 0) {
                    return index;
                }else if(it == item){
                    return index;
                }else if(index < Math.Pow(2,numLinhas)){
                    index++;
                }
        }
        return index;
    }
    public String[] knowWhatIterate(string[] expr, int exec){ //retira as particulas repetidas de uma expr
        if (exec == 0) {
            exprComposta = new String[numLinhas];
            string[] aux = new String[1];
            int count = 0;
            for (int i = 0; i < numLinhas; i++) {
                foreach (var item in expr) {
                    if (item != null) { //iterar apenas em itens não nulos
                        if (exprComposta[i] == null) {
                            if (item.Contains("-")) {
                                aux = item.Split('-');
                                count = findMyIndex(expr,aux[1]);
                                expr[count] = null;
                            }
                            exprComposta[i] = item;
                            count = findMyIndex(expr,item);
                            expr[count] = null;
                            foreach (var it3 in expr) {
                                if (it3 == exprComposta[i]) {
                                    count = findMyIndex(expr,it3);
                                    expr[count] = null;
                                }
                            }
                        }else {
                            if (item.Contains("-")) {
                                aux[0] = "-" + exprComposta[i];
                                if (item == aux[0]) {
                                    exprComposta[i] = item;
                                    count = findMyIndex(expr,item);
                                    expr[count] = null;
                                }
                            }
                        }
                    }
                }
            }
            return exprComposta;   
        }else {
            return exprComposta;
        }
    }

    public void verificaInput(InputField input) {
        setId inputAtual = input.GetComponent<setId>();
        Debug.Log("Meu ID é = " + inputAtual.Id); 
        //Debug.Log("entrada: " + inputs[inputAtual.Id].text); Debug.Log("reposta: " + resp[inputAtual.Id]);  
        //CONVERTER TODA A ENTRADA P MINUSCULO
        if (resp[inputAtual.Id].Equals(inputs[inputAtual.Id].text)) {
            Debug.Log("resposta certa");
            //inputs[inputAtual.Id].image.color = new Color32(69, 202, 35, 255); //cor verde
            acertos[inputAtual.Id] = 1;
        }else {
            Debug.Log("resposta errada");
            //inputs[inputAtual.Id].image.color = new Color32(202, 41, 49, 255);//cor vermelha
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
                if (certo == acertos.Length) {
                    Debug.Log("fim de jogo");
                    endGame.SetActive(true);
                    for (int j = 0; j < inputs.Length - 1; j++) {
                        inputs[j].image.color = new Color32(69, 202, 35, 255); //cor verde
                    }   
                }
            }else {
                Debug.Log("Ainda não acabou");
                break;
            }
        }
    }
    public void keepUser(){
         users[userCount] = userInput.text;
         userInput.text = "";
         //userTable.text += users[userCount] + "\t\t\t\t" + tempos[userCount] + "segundos" + "\n";
         userTable.text += String.Format("{0,-12}{1,24}\n",users[userCount],tempos[userCount] + " segundos");
    }
}
