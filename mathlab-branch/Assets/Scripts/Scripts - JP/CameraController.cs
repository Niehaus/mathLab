using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    
    public Transform target;
    public float smoothing; 
    public Vector2 minPosition;
    public Vector2 maxPosition;
    
    public VectorValue vectorValue;

    private void Start() {
        
    }

    // Update is called once per frame after Update function
    void LateUpdate() {
        //transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);   acompanha o player

        if (transform.position == target.position
        ) return; //dá um certo smooth movement à câmera, a faz andar atrás do player, e colocá-la sobre ele quando ele parar de andar(followup)
            
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

        if (targetPosition.x <= -7.74f && targetPosition.y >= -9f) {
            minPosition = new Vector2(-27f, -4.2f );
            maxPosition = new Vector2(-16.21f, 4.63f);
        }
        else if (targetPosition.x <= 28.15f && targetPosition.x >= 5f) {
            minPosition = new Vector2(12.5f, -4.6f );
            maxPosition = new Vector2(21.6f, 1.3f);
        }
        else if (targetPosition.x >= -10.41f && targetPosition.y >= -7f  ){
            minPosition = new Vector2(-6.4f, -6.45f );
            maxPosition = new Vector2(2.85f, 1.06f);
        }
        else if (targetPosition.y >= -22f && targetPosition.y <= -6.36f  ){
            minPosition = new Vector2(-4.28f, -20.83f );
            maxPosition = new Vector2(-0.28f, -12.1f);
        }
        
        
        targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x); //pega um valor minimo e maximo e retorna um outro valor entre ambos
        targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);
        /*minPosition = vectorValue.minPosition;
        maxPosition = vectorValue.maxPosition;*/
        
        
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
    }
}
