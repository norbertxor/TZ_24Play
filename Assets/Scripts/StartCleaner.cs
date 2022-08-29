using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCleaner : MonoBehaviour {
    [SerializeField] private GameObject startEnvirmoment;

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player"))
           Destroy(startEnvirmoment, 2f);
    }
}
