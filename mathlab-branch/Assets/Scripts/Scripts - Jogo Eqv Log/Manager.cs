using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class Manager : MonoBehaviour {

     public Text[] contador;

     internal static int coins;
     internal static int hearts = 3;
     
     private int _qntCoins = 10;
     private int _qntHearts = 3;
     private int _qntCenouras = 4;
     
     public Cenoura _cenoura;
     public Coins _coins1;
     public Pao _pao;
     private Palavra _palavra;
     private Painel _painel;
     private PlayerController _disablekey; //bloqueador de teclado enquanto NPC fala
     public GameObject startAnim;

     private Random _rand = new Random();
     private static readonly int Start1 = Animator.StringToHash("start");
     private Animator _animator;
     private Rigidbody2D _rigidbody2D;

     // Start is called before the first frame update
     private void Start() {
         _palavra = FindObjectOfType<Palavra>();
         _painel = FindObjectOfType<Painel>();
         _rigidbody2D = _palavra.GetComponent<Rigidbody2D>(); //Rigid da palavra
         _animator = startAnim.GetComponent<Animator>(); //Animator do Start
         _disablekey = FindObjectOfType<PlayerController>();
         _disablekey.keyboardAble = false;
        
         contador[0].text = coins + "x";
         contador[1].text =  hearts + "x" ;
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
             if (hearts == 0 || hearts < 0) {  
                 //TODO: JOGO ACABA
             }
         }
     }

     private void IniciaJogo() {
         _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
         for (var i = 1; i < _qntCenouras + 1; i++) {
             StartCoroutine(Spawn(i * _cenoura.spawn, _cenoura.transform, _rand.NextDouble(), _cenoura.name));
         }
         for (var i = 1; i < _qntCoins + 1; i++) {
             StartCoroutine(Spawn(i * _coins1.spawn, _coins1.transform, _rand.NextDouble(), _coins1.name));
         }
         for (var i = 1; i < 5 + 1; i++) {
             StartCoroutine(Spawn(i * 40, _coins1.transform, _rand.NextDouble(), _coins1.name));
         }
         for (var i = 1; i < _qntHearts + 1; i++) {
             StartCoroutine(Spawn(i * _pao.spawn, _pao.transform, _rand.NextDouble(), _pao.name));
         }
     }

     public void Play() {
         StartCoroutine(JustInTime());
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
}

