using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerInf : MonoBehaviour {

    public Alavanca[] alavancas;
    public Text[] textoResposta;
    public Text[] contadores;
    public Text comandos;
    private int _hearts = 3;
    private int _coins;
    private string[] _stringSeparators = { "\r\n" };
    private string[] _lines;
    private int respostaAtual;
    private int index;
    private static readonly int Ativa = Animator.StringToHash("ativa");

    // Start is called before the first frame update
    private void Start() {
        contadores[0].text = _coins + "x";
        contadores[1].text = _hearts + "x";
        var textFile = Resources.Load<TextAsset>("Data/infLog");
        if (textFile == null) {
            Debug.Log("Arquivo Texto Não Está Aqui");
            return;
        }
        _lines = textFile.text.Split(_stringSeparators, StringSplitOptions.None);
        setaDesafio();
        StartCoroutine(VerificaCanhoes());
        
    }
    
    private IEnumerator VerificaCanhoes() { //seta animações tempos etc
        
         Debug.Log(message: "começa a contar ");
        yield return new WaitForSeconds(seconds: 5);
        foreach (var alavanca in alavancas) {
            if (alavanca.GetComponent<Animator>().GetBool(id: Ativa)) {
                Debug.Log(message: "Alavanca " + alavanca.identificador + " ativa");
                alavanca.canhoes.AtivaCanhao();
                if (respostaAtual == alavanca.identificador) {
                    Debug.Log("Ativou a alavanca correta!");
                    _coins += 10;
                    contadores[0].text = _coins + "x";
                }
                else {
                    Debug.Log("Ativou a alavanca incorreta :(");
                    _hearts -= 1;
                    contadores[1].text = _hearts + "x";
                }
            }
        }
    }


    public void setaDesafio() {
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
}
