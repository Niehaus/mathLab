using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class configMaster : MonoBehaviour {

    public GameObject audioPanel, prefPanel,truthPanel, equivPanel, infPanel;
    
    public void openPanel(int panelNum) {
         
         switch (panelNum) {
            case (0):
                audioPanel.SetActive(true);
                break;
            case (1):
                prefPanel.SetActive(true);
                break;
            case (2):
                truthPanel.SetActive(true);
                equivPanel.SetActive(false);
                infPanel.SetActive(false);
                break;
            case (3): 
                equivPanel.SetActive(true);
                infPanel.SetActive(false);
                truthPanel.SetActive(false);
                break;
            case (4): 
                infPanel.SetActive(true);
                equivPanel.SetActive(false);
                truthPanel.SetActive(false);
                break;
        }
        
    }
    
    public void closePanel(int panelNum) {
        
            switch (panelNum) {
                case (0):
                    audioPanel.SetActive(false);
                    break;
                case (1):
                    prefPanel.SetActive(false);
                    break;
                case (2):
                    truthPanel.SetActive(false);
                    break;
                case (3): 
                    equivPanel.SetActive(false);
                    break;
                case (4): 
                    infPanel.SetActive(false);
                    break;
            }
    }

}
