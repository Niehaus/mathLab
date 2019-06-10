using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GridGenerator : MonoBehaviour {
    private GridTile[,] grid;

    private List<GridTile> gridTiles;
    private List<float> xValues;
    private List<float> yValues;
    public InputField[] inputs;
    public GameObject endGame;

    private Hashtable hashResult; 

    public string result = "Grid:\n";
    private int[] acertos = new int[4];
    private int certo;
    public GridTile[,] Grid {
        get {
            return grid;
        }
    }

    void Start() {
        hashResult = new Hashtable();
        gridTiles = new List<GridTile>();
        xValues = new List<float>();
        yValues = new List<float>();
        foreach (Transform child in transform) {
            bool wall = false;
            if (child.tag == "True") {
                wall = true;
            }
            gridTiles.Add(new GridTile(child.position.x, child.position.y, wall));
            if (!xValues.Contains(child.position.x)) {
                xValues.Add(child.position.x);
            }
            if (!yValues.Contains(child.position.y)) {
                yValues.Add(child.position.y);
            }
        }
        xValues.Sort();
        yValues.Sort();
        yValues.Reverse();
        float[] xValuesSorted = xValues.ToArray();
        float[] yValuesSorted = yValues.ToArray();
        grid = new GridTile[xValues.Count, yValues.Count];
        
        for (int y = 0; y < yValuesSorted.Length; y++) {
            for (int x = 0; x < xValuesSorted.Length; x++) {
                foreach (GridTile tile in gridTiles) {
                    if (tile.x == xValuesSorted[x] && tile.y == yValuesSorted[y]) {
                        grid[x, y] = tile;
                        if (tile.wall) {
                            result += "1 ";
                        } else {
                            result += "0 ";
                        }
                    }
                }
            }
            result += "\n";
        }
        Debug.Log(result); //vê index do Grid
        for (int i = 0; i < result.Length; i++) {
           // Debug.Log("matriz " + i + "resultado " + result[i]);
        }
        //Declaração da hash de respota
            hashResult.Add(0, 6);
            hashResult.Add(1, 17);
            hashResult.Add(2, 28);
            hashResult.Add(3, 39);
            hashResult.Add(4, 50);
            hashResult.Add(5, 61);
            hashResult.Add(6, 72);
            hashResult.Add(7, 83);
       // Debug.Log(result[6]);
        /// Debug.Log(result[25]);
       
    }

    public void input2(int inputNum) {
        Debug.Log("input " + inputNum);
        Debug.Log("resultado " + result[hashResult[inputNum].GetHashCode()]);
        if (inputs[inputNum].text.Equals("v")) {
            if (result[hashResult[inputNum].GetHashCode()].ToString().Equals("1")) {
                //Debug.Log("resposta certa 1");
                inputs[inputNum].image.color = new Color32(69, 202, 35, 255);
                acertos[inputNum] = 1;
            } else {
                //Debug.Log("resposta errada");
                inputs[inputNum].image.color = new Color32(202, 41, 49, 255);
                acertos[inputNum] = 0;
            }
        } else if (inputs[inputNum].text.Equals("f")) {
            if (result[hashResult[inputNum].GetHashCode()].ToString().Equals("0")) {
                //Debug.Log("resposta certa 1");
                inputs[inputNum].image.color = new Color32(69, 202, 35, 255);
                acertos[inputNum] = 1;
            } else {
                //Debug.Log("resposta errada");
                inputs[inputNum].image.color = new Color32(202, 41, 49, 255);
                acertos[inputNum] = 0;
            }
        }

        if (acertos[0] == 1 && acertos[1] == 1 && acertos[2] == 1 && acertos[3] == 1) {//contabiliza acertos
            endGame.SetActive(true);
            //Debug.Log("fim de jogo");
        }
        
    }

}
