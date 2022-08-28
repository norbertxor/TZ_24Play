using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private GameObject _parrent;
    public float speed = 5f;
    
    void Update() {
        transform.Translate(0f,0f, speed * Time.deltaTime);
    }
    
    

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Respawn")) 
            other.GetComponent<PlatformGenerator>().CreatePlatform();
    }


}
