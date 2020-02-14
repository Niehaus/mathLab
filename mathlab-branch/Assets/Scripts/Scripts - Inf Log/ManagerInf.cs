using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagerInf : MonoBehaviour {

    public Alavanca[] alavancas;
    public Text[] textoResposta;
    public Text[] contadores;
    public Text[] textPainelDerrota;
    public Text[] textPainelVitoria;
    public Text comandos;
    //public Text dropoDownText;
    private int _hearts = 3;
    private int _coins;
    private string[] _stringSeparators = { "\r\n" };
    private string[] _lines;
    private int respostaAtual;
    private int index;
    private float _currCountdownValue;
    private bool _fimDoJogo;
    private static readonly int Ativa = Animator.StringToHash("ativa");
    private static readonly int MoveX = Animator.StringToHash("moveX");
    private static readonly int MoveY = Animator.StringToHash("moveY");
    private PlayerController _disablekey; //bloqueador de teclado enquanto NPC fala

    public GameObject painelVitoria;
    public GameObject painelDerrota;
    public GameObject painelInicial;
    public Cronometro cronometro;
    public Dropdown dropdown;
    private bool _controller;
    private int _totalDePontos;
    
    // Start is called before the first frame update
    private void Start() {
        contadores[0].text = _coins + "x";
        contadores[1].text = _hearts + "x";
        _disablekey = FindObjectOfType<PlayerController>();
        var textFile = Resources.Load<TextAsset>("Data/infLog");
        if (textFile == null) {
            Debug.Log("Arquivo Texto Não Está Aqui");
            return;
        }
        _lines = textFile.text.Split(_stringSeparators, StringSplitOptions.None);
        _disablekey.keyboardAble = false;
    }

    private void Update() {
        if (_fimDoJogo && _hearts <= 0) {
            //TODO: PAINEL DE DERROTA
            painelDerrota.SetActive(true);
            _disablekey.keyboardAble = false;
            _disablekey.GetComponent<Rigidbody2D>().velocity = new Vector2(0f,0f);
            _disablekey.GetComponent<Animator>().SetFloat(MoveX, 0f);
            _disablekey.GetComponent<Animator>().SetFloat(MoveY, 0f);
            _totalDePontos = _coins / 10 + _coins;
            textPainelDerrota[0].text = "Moedas Coletadas:" + _coins + "x";
            textPainelDerrota[1].text = "Total de Pontos:" + _totalDePontos + "x"; 
        } else if (_fimDoJogo && _hearts > 0) {
            //TODO: PAINEL DE VITORIA
            painelVitoria.SetActive(true);
            _disablekey.keyboardAble = false;
            _disablekey.GetComponent<Rigidbody2D>().velocity = new Vector2(0f,0f);
            _disablekey.GetComponent<Animator>().SetFloat(MoveX, 0f);
            _disablekey.GetComponent<Animator>().SetFloat(MoveY, 0f);
            _totalDePontos = (_coins / 10 + _hearts) + _coins;
            textPainelVitoria[0].text = "Moedas Coletadas:" + _coins + "x";
            textPainelVitoria[1].text = "Vidas Restantes:" + _hearts + "x";
            textPainelVitoria[2].text = "Total de Pontos:" + _totalDePontos + "x";
        }
    }

    private void IniciaRodada() {
        SetaDesafio();
        cronometro.timerActive = true;
    }

    private void RepeteRodada() {
        cronometro.timerActive = true;
    }
  
    public void VerificaCanhoes() { //seta animações tempos etc
        //TODO: ATIVAR ANIMAÇÃO DO CANHAO ATIRANDO
        var alavancasAtivas = 0;
        foreach (var alavanca in alavancas) {
            if (alavanca.GetComponent<Animator>().GetBool(id: Ativa)) {
                alavancasAtivas += 1;
            }
        }
        if (alavancasAtivas == 0) {
            _hearts -= 1;
            contadores[1].text = _hearts + "x";
            if (_hearts <= 0) {
                _fimDoJogo = true;
            }
            RepeteRodada();
        }
        
        foreach (var alavanca in alavancas) {
            if (alavanca.GetComponent<Animator>().GetBool(id: Ativa)) {
                Debug.Log(message: "Alavanca " + alavanca.identificador + " ativa");
                alavanca.canhoes.AtivaCanhao();
                if (respostaAtual == alavanca.identificador) {
                    Debug.Log("Ativou a alavanca correta!");
                    _coins += 10;
                    contadores[0].text = _coins + "x";
                    if (index == _lines.Length) {
                        Debug.Log("ACABOU");
                        _fimDoJogo = true;
                    }
                    _controller = true;
                }
                else {
                    Debug.Log("Ativou a alavanca incorreta :(");
                    _hearts -= 1;
                    contadores[1].text = _hearts + "x";
                    if (_hearts <= 0) {
                        _fimDoJogo = true;
                        ResetaAlavancas();
                    }
                    _controller = false;
                }
            }
        }
        ProximaAcao(_controller);
    }

    private void ProximaAcao(bool comando) {
        ResetaAlavancas();
        if (comando) {
            IniciaRodada();
        }
        else {
            RepeteRodada();
        }
    }
    private void SetaDesafio() {
        Debug.Log("fui chamda");
        comandos.text = _lines[index];
        index += 1;
        for (var i = 0; i < textoResposta.Length; i++) {
            textoResposta[i].text = _lines[index + i];
        }
        index += 5;
        //respostaAtual = _lines[index];
        respostaAtual = AlavancaCorreta(_lines[index]);
        index += 1;
    }

    private int AlavancaCorreta(string respostaCorreta) {
        for (var i = 0; i < textoResposta.Length; i++) {
            if (textoResposta[i].text == respostaCorreta) {
                Debug.Log("Alavanca correta: " + (i + 1));
                return i + 1;
            }
        }
        return 0;
    }

    private void ResetaAlavancas() {
        foreach (var alavanca in alavancas) {
            alavanca.GetComponent<Animator>().SetBool(Ativa, false);
        }
        cronometro.timeStart = 12f;
    }

    public void Play() {
        _disablekey.keyboardAble = true;
        painelInicial.SetActive(false);
        IniciaRodada();
    }

    public void JogarNovamente() {
        SceneManager.LoadScene("Jogo Inf Logica");
    }

    public void MudaTexto(Text text) {
        switch (dropdown.value) {
            case 0:
                text.text = "Nenhuma Função Selecionada";
                break;
            case 1:
                text.text = "modus tolens";
                break;
            case 2:
                text.text = "modus ponens";
                break;
            case 3:
                text.text = "silogismo";
                break;
        }
        
    }
    
}
