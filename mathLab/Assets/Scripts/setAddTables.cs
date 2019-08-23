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
    protected string[] resp, expr, exprComposta;
    private int tableNumber = 0, numLinhas, count = 0, vars = 0;
    private Hashtable convertColunas = new Hashtable();
    private Transform ResultadoTransform;
    private int[] acertos;
    static int counter = 0, certo;
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
            if (line == "NOVA TABELA VERDADE" ) { //verifica se é um tabela
                line = file.ReadLine();
                tableNumber = convert(line[0]); // pega o numero dessa tabela e convert p int
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
                        Debug.Log("NUMB DE VARS " + vars);
                        Debug.Log("numLinhas " + numLinhas);
                        if (vars > numLinhas) {
                            //comandos aqui
                            Debug.Log("to aqui vars > numlinhas");
                            knowWhatIterate(expr,i);
                           foreach (var item in expr) {
                               if (item != null) {
                                // Debug.Log("particula: " + item);
                               }
                           }
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
        if ((line = file.ReadLine()) == null) { //vai para algum lugar, fim do arquivo de tabelas**
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

    private int findMyIndex(string[] expr, string item){
        int index = 0;
        foreach (var it in expr) {
                if (it == item && index != 0) {
                    index++;
                    Debug.Log("ITEM PROCURADO " + item + " na pos " + index + " ENCONTRADO ");
                    return index;
                }else {
                    index++;
                }
        }//arrumar essa função pra pegar o index certo e provavelmente a solução vai funcionar
        return 0;
    }
    public String[] knowWhatIterate(string[] expr, int exec){
        if (exec == 0) {
        exprComposta = new String[numLinhas];
        string[] aux = new String[1];
        int count = 0;
        Debug.Log("TAM " + exprComposta.Length);
            foreach (var item in expr) {
                if (item != null)
                {
                    Debug.Log("particula: " + item);
                }
            }
        for (int i = 0; i < numLinhas; i++) {
            Debug.Log("POS INICIAL É " + i);
            foreach (var item in expr) {
                if (item != null) { //iterar apenas em itens não nulos
                Debug.Log("ITEM P/ ADC " + item);
                    if (exprComposta[i] == null) {
                        Debug.Log("vou adc " + item + " em " + i);
                        exprComposta[i] = item;
                        count = findMyIndex(expr,item);
                        expr[count] = null;
                        Debug.Log("RETIRANDO ITEM NA POSIÇÃO " + count);
                       // count++;  
                        foreach (var it in expr) {
                            if (it != null)
                                Debug.Log("EXPR COM O QUE SOBROU " + it);
                        }
                    }else {
                        Debug.Log("pos não nula " + item);
                        if (item.Contains("-")) {
                            Debug.Log("TENHO - ");
                            aux[0] = "-" + exprComposta[i];
                            Debug.Log("aux é " + aux[0]);
                            if (item == aux[0]) {
                                Debug.Log("substituindo " + exprComposta[i] + " por " + item);
                                exprComposta[i] = item;
                                count = findMyIndex(expr,item);
                                Debug.Log("RETIRAR OUTRO ITEM NA POSIÇÃO " + count);
                                expr[count] = null;
                                //count++;
                                foreach (var it in expr) {
                                    if (it != null)
                                        Debug.Log("EXPR COM novamente O QUE SOBROU " + it);
                                }
                            }else{
                               // count++
                            }
                        }
                    }
                }
            }
            Debug.Log("particula adc " + exprComposta[i] + " na pos " + i);
        }

        foreach (var it in exprComposta) {
                Debug.Log("vet final " + it);
        }
        return exprComposta;
            
        }else{
            Debug.Log("JA EXECUTEI UMA VEZ VIADO");
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
            inputs[inputAtual.Id].image.color = new Color32(69, 202, 35, 255); //cor verde
            acertos[inputAtual.Id] = 1;
        }else {
            Debug.Log("resposta errada");
            inputs[inputAtual.Id].image.color = new Color32(202, 41, 49, 255);//cor vermelha
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
                }
            }else {
                Debug.Log("Ainda não acabou");
                break;
            }
        }
    }

}
