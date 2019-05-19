using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile {

    public float x;
    public float y;
    public bool wall;

    public GridTile(float x, float y, bool wall) {
        this.x = x;
        this.y = y;
        this.wall = wall;
    }
}