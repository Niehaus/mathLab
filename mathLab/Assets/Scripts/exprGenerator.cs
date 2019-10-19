using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class exprGenerator : MonoBehaviour {


    public Text countProp, numLinhas;
   // public GameObject countProp2;
    //private GameObject[] instance;
    private string[] expr;
    public InputField mainInput;
    private int count = 0;
    //private static int i = 0;
   // private Transform ResultadoTransform;

    void Start() {
        //ResultadoTransform = GameObject.FindWithTag("TabelaPai").transform;
    }
    public void countExpr(){
        count = 0;
        expr = splitString(mainInput.text.ToLower());
        foreach (var item in expr) {
            if (item != null) {
                count++;
            }
        }
        countProp.text = count.ToString();
        numLinhas.text = Math.Pow(2, count).ToString();
       // instance[i] = Instantiate(countProp2, new Vector3(0.0072868f, -42.284f, 0), Quaternion.identity);
       // instance[i].transform.SetParent(ResultadoTransform, false);
    }

    public String[] splitString(string expr) { //separa a string a partir dos operadores - get variaveis
        int k = 0;
        string[] retornaArray = new String[32];
        string[] multiArray = expr.Split(new Char[] { ' ', '^', '|', '(', ')', '[', ']' });
        foreach (string author in multiArray) {
            if (author.Trim() != "") {
                retornaArray[k] = author;
                k++;
            }
        }
        for (int i = 0; i < retornaArray.Length; i++) {
            if (retornaArray[i] == ("->")) {//explit -> 
                retornaArray[i] = null;
            }
        }
        for (int i = 0; i < retornaArray.Length; i++) {
            for (int j = i + 1; j < retornaArray.Length; j++) {
                if (retornaArray[i] == retornaArray[j]) {
                    retornaArray[j] = null;
                }
            }
        }
        foreach (var item in retornaArray) {
            if (item !=  null) {
                Debug.Log(item);
            }
        }
        return retornaArray;
    }

}
