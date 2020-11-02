using System;
using System.Collections;
using System.Collections.Generic;
using RestSupport;
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
    private int _respostaAtual;
    private int _index;
    private int _respostaBonus;
    private float _currCountdownValue;
    private bool _fimDoJogo;
    private static readonly int Ativa = Animator.StringToHash("ativa");
    private static readonly int MoveX = Animator.StringToHash("moveX");
    private static readonly int MoveY = Animator.StringToHash("moveY");
    private PlayerController _disablekey; //bloqueador de teclado enquanto NPC fala
    
    public AudioSource audioSource;
    public GameObject painelVitoria;
    public GameObject painelDerrota;
    public GameObject painelInicial;
    public GameObject painelRespCorreta;
    public AudioClip[] audioClips;
    public Cronometro cronometro;
    public Dropdown dropdown;
    public CadastroUser managerHTTP;
    private bool _controller;
    private int _totalDePontos;
    private bool _bonusAtivo;
    
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
            painelRespCorreta.SetActive(false);
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
            painelRespCorreta.SetActive(false);
            _disablekey.keyboardAble = false;
            _disablekey.GetComponent<Rigidbody2D>().velocity = new Vector2(0f,0f);
            _disablekey.GetComponent<Animator>().SetFloat(MoveX, 0f);
            _disablekey.GetComponent<Animator>().SetFloat(MoveY, 0f);
            _totalDePontos = (_coins / 10 + _hearts) + _coins;
            textPainelVitoria[0].text = "Moedas Coletadas:" + _coins + "x";
            textPainelVitoria[1].text = "Vidas Restantes:" + _hearts + "x";
            textPainelVitoria[2].text = "Total de Pontos:" + _totalDePontos + "x";
            ManagerGeral.totalPontosFase3 = _totalDePontos;
            ManagerGeral.faseFeita[2] = true;
            managerHTTP.AttUser(ManagerGeral.totalTempoFase1, ManagerGeral.totalPontosFase2, ManagerGeral.totalPontosFase3);
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
            _controller = false;
            contadores[1].text = _hearts + "x";
            StartCoroutine(PainelRespostaCorreta("Nenhuma Alavanca Ativa ):", audioClips[2]));
            if (_hearts <= 0) {
                _fimDoJogo = true;
            }
        }
        
        foreach (var alavanca in alavancas) {
            if (alavanca.GetComponent<Animator>().GetBool(id: Ativa)) {
                //Debug.Log(message: "Alavanca " + alavanca.identificador + " ativa");
                alavanca.canhoes.AtivaCanhao();
                if (_respostaAtual == alavanca.identificador) {
                    //Debug.Log("Ativou a alavanca correta!");
                    StartCoroutine(PainelRespostaCorreta("Resposta Correta!", audioClips[0]));
                    _coins += 10;
                    contadores[0].text = _coins + "x";
                    if (_index == _lines.Length) {
                        //Debug.Log("ACABOU");
                        _fimDoJogo = true;
                    }
                    _controller = true;
                }
                else {
                    if (_respostaBonus == alavanca.identificador) {
                        //Debug.Log("Ativou a alavanca bonus!!");
                        if (_bonusAtivo) continue;
                        _coins += 10;
                        _bonusAtivo = true;
                        contadores[0].text = _coins + "x";
                        textoResposta[_respostaBonus - 1].text = "Bonus Já Ativo";
                    }
                    else {
                        //Debug.Log("Ativou a alavanca incorreta :(");
                        StartCoroutine(PainelRespostaCorreta("Resposta Incorreta ):", audioClips[1]));
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
        }
        
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
        //Debug.Log("Seta Desafio");
        comandos.text = _lines[_index];
        _index += 1;
        for (var i = 0; i < textoResposta.Length; i++) {
            textoResposta[i].text = _lines[_index + i];
        }
        _index += 5;
        _respostaAtual = AlavancaCorreta(_lines[_index]);
        _respostaBonus = AlavancaCorreta("Pontos Extra");
        _index += 1;
    }

    private int AlavancaCorreta(string respostaCorreta) {
        for (var i = 0; i < textoResposta.Length; i++) {
            if (textoResposta[i].text == respostaCorreta) {
               // Debug.Log("Alavanca correta: " + (i + 1));
                _bonusAtivo = false;
                return i + 1;
            }
        }
        return 0;
    }

    private void ResetaAlavancas() {
        foreach (var alavanca in alavancas) {
            alavanca.GetComponent<Animator>().SetBool(Ativa, false);
            alavanca.AlavancasSound(false);
        }
        cronometro.timeStart = 12f;
    }

    public void Play() {
        _disablekey.keyboardAble = true;
        painelInicial.SetActive(false);
        IniciaRodada();
    }

    //TODO: Ao sair salvar a pontuação do jogador no ManagerOfAll 
    public void JogarNovamente() {
        painelDerrota.SetActive(false);
        SceneManager.LoadScene("Jogo Inf Logica");
    }

    public void Sair() {
        SceneManager.LoadScene("Jogo Principal");
    }
    
    private IEnumerator PainelRespostaCorreta(string textoPrint, AudioClip audioClip) { //ativa painel de resposta correta
        audioSource.clip = audioClip;
        audioSource.Play();
        painelRespCorreta.SetActive(true);
        painelRespCorreta.transform.GetChild(0).GetComponent<Text>().text = textoPrint;
        yield return new WaitForSeconds(2);
        ProximaAcao(_controller);
        painelRespCorreta.SetActive(false);
    }

    public void MudaTexto(Text text) {
        //TODO: Preencher texto das funções
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
