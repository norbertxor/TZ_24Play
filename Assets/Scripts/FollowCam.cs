using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {
    public Transform target;
    public float smoothTime = 0.5f;
    public float maxH = 12f;
    [Header("Відстань слідкування пл висоті")] 
    [Range (3,6)]public float deltaY = 5f;
    [Header("Відстань слідкування за об'єктом")]
    [Range (5,10)] public float deltaZ = 10f;

    private Vector3 _velocity = Vector3.zero;

    void LateUpdate() {
        Vector3 targetPosition = new Vector3(transform.position.x, target.position.y+deltaY, target.position.z-deltaZ);
        if (transform.position.y > maxH)
            targetPosition.y = maxH;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);
        
            
    }
}
