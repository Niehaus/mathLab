using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class SceneTransition : MonoBehaviour  {

    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerStorage;
    public GameObject caixaDialogo;
    private Boolean _playerInRange;
    public Vector2 newMinPos;
    public Vector2 newMaxPos;
    private void Update() {
        if (!Input.GetKeyDown(KeyCode.Space) || !_playerInRange) return;
        playerStorage.valorInicial = playerPosition;
        SceneManager.LoadScene(sceneToLoad);
    }

    public void LoadNext(string sceneName) {
        playerStorage.valorInicial = playerPosition;
       
        ManagerGeral.newMaxPos = newMaxPos;
        ManagerGeral.newMinPos = newMinPos;
      
        SceneManager.LoadScene(sceneName);
    }
    
    private void OnTriggerEnter2D(Collider2D other)  {
        if (!other.CompareTag("Player") || other.isTrigger) return;
        caixaDialogo.SetActive(true);
        _playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        _playerInRange = false;
        caixaDialogo.SetActive(false);
    }
}
