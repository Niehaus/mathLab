using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameMasterTable : MonoBehaviour
{

    
    public GameObject jogarButton, tablePanel, mainText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void jogarTable() {
        mainText.SetActive(false);
        tablePanel.SetActive(true); // desativa gameobjet e childrens
        jogarButton.SetActive(false); 
    }
}
