using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformGenerator : MonoBehaviour {
    [SerializeField] private GameObject _platformPrefab;
    [SerializeField] private GameObject _obstaclePrefab;
    [SerializeField] private GameObject _collectPrefab;
    
    private const float DELTAZ = 0.01583336f;

    private bool _isCreatedPlatform = false;
    private bool _isCreatedObstacle = false;
    private bool _isCreatedCollect = false;


    //генеруєм перешкоди на новій платформі, в залежності від зібраної кількості кубів
    private void GenerateObstacle(GameObject parrent) {
        if(_isCreatedObstacle) return;
        for (float i = -0.4f; i < 0.6f; i += 0.2f) {
            int maxCubes = Random.Range(0, PlayerController.currentCubes);
            for (int j = 0; j <= maxCubes; j++) {
                GameObject obj = Instantiate(_obstaclePrefab) as GameObject;
                obj.transform.parent = parrent.transform;
                obj.transform.localPosition = new Vector3(i, j/5f, DELTAZ);
                obj.transform.parent = null;
            }
        }
        _isCreatedObstacle = true;
    }

    //генеруєм куби для підбору
    private void GenerateCollect(GameObject parrent) {
        if (_isCreatedCollect) return;
        for (float i = 0.2f; i < 1; i+=0.3f) {
            GameObject obj = Instantiate(_collectPrefab) as GameObject;
            obj.transform.parent = parrent.transform;
            obj.transform.localPosition = new Vector3(Random.Range(-0.4f, 0.4f), 0.095f, i);
            obj.transform.parent = null;
        }
        _isCreatedCollect = true;
    }
    
    //генеруєм платформу повністю
    public void CreatePlatform() {
        if(_isCreatedPlatform) return;
        GameObject parrent = Instantiate(_platformPrefab, new Vector3(0f, 0f, transform.position.z + 121.5f), Quaternion.identity) as GameObject;
        GenerateCollect(parrent);
        GenerateObstacle(parrent);
        _isCreatedPlatform = true;
    }
}
