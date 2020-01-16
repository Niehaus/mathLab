using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class Manager : MonoBehaviour {

     public Text[] contador;

     internal static int _coins;
     internal static int _hearts = 3;
     
     private int qnt_Coins = 6;
     private int qnt_Hearts = 3;
     private int qnt_Cenouras = 4;
     
     private Cenoura _cenoura;
     private Coins _coins1;
     private Pao _pao;
     Random rand = new Random();
     
     
     // Start is called before the first frame update
    void Start() {
        
        Debug.Log("go") ;
        _coins1 = FindObjectOfType<Coins>();
        _cenoura = FindObjectOfType<Cenoura>();
        _pao = FindObjectOfType<Pao>();
        
        contador[0].text = _coins + "x";
        contador[1].text =  _hearts + "x" ;
        
       
        for (int i = 1; i < qnt_Cenouras + 1; i++) {
            StartCoroutine(Spawn(i * _cenoura.spawn, _cenoura.transform, rand.NextDouble()));
        }
        for (int i = 1; i < qnt_Coins + 1; i++) {
            StartCoroutine(Spawn(i * _coins1.spawn, _coins1.transform, rand.NextDouble()));
        }
        for (int i = 1; i < qnt_Hearts + 1; i++) {
            StartCoroutine(Spawn(i * _pao.spawn, _pao.transform, rand.NextDouble()));
        }
       
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    private IEnumerator Spawn(int time, Transform prefab, double orientacao) {
        yield return new WaitForSeconds(time);
        if (orientacao > 0.5) { //direita
            Debug.Log("Direita");
            var vector = GeraCoord(new Vector2(9.3f, -4.3f), new Vector2(3.23f, 5.36f));
            Instantiate(prefab, new Vector3(vector.x, vector.y, 0), Quaternion.identity);
        }
        else { //esquerda
            Debug.Log("Esquerda");
            var vector = GeraCoord(new Vector2(-8.4f, -4.3f), new Vector2(-2.16f, 5.36f));
            Instantiate(prefab, new Vector3(-vector.x, vector.y, 0), Quaternion.identity);
        }
        Debug.Log("ok");
    }

    //TODO: Tentar usar uma função só.. talvez valores absolutos sejam utéis aqui --> RESOLVIDO!
    private Vector2 GeraCoord(Vector2 minAltura, Vector2 minLargura) {
        
        var valorX =  (rand.NextDouble() + 10 * rand.NextDouble()) + Math.Abs(minLargura.x);
        var valorY =  rand.NextDouble() + 10 * rand.NextDouble() + minAltura.y;    
            
        Debug.Log(valorX);
        while (Math.Abs(valorX) > Math.Abs(minAltura.x)) {
            valorX = (rand.NextDouble() + 10 * rand.NextDouble()) + Math.Abs(minLargura.x);
            Debug.Log("ERROU - Novo valor em X: " + valorX);
        }
        
        while (valorY > minLargura.y) {
            valorY =  rand.NextDouble() + 10 * rand.NextDouble() + minAltura.y;
            Debug.Log("ERROU - Novo valor em Y: " + valorY);
        }
        var coordenadas = new Vector2((float) valorX, (float) valorY);
        return coordenadas;
    }
    
    

}
