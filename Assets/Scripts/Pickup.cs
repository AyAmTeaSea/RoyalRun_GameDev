using System;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Pickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            OnPickup();
            Destroy(gameObject);
        }
    }
    
    protected abstract void OnPickup();
}
