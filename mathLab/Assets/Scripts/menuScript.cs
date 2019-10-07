using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class menuScript : MonoBehaviour {

    public void triggerMenu(int trigger) {
		switch (trigger) {
            case (0):
                SceneManager.LoadScene("introScene1");
                break;
		    case (1):
                Application.Quit();
                break;
            case (2):
                SceneManager.LoadScene("settingsScene");
                break;
            case (3):
                SceneManager.LoadScene("menuScene");
                break;
		}
	}

    public void showCardsMenu(int trigger) {
        switch (trigger) {
            case (0):
                SceneManager.LoadScene("Inferencia - 4 cartas");
                break;
            case (1):
                Application.Quit();
                break;
        }
    }

    public void introSceneButtons(int trigger) {
        switch (trigger) {
            case (0):
                SceneManager.LoadScene("MostraCards");
                break;
            case (1):
                Application.Quit();
                break;
        }
    }

    public void tutorialskip(int op){
        GameObject manager = GameObject.FindGameObjectWithTag("manager");
        GameObject panel = GameObject.FindGameObjectWithTag("TutoPanel");
        GameObject panelTalk = GameObject.FindGameObjectWithTag("TutoPanelTalk");
        switch (op) {
            case (0):
                SceneManager.LoadScene("Jogo - Tabela Verdade Next (2)");
                break;
            case (1):
                panel.SetActive(false);
                panelTalk.SetActive(true);
                tutorial man = manager.GetComponent<tutorial>();
                man.iniciaTutorial();
                break;
        }
    }
}
