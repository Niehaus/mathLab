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
        Debug.Log(rand.NextDouble() * 10) ;
        _coins1 = FindObjectOfType<Coins>();
        _cenoura = FindObjectOfType<Cenoura>();
        _pao = FindObjectOfType<Pao>();
        
        contador[0].text = _coins + "x";
        contador[1].text =  _hearts + "x" ;
        
        for (int i = 1; i < qnt_Cenouras + 1; i++) {
            Debug.Log("time" + (i * _cenoura.spawn));
            Debug.Log("time" + (i * _pao.spawn));
            Debug.Log("time" + (i * _coins1.spawn));
            StartCoroutine(Spawn(i * _cenoura.spawn, _cenoura.transform));    
            StartCoroutine(Spawn(i * _pao.spawn, _pao.transform));    
            StartCoroutine(Spawn(i * _coins1.spawn, _coins1.transform));    
        }
       
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    private IEnumerator Spawn(int time, Transform prefab) {
        
        yield return new WaitForSeconds(time);
        
        Instantiate(prefab, new Vector3((float) (rand.NextDouble() * 20.0f), 0, 0), Quaternion.identity);
        
        Debug.Log("ok");
        
        
    }

}
