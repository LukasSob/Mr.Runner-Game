using UnityEngine;
using DG.Tweening;
using EZCameraShake;

public class MoveCamera : MonoBehaviour {

    public Transform player;

    void Update() {
        transform.position = player.transform.position;
    }

    public void DoFov(float endValue)
    {
        GetComponent<Camera>().DOFieldOfView(endValue, 0.25f);
    }

    public void DoTilt(float zTilt)
    {
        transform.DOLocalRotate(new Vector3(0, 0, zTilt), 0.25f);
    }

    public void ShakeCamera()
    {
        CameraShaker.Instance.ShakeOnce(2f, 10f, 0.1f, 1f);
    }

}
