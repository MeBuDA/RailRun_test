using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFOV : MonoBehaviour
{
    [Range(0, 2)] [SerializeField] float Value;//FOV切り替え閾値
    private Camera mainCamera;
    private float speed = 0;//速度
    private bool tig = true;
    void Start()
    {
        mainCamera = this.GetComponent<Camera>();
    }
    void FixedUpdate()
    {
        var pos = Vector3.Scale(transform.position, new Vector3(1, 0, 1));
        var diff = (pos.magnitude - speed);
        if (diff > Value && tig)
        {
            tig = false;
            LeanTween.value(gameObject, 0f, 1f, 0.5f).setOnUpdate((float val) =>
            {
                mainCamera.fieldOfView += val;
            }).setOnComplete(tweenFinished).setOnComplete(() => tig = true);
        }
        speed = pos.magnitude;
        Debug.Log(diff);
    }
    void tweenFinished()
    {
        LeanTween.value(gameObject, 0f, 1f, 0.2f).setOnUpdate((float val) =>
        {
            mainCamera.fieldOfView -= val;
        });
    }
}