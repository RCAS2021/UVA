using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobbingAnimation : MonoBehaviour
{
    public float frequency; // Velocidade de movimento
    public float magnitude; // Distância do movimento
    public Vector3 direction; // Direção do movimento
    Vector3 initialPosition; // Posição inicial
    Pickup pickup; // Item

    void Start()
    {
        pickup = GetComponent<Pickup>();
        initialPosition = transform.position;
    }

    void Update()
    {
        if(pickup && !pickup.hasBeenCollected)
        {
            transform.position = initialPosition + direction * Mathf.Sin(Time.time * frequency) * magnitude; // Mathf.Sin() Gera uma onda movendo de -1 para 1
        }
    }
}
