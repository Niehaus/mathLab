using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial : MonoBehaviour
{
    public GameObject instrutor;
    public void iniciaTutorial(){
        Instantiate(instrutor, new Vector3(161.5373f, 161.5373f, 1f), Quaternion.identity);

    }
}
