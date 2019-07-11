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
}
