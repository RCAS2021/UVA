using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target; // Transform do alvo
    public Vector3 offset; // Desvio

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset; // Move a câmera para seguir o alvo
    }
}
