using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformGenerator : MonoBehaviour {
    [SerializeField] private GameObject _platformPrefab;
    [SerializeField] private GameObject _obstaclePrefab;
    [SerializeField] private GameObject _collectPrefab;

    private readonly float DELTAZ = 0.01583336f;
    private void Awake() {
        for (float i = -0.4f; i < 0.6f; i += 0.2f) {
            int maxCubes = Random.Range(0, PlayerController.collectedCubes);
            Debug.Log(maxCubes);
            for (int j = 0; j <= maxCubes; j++) {
                GameObject obj = Instantiate(_obstaclePrefab);
                obj.transform.parent = _platformPrefab.transform;
                obj.transform.localPosition = new Vector3(i, j/5f, DELTAZ);
            }
        }
    }
    public void CreatePlatform() {
        Instantiate(_platformPrefab, new Vector3(0f, 0f, transform.position.z + 121.5f), Quaternion.identity);
        Destroy(gameObject.transform.parent.gameObject, 2f);
    }
}
