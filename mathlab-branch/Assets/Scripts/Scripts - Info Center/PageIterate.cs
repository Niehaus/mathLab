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
    
    public void Livro_Ativo() {
        var maxContent = Math.Max(livroAberto.pageRightContent.Length - 1, livroAberto.pageLeftContent.Length - 1);
        if (pageCounter == maxContent) {
            /*Pagina da direita*/
            livroAberto.pageRightContent[livroAberto.pageRightContent.Length - 2].SetActive(false);   
            livroAberto.pageRightContent[livroAberto.pageRightContent.Length - 1].SetActive(true);
            
            /*Pagina da esquerda*/
            livroAberto.pageLeftContent[livroAberto.pageLeftContent.Length - 2].SetActive(false);   
            livroAberto.pageLeftContent[livroAberto.pageLeftContent.Length - 1].SetActive(true);
            
            Parity_of_page();
            return;
        }
        
        if (pageCounter > 0 ) {
            livroAberto.pageRightContent[pageCounter - 1].SetActive(false);   
            livroAberto.pageRightContent[pageCounter].SetActive(true);


            livroAberto.pageLeftContent[pageCounter - 1].SetActive(false);
            livroAberto.pageLeftContent[pageCounter].SetActive(true);
            
            Parity_of_page();
            
        }else if (pageCounter == 0) {
            livroAberto.pageRightContent[pageCounter].SetActive(true);
            livroAberto.pageLeftContent[pageCounter].SetActive(true);
            
            livroAberto.pageMarkRight.GetComponent<Image>().sprite = numerosSprite[pageCounter + 1];
            livroAberto.pageMarkLeft.GetComponent<Image>().sprite = numerosSprite[pageCounter];
        }
        
        pageCounter++;
        
    }
    private void Parity_of_page() {
        if (pageCounter % 2 != 0) { //pageCounter impar
            livroAberto.pageMarkRight.GetComponent<Image>().sprite = numerosSprite[pageCounter + 2];
            livroAberto.pageMarkLeft.GetComponent<Image>().sprite = numerosSprite[pageCounter + 1];
        }
        else { //pageCounter par
            livroAberto.pageMarkRight.GetComponent<Image>().sprite = numerosSprite[pageCounter + 1];
            livroAberto.pageMarkLeft.GetComponent<Image>().sprite = numerosSprite[pageCounter];
        }
    }
    
}

