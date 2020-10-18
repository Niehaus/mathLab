using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTransitions : MonoBehaviour
{
    private CameraController _cam;
    public Vector2 newMinPos;
    public Vector2 newMaxPos;
    public Vector3 movePlayer;

    public VectorValue vectorValue;
    // Start is called before the first frame update
    private void Start()
    {
        _cam = Camera.main.GetComponent<CameraController>();
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Player")) return;
        _cam.minPosition = newMinPos;
        _cam.maxPosition = newMaxPos;
        
        other.transform.position += movePlayer;
    }
}
