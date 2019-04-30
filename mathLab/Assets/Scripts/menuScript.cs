using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class menuScript : MonoBehaviour {

	public void triggerMenu(int trigger) {
		switch (trigger) {
		case(0) :
			SceneManager.LoadScene("gameScene");
			break;
		case(1) :
			Application.Quit();
			break;
		}
	}

    public void showCardsMenu(int trigger)
    {
        switch (trigger)
        {
            case (0):
                SceneManager.LoadScene("Inferencia - 4 cartas");
                break;
            case (1):
                Application.Quit();
                break;
        }
    }
}
