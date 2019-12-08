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
    private Boolean playerInRange;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("APERTEI ESPAÇO");
            playerStorage.valorInicial = playerPosition;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)  {
        if (other.CompareTag("Player") && !other.isTrigger) {
            caixaDialogo.SetActive(true);
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        playerInRange = false;
        caixaDialogo.SetActive(false);
    }
}
