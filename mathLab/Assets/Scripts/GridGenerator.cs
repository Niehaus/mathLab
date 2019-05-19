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
    public string result = "Grid:\n";
    public GridTile[,] Grid {
        get {
            return grid;
        }
    }

    void Start() {
        gridTiles = new List<GridTile>();
        xValues = new List<float>();
        yValues = new List<float>();
        foreach (Transform child in transform) {
            bool wall = false;
            if (child.tag == "Wall") {
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
        Debug.Log(result);
        for (int i = 0; i < result.Length; i++) {
            Debug.Log("matriz " + i + "resultado " + result[i]);
        }      

    }

    public void input(int inputNumber) {
        switch (inputNumber) {
            case (0):

                Debug.Log("resposta = " + result[6]);
                Debug.Log("input = " + inputs[0].text);
                
                if (inputs[0].text.Equals("v")) {
                    if (result[6].ToString().Equals("1")) {
                        Debug.Log("resposta certa 1");
                        inputs[0].image.color = new Color32(69, 202, 35, 255); 
                    } else {
                        Debug.Log("resposta errada");
                        inputs[0].image.color = new Color32(202, 41, 49, 255);
                    }
                } else if (inputs[0].text.Equals("f")) {
                    if (result[6].ToString().Equals("0")) {
                        Debug.Log("resposta certa 1");
                        inputs[0].image.color = new Color32(69, 202, 35, 255);
                    } else {
                        Debug.Log("resposta errada");
                        inputs[0].image.color = new Color32(202, 41, 49, 255);
                    }
                }

                break;
            case (1):
                Debug.Log("resposta = " + result[13]);
                Debug.Log("input = " + inputs[1].text);

                if (inputs[1].text.Equals("v")) {
                    if (result[13].ToString().Equals("1")) {
                        Debug.Log("resposta certa 2");
                        inputs[1].image.color = new Color32(69, 202, 35, 255);
                    } else {
                        Debug.Log("resposta errada");
                        inputs[1].image.color = new Color32(202, 41, 49, 255);
                    }
                } else if (inputs[1].text.Equals("f")) {
                    if (result[13].ToString().Equals("0")) {
                        Debug.Log("resposta certa 2");
                        inputs[1].image.color = new Color32(69, 202, 35, 255);
                    } else {
                        Debug.Log("resposta errada");
                        inputs[1].image.color = new Color32(202, 41, 49, 255);
                    }
                }
                break;
            case (2):
                Debug.Log("resposta = " + result[20]);
                Debug.Log("input = " + inputs[2].text);

                if (inputs[2].text.Equals("v")) {
                    if (result[20].ToString().Equals("1")) {
                        Debug.Log("resposta certa 3");
                        inputs[2].image.color = new Color32(69, 202, 35, 255);
                    } else {
                        Debug.Log("resposta errada");
                        inputs[2].image.color = new Color32(202, 41, 49, 255);
                    }
                } else if (inputs[2].text.Equals("f")) {
                    if (result[20].ToString().Equals("0")) {
                        Debug.Log("resposta certa 3");
                        inputs[2].image.color = new Color32(69, 202, 35, 255);
                    } else {
                        Debug.Log("resposta errada");
                        inputs[2].image.color = new Color32(202, 41, 49, 255);
                    }
                }
                break;
            case (3):
                
                if (inputs[3].text.Equals("v")) {
                    if (result[28].ToString().Equals("1")) {
                        Debug.Log("resposta certa 4");
                        inputs[3].image.color = new Color32(69, 202, 35, 255);
                    } else {
                        Debug.Log("resposta errada");
                        inputs[3].image.color = new Color32(202, 41, 49, 255);
                    }
                } else if (inputs[3].text.Equals("f")) {
                    if (result[28].ToString().Equals("0")) {
                        Debug.Log("resposta certa 4");
                        inputs[3].image.color = new Color32(69, 202, 35, 255);
                    } else {
                        Debug.Log("resposta errada");
                        inputs[3].image.color = new Color32(202, 41, 49, 255);
                    }
                }
                break;
        }

    }

}
