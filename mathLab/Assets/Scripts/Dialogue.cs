using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour {
   public TextMeshProUGUI textDisplay;
   public string[] sentences;
   private int index;
   public float typingSpeed;
   public GameObject continueButton;
   public GameObject[] panelsActive;
   
   public Animator instrutor;
   void Start() {
       StartCoroutine(Type());
   }
   void Update()    {
       if (textDisplay.text == sentences[index]) {
            instrutor.SetBool("fala", false);
           continueButton.SetActive(true);
       }
       if (index == 4) {
           panelsActive[0].SetActive(true);
           panelsActive[1].SetActive(true);
       }else {
            panelsActive[0].SetActive(false);
            panelsActive[1].SetActive(false);
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
