using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public class Manager : MonoBehaviour {

     public Text[] contador;

     internal static int coins;
     internal static int hearts = 3;
     
     private int _qntCoins = 10;
     private int _qntHearts = 3;
     private int _qntCenouras = 4;
     
     public Cenoura cenoura;
     public Coins coins1;
     public Pao pao;
     public GameObject bau;
     private Palavra _palavra;
     private Painel _painel;
     public PainelLose painelLose;
     public PainelLose painelWin;
     private PlayerController _disablekey; //bloqueador de teclado enquanto NPC fala
     public GameObject startAnim;

     private Random _rand = new Random();
     private static readonly int Start1 = Animator.StringToHash("start");
     private Animator _animator;
     private Rigidbody2D _rigidbody2D;

     [NonSerialized] public bool fimDeJogo;

     private static readonly int MoveX = Animator.StringToHash("moveX");
     private static readonly int MoveY = Animator.StringToHash("moveY");

     // Start is called before the first frame update
     private void Start() {
         _palavra = FindObjectOfType<Palavra>();
         _painel = FindObjectOfType<Painel>();
         _rigidbody2D = _palavra.GetComponent<Rigidbody2D>(); //Rigid da palavra
         _animator = startAnim.GetComponent<Animator>(); //Animator do Start
         _disablekey = FindObjectOfType<PlayerController>();
         _disablekey.keyboardAble = false;
         _palavra.gameObject.SetActive(false);
         coins = 0;
         hearts = 3;
         contador[0].text = coins + "x";
         contador[1].text =  hearts + "x";
     }
    
     private IEnumerator Spawn(int time, Transform prefab, double orientacao, string nome) {
         yield return new WaitForSeconds(time);
         if (orientacao > 0.5) { //direita
             var vector = GeraCoord(new Vector2(9.3f, -4.3f), new Vector2(3.23f, 5.36f));
             Instantiate(prefab, new Vector3(vector.x, vector.y, 0), Quaternion.identity);
         }
         else { //esquerda
             var vector = GeraCoord(new Vector2(-8.4f, -4.3f), new Vector2(-2.16f, 5.36f));
             Instantiate(prefab, new Vector3(-vector.x, vector.y, 0), Quaternion.identity);
         }

         if (nome == "Cenoura") {
             _palavra.MakeMeFaster();
         }
         //Debug.Log("ok");
     }

     private void Update() {
         if (!fimDeJogo) return;
         _disablekey.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0f,0f,0f); //faz jogador parar
         _disablekey.gameObject.GetComponent<Animator>().SetFloat(MoveX,_disablekey.gameObject.GetComponent<Rigidbody2D>().velocity.x);
         _disablekey.gameObject.GetComponent<Animator>().SetFloat(MoveY,_disablekey.gameObject.GetComponent<Rigidbody2D>().velocity.y);
         _rigidbody2D.bodyType = RigidbodyType2D.Static; //para objeto
     }

     private Vector2 GeraCoord(Vector2 minAltura, Vector2 minLargura) {
        
         var valorX =  (_rand.NextDouble() + 10 * _rand.NextDouble()) + Math.Abs(minLargura.x);
         var valorY =  _rand.NextDouble() + 10 * _rand.NextDouble() + minAltura.y;
         
         while (Math.Abs(value: valorX) > Math.Abs(minAltura.x)) {
             valorX = (_rand.NextDouble() + 10 * _rand.NextDouble()) + Math.Abs(minLargura.x);
             //Debug.Log("ERROU - Novo valor em X: " + valorX);
         }
        
         while (valorY > minLargura.y) {
             valorY =  _rand.NextDouble() + 10 * _rand.NextDouble() + minAltura.y;
             //Debug.Log("ERROU - Novo valor em Y: " + valorY);
         }
         var coordenadas = new Vector2((float) valorX, (float) valorY);
         return coordenadas;
     }

     public void ContabilizaPontos(bool acerto) {
         if (acerto) {
             coins += 5;
             contador[0].text = coins + "x";
         }
         else {
             hearts -= 1;
             contador[1].text = hearts + "x";
             if (hearts != 0 && hearts >= 0) return;
             //TODO: JOGO ACABA
             _disablekey.keyboardAble = false;
             fimDeJogo = true;
             painelLose.AtualizaTexto();
             painelLose.gameObject.SetActive(true);
         }
     }

     private void IniciaJogo() {
         _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
         _palavra.gameObject.SetActive(true);
         for (var i = 1; i < _qntCenouras + 1; i++) {
             StartCoroutine(Spawn(i * cenoura.spawn, cenoura.transform, _rand.NextDouble(), cenoura.name));
         }
         for (var i = 1; i < _qntCoins + 1; i++) {
             StartCoroutine(Spawn(i * coins1.spawn, coins1.transform, _rand.NextDouble(), coins1.name));
         }
         for (var i = 1; i < 5 + 1; i++) {
             StartCoroutine(Spawn(i * 40, coins1.transform, _rand.NextDouble(), coins1.name));
         }
         for (var i = 1; i < _qntHearts + 1; i++) {
             StartCoroutine(Spawn(i * pao.spawn, pao.transform, _rand.NextDouble(), pao.name));
         }
     }

     private IEnumerator JustInTime() { //seta animações tempos etc
         _animator.SetBool(Start1,true);
         _painel.gameObject.SetActive(false);
         
         yield return new WaitForSeconds(2);
         
         _animator.SetBool(Start1,false);
         startAnim.gameObject.SetActive(false);
         _disablekey.keyboardAble = true;
         IniciaJogo();
         //TODO:COMEÇA CONTAR TEMPO QD TIVER O TIMER
     }
    
     public void ReloadScene() {
         painelLose.gameObject.SetActive(false);
         painelWin.gameObject.SetActive(false);
         fimDeJogo = false;
         SceneManager.LoadScene("Jogo Eqv Logica");
         _painel.gameObject.SetActive(true);
     }

     public void CenaPrincipal() {
         SceneManager.LoadScene("Jogo Principal");
     }
     public void Jogar() {
         StartCoroutine(JustInTime());
     }
     
}

