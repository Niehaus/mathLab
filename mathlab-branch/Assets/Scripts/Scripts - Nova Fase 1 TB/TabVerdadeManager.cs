using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabVerdadeManager : MonoBehaviour {
    
    public Switch[] switchVector;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var validSwitch in switchVector) {
            if (validSwitch.GetComponent<Animator>().GetBool("on")) {
                Debug.Log("switch ativo " + validSwitch.variavel_ref);
            }
        }
    }
}
