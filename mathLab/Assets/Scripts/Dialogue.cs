using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour {
   public TextMeshProUGUI textDisplay;
   public string[] sentences;
   public int index;
   public float typingSpeed;
   public GameObject continueButton;
   public GameObject[] panelsActive;   
   public Animator instrutor;
    void Start() {
       StartCoroutine(Type());
    }
    void Update() {
       if (textDisplay.text == sentences[index]) {
            instrutor.SetBool("fala", false);
           continueButton.SetActive(true);
       }
        if (index == 4) {
            panelsActive[0].SetActive(true);
            panelsActive[1].SetActive(true);
        }else if (index == 5) {
            panelsActive[0].SetActive(false);
            panelsActive[1].SetActive(false);
            panelsActive[2].SetActive(true);
        }else if(index == 6) {
            panelsActive[2].SetActive(false);
            panelsActive[3].SetActive(true);
            panelsActive[4].SetActive(true);
            panelsActive[5].SetActive(true);
        }else if (index == 7 || index == 8) { //manter ativo em 2 estados
            panelsActive[3].SetActive(false);
            panelsActive[4].SetActive(false);
            panelsActive[5].SetActive(false);
            panelsActive[6].SetActive(true);
            panelsActive[7].SetActive(true);
        }else if (index == 9) {
                panelsActive[6].SetActive(false);
                panelsActive[7].SetActive(false);
                panelsActive[8].SetActive(true);
        }else {
            for (int i = 0; i < panelsActive.Length; i++) {
                panelsActive[i].SetActive(false);
            }
        }
    }

    IEnumerator Type(){
        foreach (char letter in sentences[index].ToCharArray()) {
            textDisplay.text += letter;
            instrutor.SetBool("fala",true);
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence(){
        continueButton.SetActive(false);
        if (index < sentences.Length - 1) {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }else {
            textDisplay.text = "";
            continueButton.SetActive(false);
        }
    }

}
