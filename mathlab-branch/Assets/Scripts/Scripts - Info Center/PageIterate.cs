using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageIterate : MonoBehaviour {
    
    public Sprite[] numerosSprite;

    public Book livroAberto;
    [NonSerialized]
    public int pageCounter = 0;
    
    // Start is called before the first frame update
    void Start() {
        gameObject.GetComponent<Image>().sprite = numerosSprite[2];
      
        
        
    }
    
    // Update is called once per frame
    void Update() {
    }

    public void passa_pagina() {
        gameObject.GetComponent<Image>().sprite = numerosSprite[2];
        
    }

    public void Livro_Ativo() {
        var maxContent = Math.Max(livroAberto.pageRightContent.Length, livroAberto.pageLeftContent.Length);
        Debug.Log(maxContent);
        //Int32.MaxValue;
        if (pageCounter == maxContent) {
            /*Pagina da direita*/
            livroAberto.pageRightContent[livroAberto.pageRightContent.Length - 1].SetActive(false);   
            livroAberto.pageRightContent[livroAberto.pageRightContent.Length].SetActive(true);
            
            /*Pagina da esquerda*/
            livroAberto.pageLeftContent[livroAberto.pageLeftContent.Length - 1].SetActive(false);   
            livroAberto.pageLeftContent[livroAberto.pageLeftContent.Length].SetActive(true);
            
            return;
        }

        if (pageCounter > 0 ) {
            livroAberto.pageRightContent[pageCounter - 1].SetActive(false);   
            livroAberto.pageRightContent[pageCounter].SetActive(true);
            
            livroAberto.pageLeftContent[pageCounter - 1].SetActive(false);   
            livroAberto.pageLeftContent[pageCounter].SetActive(true);   
        }
        pageCounter++;

    }
}

