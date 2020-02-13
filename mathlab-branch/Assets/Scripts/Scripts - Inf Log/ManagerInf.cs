using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerInf : MonoBehaviour {

    public Alavanca[] alavancas;
    public Text[] textoResposta;
    public Text[] contadores;
    public Text[] textPainelDerrota;
    public Text[] textPainelVitoria;
    public Text comandos;
    public Text timer;
    private int _hearts = 3;
    private int _coins;
    private string[] _stringSeparators = { "\r\n" };
    private string[] _lines;
    private int respostaAtual;
    private int index;
    private float _currCountdownValue;
    private bool _fimDoJogo;
    private static readonly int Ativa = Animator.StringToHash("ativa");
    private PlayerController _disablekey; //bloqueador de teclado enquanto NPC fala

    public GameObject painelVitoria;
    public GameObject painelDerrota;
    public GameObject painelInicial;

    private bool _controller;
    
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
        //IniciaRodada();
    }

    private void Update() {
        if (_fimDoJogo && _hearts <= 0) {
            //TODO: PAINEL DE DERROTA
            painelDerrota.SetActive(true);
            _disablekey.keyboardAble = false;
            textPainelDerrota[0].text = "Moedas Coletadas:" + _coins + "x";
            textPainelDerrota[1].text = "Total de Pontos:" + _coins + "x"; //TODO: CALCULAR PONTOS
        } else if (_fimDoJogo && _hearts > 0) {
            //TODO: PAINEL DE VITORIA
            painelVitoria.SetActive(true);
            _disablekey.keyboardAble = false;
            textPainelVitoria[0].text = "Moedas Coletadas:" + _coins + "x";
            textPainelVitoria[1].text = "Vidas Restantes:" + _hearts + "x";
            textPainelVitoria[2].text = "Total de Pontos:" + _coins + "x"; //TODO: CALCULAR PONTOS
        }
    }

    private void IniciaRodada() {
        SetaDesafio();
        StartCoroutine(StartTimer(12f));
    }

    private void RepeteRodada() {
        StartCoroutine(StartTimer(12f));
    }
    
    private IEnumerator VerificaCanhoes() { //seta animações tempos etc
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
                ResetaAlavancas();
            }
            RepeteRodada();
            _controller = false;
        }
        yield return new WaitForSeconds(seconds: 1);
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
                    ResetaAlavancas();
                    IniciaRodada();
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
                    RepeteRodada();
                    _controller = false;
                }
            }
        }
    }

    private IEnumerator StartTimer(float countdownValue) {
        //Debug.Log("Começa Timer");
        timer.color = Color.white;
        _currCountdownValue = countdownValue;
        while (_currCountdownValue >= 0) {
            //Debug.Log("Countdown: " + _currCountdownValue);
            timer.text = _currCountdownValue.ToString("0");
            yield return new WaitForSeconds(1.0f);
            _currCountdownValue--;
            Debug.Log("estou aqui");
            if (_currCountdownValue == 0) {
                timer.color = new Color(0.79f, 0.16f, 0.19f);
                StartCoroutine(VerificaCanhoes());
            }
        }
    }

    private void SetaDesafio() {
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

        respostaAtual = 10;
    }

    public void Play() {
        _disablekey.keyboardAble = true;
        painelInicial.SetActive(false);
        IniciaRodada();
    }
    
}
