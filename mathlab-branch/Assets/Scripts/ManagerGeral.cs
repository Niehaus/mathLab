using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerGeral : MonoBehaviour
{
    public static bool[] faseFeita = {true, false, false, false};
    public static string orientacao = "principal";
    public static VectorValue _vectorValue;
    public PlayerController playerController;
    public static bool _changepos;
    private Vector2 _atualPos;
    private CameraController _cam;
    public static Vector2 newMinPos = new Vector2(-6.4f, -6.45f);
    public static Vector2 newMaxPos = new Vector2(2.85f, 1.08f);

    public static int totalPontosFase2 = 0;
    public static int totalPontosFase3 = 0;
    public static int totalTempoFase1 = 0;

    private void Start() {
        Debug.Log("pontos fase2:" + totalPontosFase2);
        Debug.Log("pontos fase3:" + totalPontosFase3);
        /*_cam.minPosition = newMinPos;
        _cam.maxPosition = newMaxPos;*/
    }
    
    
    
    
}
