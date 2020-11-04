using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagerGeral : MonoBehaviour
{
    public static bool[] faseFeita = {false, false, false};
    public static string orientacao = "principal";
    public PlayerController playerController;
    private Vector2 _atualPos;
    private CameraController _cam;
    public static Vector2 newMinPos = new Vector2(-6.4f, -6.45f);
    public static Vector2 newMaxPos = new Vector2(2.85f, 1.08f);

    public static string jogadorAtual;
    public static string tempoJogador;
    public static int totalPontosFase2 = 0;
    public static int totalPontosFase3 = 0;
    public static float totalTempoFase1 = 0;
    
    
    public Text pontos1, pontos2, pontos3;
    public GameObject panelFimdeJogo, panelWelcome;
    
    
    private static bool _first = true;
    private void Start() {
        Debug.Log("JOGADOR ATUAL: " + jogadorAtual);
        Debug.Log("pontos fase2:" + totalPontosFase2);
        Debug.Log("pontos fase3:" + totalPontosFase3);

        if (_first) {
            panelWelcome.SetActive(true);
            _first = false;
        }
    }
    
    private void Update() {
        if (VerificaFim() != 3) return;
        // Debug.Log("Acabou de vdd :D");
        pontos1.text = totalTempoFase1.ToString();
        pontos2.text = totalPontosFase2.ToString();
        pontos3.text = totalPontosFase3.ToString();
            
        panelFimdeJogo.SetActive(true);
    }

    private static int VerificaFim() {
        var count = 0;
        foreach (var fase in faseFeita) {
            if (fase) {
                count++;
            }
        }
        
        return count;
    }

    
    public void ClosePanel() {
        panelWelcome.SetActive(false);
        _first = false;
    }
    
    public void Sair() {
        Application.Quit ();
    }
    /*
     TODO: - dialogo principal -> 7
     TODO: - http request pro bd -> 2 -> OK
     TODO: - finalizar jogo qd completa as 3 fases ->  1 -> OK
     TODO: - cronometro fase 1 -> 3 -> OK
     TODO: - sala de estudos conteudo e sons-> 8
     TODO: - dicas da fase 3  -> 4 -> OK
     TODO: - mais expressoes pra fase 3 e tabelas pra 1[OK] -> 5 -> OK
     TODO: - sons jogo principal  -> 6 -> OK
    */
}
