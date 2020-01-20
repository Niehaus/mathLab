using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    protected Animator myAnim;
    public new string name;
    public int statusUp;
    public int spawn;
    private static readonly int Picked = Animator.StringToHash("picked");
    protected Manager manager;
    protected PlayerController playerController;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            myAnim.SetBool(Picked, true);
            if (name == "Coins") {
                Manager.coins += statusUp;
                manager.contador[0].text = Manager.coins + "x";
            }else if (name == "Pao") {
                Manager.hearts += statusUp;
                manager.contador[1].text = Manager.hearts + "x";
            }else if (name == "Cenoura") {
                playerController.speed += statusUp;
                Debug.Log(playerController.speed);  
            }
            StartCoroutine(WaitToDestroy());
        }
    }

    protected IEnumerator WaitToDestroy() {
        yield return new WaitForSeconds(0.5f); //TODO: ajustar tempo ao som de destruir
        Destroy(gameObject);
    }
    
}
