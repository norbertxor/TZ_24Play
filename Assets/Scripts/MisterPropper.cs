using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisterPropper : MonoBehaviour {
    
    private void OnTriggerEnter(Collider other) {
        Destroy(other.gameObject);
    }
}
