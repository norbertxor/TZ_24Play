using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public static float cubesToOut = 0;
    public static int collectedCubes = 1;
    [SerializeField] private GameObject _parrent;
    [SerializeField] private Animator _animator;
    public float speed = 5f;

    void Update() {
        transform.Translate(0f,0f, speed * Time.deltaTime);
        Debug.Log(collectedCubes);
    }
    
    

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Respawn")) 
            other.GetComponent<PlatformGenerator>().CreatePlatform();
        //підбираєм куби
        if (!other.GetComponent<CubeCollector>().GetCollectStatus()) {
            other.GetComponent<CubeCollector>().SetCollectStatus(true);
            _parrent.transform.localPosition += new Vector3(0,1f,0);
            other.transform.parent = _parrent.transform;
            other.transform.localPosition = new Vector3(0f,_parrent.transform.position.y * -1f+0.5f,0f);
            _animator.SetTrigger("Jump");
            collectedCubes++;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Obstacle")) {
            _parrent.transform.localPosition -= new Vector3(0, cubesToOut, 0);
            cubesToOut = 0;
        }
    }

}
