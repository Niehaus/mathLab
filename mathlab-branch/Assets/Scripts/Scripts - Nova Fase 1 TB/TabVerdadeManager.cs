using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TabVerdadeManager : MonoBehaviour {

    public Text expressaoText;
    public Text logLinhas;
    public Switch[] switchVector;
    public UiManager uiManager;
    public AudioClip[] audioClips;
    public bool alreadyEnd;
    private TextAsset[] _textFiles;
    private int _vars, _totalLinhas = -1;
    private List<Tuple<string, string>> _tuplaDeLinhasRespostas = new List<Tuple<string, string>>();
    private List<Tuple<string, string>> _logRespostas = new List<Tuple<string, string>>();
    private List<Switch> _switchesValidos = new List<Switch>();
    private string _currentFile;

    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start() {
        /*_textFiles = Resources.LoadAll("TabVerdade", typeof(TextAsset)).Cast<TextAsset>().ToArray();
        foreach (var textAsset in _textFiles) {
            IniciaEtapa(textAsset);
            
        }*/
    }

    // Update is called once per frame
    void Update() {
        if (_logRespostas.Count == _totalLinhas && !alreadyEnd ) { //Procedimento de fim de jogo
            alreadyEnd = true;
            _totalLinhas = -1;
            _logRespostas.Clear();
            foreach (var @switch in switchVector) {
                @switch.ConjuntoSwitchAction("colorful", false, true);
                @switch.switchAtivo = true;
            }
            uiManager.TabelPanelFinaliza(_currentFile, alreadyEnd);
        } 
    }

    public void IniciaEtapa(TextAsset textAsset) {
        Debug.Log("arquivo selecionado: " + textAsset.name);
        var infoTable = GetLinesFromTable(textAsset);
        _currentFile = textAsset.name;
        expressaoText.text = infoTable.Item2;
        _switchesValidos =  ValidaSwitches(infoTable.Item2);
        for (var i = 0; i < infoTable.Item1.Length; i++) {
            _tuplaDeLinhasRespostas.Add(LinhasRespostas(infoTable.Item1[i], infoTable.Item3[i]));
        }
    }

    private Tuple<string, string> LinhasRespostas(string linha, string resposta) {
        return new Tuple<string, string>(linha, resposta);
    }
    
    /* Valida apenas os switches que estão contidos
    * na expressão avaliada naquele momento
    */
    private List<Switch> ValidaSwitches(string expressao) { 
        var expressaoSeparada = SplitString(expressao);
        var switchesValidos  = new List<Switch>();
        
        foreach (var @switch in switchVector) {
            if (!expressaoSeparada.Contains(@switch.variavelRef.text.ToLower())  && @switch.variavelRef.text != "Resposta") {
                @switch.switchAtivo = false;
                @switch.ConjuntoSwitchAction("colorful", true, false);
                
                //TODO: desabilitar switch anim
            }
            else {
                switchesValidos.Add(@switch);   
            }
        }
        return switchesValidos;
    }

    private String[] SplitString(string expr) { //separa a string a partir dos operadores - get variaveis
        var k = 0;
        _vars = 0;
        var retornaArray = new String[32];
        var multiArray = expr.Split(new Char[] { ' ', '^', '|', '(', ')', '[', ']'});
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
    
    
    private Tuple<string[], string, string[]> GetLinesFromTable(TextAsset textAsset) {
        var tableLines = Regex.Split(textAsset.text, "\r\n|\r|\n");
        var expressao = tableLines[0]; var numeroLinhasTabela = tableLines[1];
        var vetorResposta = Regex.Split(tableLines[tableLines.Length - 1].ToUpper(), @"\s+");
        var linhasTabela = new string[_totalLinhas = (int) Math.Pow(2,int.Parse(numeroLinhasTabela))];

        for (var i = 2; i <  (int) Math.Pow(2,int.Parse(numeroLinhasTabela)) + 2; i++) {
            // Debug.Log("linha index " + i + " " +  tableLines[i]);
            linhasTabela[i - 2] = tableLines[i];
        }
        
        return new Tuple<string[], string, string[]>(linhasTabela, expressao, vetorResposta);
    }


    private Tuple<string, string> getRespostaJogador() {
        var playerLinha = "";
        foreach (var validSwitch in _switchesValidos) {
            if (validSwitch.variavelRef.text != "Resposta") playerLinha += validSwitch.textoTv.text;
        }
        var playerResposta = switchVector[4].textoTv.text;
        
        return new Tuple<string, string>(playerLinha, playerResposta);
    }
    
    
    private IEnumerator AnimEvent(string instrucao) { //seta animações tempos etc
        
        foreach (var @switch in _switchesValidos) {
            @switch.switchAtivo = false;
            @switch.ConjuntoSwitchAction(instrucao, true, false);
        }
        
        yield return new WaitForSeconds(2);
        
        foreach (var @switch in _switchesValidos) {
            @switch.switchAtivo = true;
            @switch.textoTv.text = "F";
            @switch.ConjuntoSwitchAction(instrucao, false, true);
        }
    }

    private void ReiniciaSwitches() {
        foreach (var @switch in _switchesValidos.Where(@switch => @switch.GetComponent<Animator>().GetBool("on"))) {
            @switch.GetComponent<Animator>().SetBool("on", false);
            @switch.textoTv.text = "F";
        }
    }
    
    public void PlayMySound(AudioClip audioClip) {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
    public void EnviarResposta() {
        var respostaJogador = getRespostaJogador();
        
        if (_logRespostas.Contains(respostaJogador)) { //Resposta Repetida
            StartCoroutine(AnimEvent("yellowScreen")); 
            ReiniciaSwitches();
            PlayMySound(audioClips[1]);      
            EventSystem.current.SetSelectedGameObject(null); //Linha que já foi respondida
            return;
        }
        
        if (_tuplaDeLinhasRespostas.Contains(respostaJogador)) { //Resposta Correta
            logLinhas.text += string.Format("{0,-2} - {1,-2} -> {2}\n", (_logRespostas.Count + 1), respostaJogador.Item1, respostaJogador.Item2);
            StartCoroutine(AnimEvent("greenScreen"));
            _logRespostas.Add(respostaJogador);
            ReiniciaSwitches();
            PlayMySound(audioClips[0]);
            EventSystem.current.SetSelectedGameObject(null);
            return;
        }
        
        StartCoroutine(AnimEvent("redScreen")); //Resposta Errada
        PlayMySound(audioClips[2]);
        ReiniciaSwitches();
        
        /*Faz com que nada mais esteja selecionado pelo
         *event system, serve para perder o selest do botao*/
        EventSystem.current.SetSelectedGameObject(null);
    }
}
