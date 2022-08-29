using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FailController : MonoBehaviour {
    [SerializeField] private GameObject _looseVFX;
    [SerializeField] private GameObject _end;
    [SerializeField] private Text _bestScore;

    private bool _isLoose = false;
    private bool _isVibration;
    private void Start() {
        _end.SetActive(false);
        _bestScore.enabled = false;
    }

    private void Update() {
        if(_isVibration == true) Handheld.Vibrate();
        if (_isLoose) {
            _end.SetActive(true);
            _bestScore.enabled = true;
            _bestScore.text = "BEST SCORE\n<size=40>" + PlayerPrefs.GetInt("BestScore").ToString() + "</size>\nTRY DO BETTER!";
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Obstacle")) {
            //Debug.Log("LOOOOOOOOOOSE!!!");
            Instantiate(_looseVFX, transform.position + Vector3.up*1.5f, Quaternion.identity);
            PlayerController.speed = 0;
            _isLoose = true;
        }
    }
    
    public void PlayVibration(float time)
    {
        StartCoroutine(VibrationTime(time));
    }


    private IEnumerator VibrationTime(float time)
    {
        _isVibration = true;
        yield return new WaitForSecondsRealtime(time);
        _isVibration = false;
    }
    
    public void RestartGame() {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
