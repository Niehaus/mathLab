using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static System.String;

public class Palavra : Item  {
    
    private Vector3 _originalPosition;
    private AudioSource _audioSource;
    public AudioClip[] clip;
    [NonSerialized] Rigidbody2D _rigidbody2D;
    public Text desafio;
    [NonSerialized] public string respostaAtual;
    private static readonly int BlowUpErrou = Animator.StringToHash("blowUpErrou");
    private static readonly int BlowUp = Animator.StringToHash("blowUp");
    private string[] _lines;
    private int _index;
    private static readonly int Open = Animator.StringToHash("open");
    string[] _stringSeparators = new string[] { "\r\n" };
    // Start is called before the first frame update
    private void Start() {
        GameObject o;
        myAnim = GetComponent<Animator>();
        manager = FindObjectOfType<Manager>();
        _audioSource = GetComponent<AudioSource>();
        _rigidbody2D = (o = gameObject).GetComponent<Rigidbody2D>();
        _originalPosition = o.transform.position;
        manager.bau.GetComponent<Animator>().SetBool(Open,true);
        _index = 0;
        var textFile = Resources.Load<TextAsset>("Data/eqvLog");
        if (textFile == null) {
            Debug.Log("Arquivo Texto Não Está Aqui");
            return;
        }
        _lines = textFile.text.Split(_stringSeparators, StringSplitOptions.None);
        desafio.text = _lines[_index];
        respostaAtual = _lines[_index + 1];
        _index += 2;

        _rigidbody2D.bodyType = RigidbodyType2D.Static;
    }
    
    public void PalavraRoutine(bool acertou) {
        manager.ContabilizaPontos(acertou);
        StartCoroutine(AnimSet(acertou));
        if (_index + 1 > _lines.Length && Manager.hearts > 0) {
            manager.painelWin.gameObject.SetActive(true);
            manager.painelWin.AtualizaTexto();
            manager.fimDeJogo = true;
            ManagerGeral.faseFeita[1] = true;
        }
        else { 
            desafio.text = _lines[_index];
           respostaAtual = _lines[_index + 1];
           _index += 2;
        }
        //TODO: qd o arquivo acabar parar o jogo 
        //TODO: fazer o timer do jogo
    }

    public void MakeMeFaster() { //DEIXA PALAVRA MAIS RAPIDA NO TEMPO DA CENOURA
        _rigidbody2D.drag -= 0.2f;
    }

    private IEnumerator AnimSet(bool acertou) {
        myAnim.SetBool(acertou ? BlowUp : BlowUpErrou, true);
        _audioSource.clip = acertou ? clip[0] : clip[1];
        _audioSource.Play();
        manager.bau.GetComponent<Animator>().SetBool(Open,false);
        yield return new WaitForSeconds(0.5f);
        manager.bau.GetComponent<Animator>().SetBool(Open,true);
        gameObject.transform.position = _originalPosition;
        myAnim.SetBool(BlowUp, false);
        myAnim.SetBool(BlowUpErrou, false);
    }

    public bool TurnMeBool(string resposta) {
        if (resposta.Equals("v")) {
            return true;
        }else if (resposta.Equals("f")) {
            return false;
        }
        return false;
    }
    
    
}
