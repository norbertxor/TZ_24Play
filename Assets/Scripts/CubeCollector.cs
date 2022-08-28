using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeCollector : MonoBehaviour {
   private bool _isCollected = false;
   private bool _isObstacle = false;

   private void OnTriggerEnter(Collider other) {

      if (other.CompareTag("Obstacle") && !_isObstacle) {
         _isObstacle = true;
         transform.parent = null;
         PlayerController.cubesToOut++;
         PlayerController.collectedCubes--;
      }
   }
   public void SetCollectStatus(bool status) {
      _isCollected = status;
   }
   public bool GetCollectStatus() {
      return _isCollected;
   }
}
