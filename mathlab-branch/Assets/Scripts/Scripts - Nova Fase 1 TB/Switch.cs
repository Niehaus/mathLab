using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Switch : MonoBehaviour {
    
    public Text textoTv;
    public Text variavel_ref;
    private Animator _myAnim; 
    private bool _playerInRange;

    private static readonly int On = Animator.StringToHash("on");

    // Start is called before the first frame update
    void Start() {
        _myAnim = GetComponent<Animator>();
    }
    
    
    private void Update() {
        if (!_playerInRange || !Input.GetKeyDown(KeyCode.Space)) return;
        _myAnim.SetBool(On, !_myAnim.GetBool(On));
        
        if (_myAnim.GetBool(On)) {
            textoTv.text = "V";    
        }
        else {
            textoTv.text = "F";
        }

        
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            _playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (!other.CompareTag("Player")) return;
        _playerInRange = false;
    }
}
