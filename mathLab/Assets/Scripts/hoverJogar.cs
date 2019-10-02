using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hoverJogar : MonoBehaviour
{
    
    public GameObject popGame;
    public void Start()
    {
        popGame.SetActive(false);
    }

    public void OnMouseOver()
    {
        popGame.SetActive(true);
    }
    public void OnMouseExit()
    {
        popGame.SetActive(false);
    }
}
