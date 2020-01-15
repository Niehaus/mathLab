using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    protected Animator myAnim;
    public new string name;
    public int statusUp;
    private static readonly int Picked = Animator.StringToHash("picked");
    protected Manager _manager;
    protected PlayerController playerController;
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            myAnim.SetBool(Picked, true);
            if (name == "Coins") {
                Manager._coins = Manager._coins + statusUp;
                _manager.contador[0].text = Manager._coins + "x";
            }else if (name == "Pao") {
                Manager._hearts = Manager._hearts + statusUp;
                _manager.contador[1].text = Manager._hearts + "x";
            }else if (name == "Cenoura") {
                playerController.speed = playerController.speed + statusUp;
                Debug.Log(playerController.speed);  
            }
            StartCoroutine(WaitToDestroy());
        }
    }

    IEnumerator WaitToDestroy() {
        yield return new WaitForSeconds(0.5f); //TODO: ajustar tempo ao som de destruir
        Destroy(gameObject);
    }
}
