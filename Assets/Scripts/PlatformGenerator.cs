using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour {
    [SerializeField] private GameObject _platformPrefab;
    

    public void CreatePlatform() {
        Instantiate(_platformPrefab, new Vector3(0f, 0f, transform.position.z + 121.5f), Quaternion.identity);
        Destroy(gameObject.transform.parent.gameObject, 2f);
    }
}
