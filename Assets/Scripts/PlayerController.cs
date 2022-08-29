using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    public static float cubesToOut = 0;
    public static int currentCubes = 1;
    public static float speed = 7f;
    
    [SerializeField] private GameObject _cubesHolder;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _collectVFX;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _startText;
    
    
    private Dictionary<int, Vector2> _activeTouches = new Dictionary<int, Vector2>();
    private int _scoreCount = 0;
    private bool _isStarted = false;

    void Update() {
        //Перед початком гри
        if (!_isStarted) {
            _startText.text = "HOLD TO MOVE";
            speed = 0f;
            if (Input.touchCount > 0) {
                _isStarted = true;
                speed = 7f;
            }
        }
        
        if (_isStarted) {
            _startText.enabled = false;
            _scoreText.enabled = true;
            transform.Translate(0f, 0f, speed*Time.deltaTime);
            //float x = Input.GetAxis("Horizontal")*speed*Time.deltaTime;
            Vector3 x = GetPlayerInput();
            transform.Translate(x.x, 0f, 0f);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -2, 2), transform.position.y,
                transform.position.z);
            _scoreText.text = $"SCORE:{_scoreCount}";
        }
        
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Respawn")) 
            other.GetComponent<PlatformGenerator>().CreatePlatform();

        //підбираєм куби
        if (other.CompareTag("Collect") && !other.GetComponent<CubeCollector>().GetCollectStatus()) {
            other.GetComponent<CubeCollector>().SetCollectStatus(true);
            Instantiate(_collectVFX, _cubesHolder.transform.position + Vector3.up*3f, quaternion.identity);
            _cubesHolder.transform.localPosition += new Vector3(0,1f,0);
            other.transform.parent = _cubesHolder.transform;
            other.transform.localPosition = new Vector3(0f,_cubesHolder.transform.position.y * -1f+0.5f,0f);
            _animator.SetTrigger("Jump");
            currentCubes++;
            _scoreCount++;
            if(_scoreCount > PlayerPrefs.GetInt("BestScore"))
                PlayerPrefs.SetInt("BestScore", _scoreCount);
        }
    }
    
    //скидаєм лишні куби збільшуєм швидкість
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Obstacle")) {
            _cubesHolder.transform.localPosition -= new Vector3(0, cubesToOut, 0);
            cubesToOut = 0;
            speed += 0.5f;
        }
    }
    
    //отримуєм координати пальця
    public Vector3 GetPlayerInput() {
        Vector3 r = Vector3.zero;
        foreach (Touch touch in Input.touches) {
            if (touch.phase == TouchPhase.Began) {
                //Debug.Log("START TOUCH");
                _activeTouches.Add(touch.fingerId, touch.position);
            }
            else if (touch.phase == TouchPhase.Ended) {
                //Debug.Log("END TOUCH");
                if (_activeTouches.ContainsKey(touch.fingerId)) {
                    _activeTouches.Remove(touch.fingerId);
                }
            }
            else {
                float mag = 0;
                r = (touch.position - _activeTouches[touch.fingerId]);
                mag = r.magnitude / 400;
                r = r.normalized * mag;
            }
        }
        return r;
    }
}

