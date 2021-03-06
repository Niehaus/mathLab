﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    protected Animator myAnim;
    protected AudioSource audioSource;
    public new string name;
    public int statusUp;
    public int spawn;
    private static readonly int Picked = Animator.StringToHash("picked");
    protected Manager manager;
    protected PlayerController playerController;

    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Player")) return;
        myAnim.SetBool(Picked, true);
        audioSource.Play();
        switch (name) {
            case "Coins":
                Manager.coins += statusUp;
                manager.contador[0].text = Manager.coins + "x";
                break;
            case "Pao":
                Manager.hearts += statusUp;
                manager.contador[1].text = Manager.hearts + "x";
                break;
            case "Cenoura":
                playerController.speed += statusUp;
//                Debug.Log(playerController.speed);
                break;
        }
        StartCoroutine(WaitToDestroy());
    }

    protected IEnumerator WaitToDestroy() {
        yield return new WaitForSeconds(0.5f); //TODO: ajustar tempo ao som de destruir
        Destroy(gameObject);
    }
    
}
