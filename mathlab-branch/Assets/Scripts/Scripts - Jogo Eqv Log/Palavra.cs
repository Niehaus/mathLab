using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Palavra : Item  {
    
    private Vector3 _originalPosition;
    [NonSerialized] Rigidbody2D _rigidbody2D;
    private static readonly string PathTable = Directory.GetCurrentDirectory() + "/Assets/NewAdds/eqvLog.txt";
    private readonly StreamReader _file = new StreamReader(PathTable);
    public Text desafio;
    [NonSerialized] public string respostaAtual;
    private static readonly int BlowUpErrou = Animator.StringToHash("blowUpErrou");
    private static readonly int BlowUp = Animator.StringToHash("blowUp");

    private static readonly int Open = Animator.StringToHash("open");

    // Start is called before the first frame update
    private void Start() {
        GameObject o;
        myAnim = GetComponent<Animator>();
        manager = FindObjectOfType<Manager>();
        _rigidbody2D = (o = gameObject).GetComponent<Rigidbody2D>();
        _originalPosition = o.transform.position;
        manager.bau.GetComponent<Animator>().SetBool(Open,true);
        desafio.text = _file.ReadLine();
        respostaAtual = _file.ReadLine();
        
        _rigidbody2D.bodyType = RigidbodyType2D.Static;
    }
    
    public void PalavraRoutine(bool acertou) {
        manager.ContabilizaPontos(acertou);
        StartCoroutine(AnimSet(acertou));
        if (_file.EndOfStream && Manager.hearts > 0) {
            Debug.Log("venceu");
            manager.painelWin.gameObject.SetActive(true);
            Debug.Log("LIGA PAINEL");
            manager.painelWin.AtualizaTexto();
            manager.fimDeJogo = true;

        }
        else {
            desafio.text = _file.ReadLine();
            respostaAtual = _file.ReadLine();    
        }
        //TODO: qd o arquivo acabar parar o jogo 
        //TODO: fazer o timer do jogo
    }

    public void MakeMeFaster() { //DEIXA PALAVRA MAIS RAPIDA NO TEMPO DA CENOURA
        _rigidbody2D.drag -= 0.2f;
    }

    private IEnumerator AnimSet(bool acertou) {
        myAnim.SetBool(acertou ? BlowUp : BlowUpErrou, true);
        manager.bau.GetComponent<Animator>().SetBool(Open,false);
        yield return new WaitForSeconds(0.5f);
        manager.bau.GetComponent<Animator>().SetBool(Open,true);
        gameObject.transform.position = _originalPosition;
        myAnim.SetBool(BlowUp, false);
        myAnim.SetBool(BlowUpErrou, false);
    }

    public bool TurnMeBool(string respostaAtual) {
        switch (respostaAtual) {
            case "v":
                return true;
            case "f":
                return false;
            default:
                return false;
        }
    }
    
    
}
