using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    
    public Transform target;
    public float smoothing; 
    public Vector2 minPosition;
    public Vector2 maxPosition;

    // Update is called once per frame after Update function
    void LateUpdate() {
        //transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);   acompanha o player

        if (transform.position == target.position
        ) return; //dá um certo smooth movement à câmera, a faz andar atrás do player, e colocá-la sobre ele quando ele parar de andar(followup)
            
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            
        targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x); //pega um valor minimo e maximo e retorna um outro valor entre ambos
        targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
    }
}
