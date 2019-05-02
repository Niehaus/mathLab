using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class flipCardMenu : MonoBehaviour
{
    [SerializeField]
    private int flip;

    public Sprite cardDown;
    public Sprite cardFace;

    public Image imagem_atual;

    private void Start() {
        flip = 1;
    }
    public void flippy() {
        if (flip == 0) {
            flip = 1;
        } else if (flip == 1) {
            flip = 0;
        }

        if (flip == 0)
            imagem_atual.sprite = cardFace;
        else if (flip == 1)
            imagem_atual.sprite = cardDown;
    }
}
