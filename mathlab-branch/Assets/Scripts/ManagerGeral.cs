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
    private static CameraController _cam;

    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void OnStartGame() {
        Debug.Log("set min e max values");
        
        
        foreach (var fase in faseFeita) {
            Debug.Log("fase feita:" + fase );
        }
    }
}
