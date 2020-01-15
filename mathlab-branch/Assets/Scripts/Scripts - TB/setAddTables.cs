using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class SetAddTables : MonoBehaviour { 
    public Text[] colunas;
    public Text expressao, time, userTable;
    public InputField inputPrefab, userInput;
    public InputField[] inputs;
    public GameObject endGame, endFile, textIni;
    public GameObject[] buttons;
    protected string pathTable = "/Assets/NewAdds/grupo1.txt", line, resps;
    protected string[] resp, expr, exprComposta, expr2;
    private int _tableNumber = 0, _numLinhas, _count = 0, _vars = 0;
    private Hashtable _convertColunas = new Hashtable();
    private Transform _resultadoTransform;
    private int[] _acertos;
    static string[] _tempos = new String[32];
    static string[] _users = new String[32];
    public static int counter = 0, certo, userCount = 0;
    protected string  path;
    public GameMasterTable manager;
    void Start() { // Ler o arquivo e pegar a tabela - setar o ponteiro no arquivo pra ler a prox tabela 
        //colunas positivas
        _convertColunas.Add("p",0);
        _convertColunas.Add("q",1);
        _convertColunas.Add("r",2);
        _convertColunas.Add("s",3);
        _convertColunas.Add("t",4);
        //colunas negadas
        _convertColunas.Add("-p", 5);
        _convertColunas.Add("-q", 6);
        _convertColunas.Add("-r", 7);
        _convertColunas.Add("-s", 8);
        _convertColunas.Add("-t", 9);
        path = Directory.GetCurrentDirectory(); //pega diretorio do arquivo
        pathTable = path + pathTable; //completa o caminho até o arquivo escolhido
        _resultadoTransform = GameObject.FindWithTag("Respostas").transform;
        SetaTabela();
    }

    public void SetaTabela() {
        System.IO.StreamReader file = new System.IO.StreamReader(pathTable);
        for (int i = 0; i < colunas.Length; i++) {   
            colunas[i].text = "";
        }
        while ((line = file.ReadLine()) != null)  {
            if (endGame) {
                endGame.SetActive(false);
            }
           // Debug.Log("oque?" + line);
            if (line == "NOVA TABELA VERDADE" ) { //verifica se é um tabela
                line = file.ReadLine();
                _tableNumber = Convert(line[0]); // pega o numero dessa tabela e convert p int
                if (_tableNumber == counter) { //instruçoes de leitura aqui
                   // Debug.Log("Tabela " + tableNumber);
                    line = file.ReadLine(); //pega expressao
                    expressao.text = line;
                    expr = SplitString(line); //separa expr pra associar uma coluna da tabela a uma variavel
                    line = file.ReadLine(); //pega num de linhas da tabela
                    _numLinhas = Convert(line[0]);//Debug.Log(numLinhas); Debug.Log(expr);
                    double totalDeLinhas = Math.Pow(2, System.Convert.ToDouble(_numLinhas));
                    _acertos = new int[System.Convert.ToInt32(totalDeLinhas)];
                    Debug.Log(totalDeLinhas);
                    for (int i = 0; i < totalDeLinhas; i++) { //a partir da conta do total de linhas pega tdas as linhas
                        line = file.ReadLine(); //linha da tabela
                        inputs[i] = Instantiate(inputPrefab, new Vector3(0, -75.5f - (85f * i), 0), Quaternion.identity); //seta novo input em sua coordenada
                        inputs[i].transform.SetParent(_resultadoTransform, false); //seta como child de Resultados
                        SetId inputScript = inputs[i].GetComponent<SetId>(); //busca script pra setar o id de cada input
                        inputScript.Id = i;
                        inputs[i].characterLimit = 1; //seta limite de caracteres como apenas 1
                        if (_vars > _numLinhas) {
                            expr2 = KnowWhatIterate(expr,i);
                            foreach (var item in expr2) {           
                                if (item.Contains("-")) {
                                    colunas[(_convertColunas[item].GetHashCode() - 5)].text += line[_count] + "\n"; //escreve a linha da tabela no postivo
                                    if (line[_count] == 'V') { // nega a linha da tabela e escreve no negativo    
                                        colunas[_convertColunas[item].GetHashCode()].text += 'F' + "\n";
                                    }else {
                                        colunas[_convertColunas[item].GetHashCode()].text += 'V' + "\n";
                                    }
                                    _count += 2;
                                }else{
                                    colunas[_convertColunas[item].GetHashCode()].text += line[_count] + "\n";
                                    _count += 2;
                                } 
                            }
                            _count = 0;
                        }else {
                            foreach (var item in expr) { //associa cada coluna da tabela a uma variavel e printa na tela
                                if (item != null) {
                                    if (item.Contains("-")) {
                                        colunas[(_convertColunas[item].GetHashCode() - 5)].text += line[_count] + "\n"; //escreve a linha da tabela no postivo
                                        if (line[_count] == 'V') { // nega a linha da tabela e escreve no negativo    
                                            colunas[_convertColunas[item].GetHashCode()].text += 'F' + "\n";
                                        }else {
                                            colunas[_convertColunas[item].GetHashCode()].text += 'V' + "\n";
                                        }
                                        _count += 2;
                                    }else {
                                        colunas[_convertColunas[item].GetHashCode()].text += line[_count] + "\n";
                                        _count += 2;
                                    }
                                }
                            }
                            _count = 0;
                        }  
                    }
                    file.ReadLine(); //lê o espaço entre a tabela e as respostas
                    resps = file.ReadLine();
                    resp = SplitString(resps); //separa o vetor de respostas p associar aos inputs
                    counter++;
                    break;
                }
            }
            if ((line = file.ReadLine()) == null) { //vai para algum lugar, fim do arquivo de tabelas**
                TimeScript timer = time.GetComponent<TimeScript>();
                _tempos[userCount] = timer.EndGame();// Debug.Log("atualiza text");
                for (int i = 0; i < _tempos.Length; i++) {
                    if (_users[i] != null) { //Debug.Log("USERS " + users[i] + "TEMPOS " + tempos[i]);
                        userTable.text += String.Format("{0,-12}{1,24}\n", _users[i], _tempos[i] + " segundos");
                    }
                }
                endFile.SetActive(true); //Debug.Log("não tem mais tabela");
                file.DiscardBufferedData();
                //line = file.ReadLine();
                //path = "";
                //pathTable = "/Assets/NewAdds/";
               // file.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
            }
        }
    }
    public int Convert(char numVar) {
        int convertido = (int)Char.GetNumericValue(numVar);
        return convertido;
    }

    public String[] SplitString(string expr) { //separa a string a partir dos operadores - get variaveis
        int k = 0;
        _vars = 0;
        string[] retornaArray = new String[32];
        string[] multiArray = expr.Split(new Char[] { ' ', '^', '|', '(', ')', '[', ']'});
        foreach (string author in multiArray) {
            if (author.Trim() != "") {
                retornaArray[k] = author;
                k++;
            }
        }
        _vars = k;
        for (int i = 0; i < retornaArray.Length; i++) {
            if (retornaArray[i] == ("->")) {//explit -> 
                retornaArray[i] = null;
            }
        }
        return retornaArray;
    }

    private int FindMyIndex(string[] expr, string item){ //encontra o index do Item procurado 
        int index = 0;
        foreach (var it in expr) {
                if (it == item && index != 0) {
                    return index;
                }else if(it == item){
                    return index;
                }else if(index < Math.Pow(2,_numLinhas)){
                    index++;
                }
        }
        return index;
    }
    public String[] KnowWhatIterate(string[] expr, int exec){ //retira as particulas repetidas de uma expr
        if (exec == 0) {
            exprComposta = new String[_numLinhas];
            string[] aux = new String[1];
            int count = 0;
            for (int i = 0; i < _numLinhas; i++) {
                foreach (var item in expr) {
                    if (item != null) { //iterar apenas em itens não nulos
                        if (exprComposta[i] == null) {
                            if (item.Contains("-")) {        
                                aux = item.Split('-');
                                count = FindMyIndex(expr,aux[1]);
                                expr[count] = null;         
                            }
                            exprComposta[i] = item;
                            count = FindMyIndex(expr,item);
                            expr[count] = null;
                            foreach (var it3 in expr) {
                                if (it3 == exprComposta[i]) {
                                    count = FindMyIndex(expr,it3);
                                    expr[count] = null;
                                }
                            }
                        }else {
                            if (item.Contains("-")) {
                                aux[0] = "-" + exprComposta[i];
                                if (item == aux[0]) {
                                    exprComposta[i] = item;
                                    count = FindMyIndex(expr,item);
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

    public void VerificaInput(InputField input) { 
        SetId inputAtual = input.GetComponent<SetId>();
        Debug.Log("Meu ID é = " + inputAtual.Id);
        Debug.Log("entrada: " + inputs[inputAtual.Id].text); 
        Debug.Log("reposta: " + resp[inputAtual.Id]);  
        //TODO: CONVERTER TODA A ENTRADA P MINUSCULO
        if (resp[inputAtual.Id].Equals(inputs[inputAtual.Id].text)) {
            //Debug.Log("resposta certa");
            //inputs[inputAtual.Id].image.color = new Color32(69, 202, 35, 255); //cor verde
            _acertos[inputAtual.Id] = 1;
        }else {
            //Debug.Log("resposta errada");
           // inputs[inputAtual.Id].image.color = new Color32(202, 41, 49, 255);//cor vermelha
            _acertos[inputAtual.Id] = 0;
        }
        VerficaAcertos();
    }

    public void VerficaAcertos() {
        certo = 0;
        for (int i = 0; i <= _acertos.Length - 1; i++) {  //Debug.Log("indice = " + i);
            if (_acertos[i] == 1) {
                certo += 1;//Debug.Log("acerto: " + certo); 
                if (certo == _acertos.Length) { // Debug.Log("fim de jogo");
                    endGame.SetActive(true);
                    for (int j = 0; j < inputs.Length; j++) 
                        inputs[j].image.color = new Color32(69, 202, 35, 255); //cor verde
                }
            }else { //Debug.Log("Ainda não acabou");
                break;
            }
        }
    }
    public void KeepUser() { //cadastra usuario
        _users[userCount] = userInput.text;
        userInput.text = "";
        userTable.text += String.Format("{0,-12}{1,24}\n",_users[userCount],_tempos[userCount] + " segundos");
        userCount++;
    }

    public void SelectArquivo(string nomeDoArquivo) {
        path = Directory.GetCurrentDirectory(); //pega diretorio do arquivo
        pathTable = path + pathTable + nomeDoArquivo; //completa o caminho até o arquivo escolhido
        textIni.SetActive(false);
        for (int i = 0; i < buttons.Length; i++) //desativa os botões
            buttons[i].SetActive(false);
        manager.tablePanel.SetActive(true);
        _resultadoTransform = GameObject.FindWithTag("Respostas").transform; //transform p/ setar inputs como Childs de Respostas na hierarquia 
        SetaTabela(); //chama função principal
    }

    public void ProximaTabela() {
        SetaTabela();
    }
}
