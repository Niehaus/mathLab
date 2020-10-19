using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tesla : Npc
{
    private bool _playerInRange;
    private PlayerController _disablekey;

    private static readonly int PlayerInRange = Animator.StringToHash("playerInRange");

    // Start is called before the first frame update
    private void Start() {
        _disablekey = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    private void Update() {
        
        if (index > 7 && ManagerGeral.faseFeita[0]) {
            caixaDialogo.SetActive(false);
            fimDialogo = true;
            _disablekey.keyboardAble = true;
            SceneManager.LoadScene("Scenes/Jogo Inf Logica");
            //TODO: ir para tela da missão se ela ja n tiver acontecido
        }
        if (_playerInRange) {
            balaodeFala.SetActive(true);
            balaodeFala.GetComponent<Animator>().SetBool(PlayerInRange, true);
        }
 
        if (dialogo.text == sentences[index]) {
            botaoContinuar.SetActive(true);
            _disablekey.keyboardAble = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && _playerInRange && _disablekey.keyboardAble) {
            caixaDialogo.SetActive(true);
            _disablekey.keyboardAble = false;
            NpcFala();
            if (fimDialogo) { 
                _disablekey.keyboardAble = true;
                caixaDialogo.SetActive(false);
                fimDialogo = false;
                Index();
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        Index();
        if (other.CompareTag("Player")) {
            _playerInRange = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        Index();
        if (!other.CompareTag("Player")) return;
        balaodeFala.GetComponent<Animator>().SetBool(PlayerInRange, false);
        balaodeFala.SetActive(false);
        _playerInRange = false;
        caixaDialogo.SetActive(false);
        //TODO: se a missão for feita muda o index pra um dialogo diferente
        fimDialogo = false;
    }
    
    private void Index() {
        index = ManagerGeral.faseFeita[0] ? 0 : 8;
    }
}
