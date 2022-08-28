using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCollector : MonoBehaviour {
   [SerializeField] private GameObject _parrent;
   [SerializeField] private Animator _animator;

   private void OnTriggerEnter(Collider other) {
      if (other.CompareTag("Player")) {
         _parrent.transform.localPosition += new Vector3(0,1f,0);
         gameObject.transform.parent = _parrent.transform;
         transform.localPosition = new Vector3(0f,_parrent.transform.position.y * -1f+0.5f,0f);
         _animator.SetTrigger("Jump");
      }
   }
}
