﻿using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class evqLogicas : MonoBehaviour
{
    public Text iniText;
    public GameObject playButton, gamePanel;

    public void IniciaJogo() {
        iniText.text = "";
        playButton.SetActive(false);    
        gamePanel.SetActive(true);
    }

    public void closePanel(){
        gamePanel.SetActive(false);
        /* levar para uma cena nova que contém só o jogo */
        
    }
}
