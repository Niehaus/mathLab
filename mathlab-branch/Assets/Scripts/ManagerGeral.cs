using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class ManagerGeral : MonoBehaviour
{
    public static bool[] faseFeita = {false, false, false, false};
    public static string orientacao = "principal";
    public static VectorValue _vectorValue;
    public PlayerController playerController;
    public static bool _changepos;
    private Vector2 _atualPos;
    private CameraController _cam;
    public static Vector2 newMinPos = new Vector2(-6.4f, -6.45f);
    public static Vector2 newMaxPos = new Vector2(2.85f, 1.08f);
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void OnStartGame() {
        Debug.Log("set min e max values");
        
        
        foreach (var fase in faseFeita) {
            Debug.Log("fase feita:" + fase );
        }
    }
    
    private void Start() {
        _cam.minPosition = newMinPos;
        _cam.maxPosition = newMaxPos;
    }
}
