using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameManager2 : MonoBehaviour {
    public Sprite cardBack; /*Cada carta possui um par diferente de si, é um par [MP Func - MP Name]*/
    public Sprite[] cardFace;
    public Sprite[] cardFacePair;
    public GameObject[] cards;
    //private int cartaDown = 1;
    private int cartaEscolhida = 0;
    
    //private bool inicia = false;
    List<int> cardInfo = new List<int>();

    private void Start() {

     for (int i = 0; i <= 4; i++) {
        cartaEscolhida = Random.Range(0, cards.Length);
            Debug.Log(cartaEscolhida);
           cardInfo.Add(cartaEscolhida);
     }
    }

}
