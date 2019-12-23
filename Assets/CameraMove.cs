using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Transform cameraTrans;
    [SerializeField] Transform playerTrans;
    [SerializeField] Vector3 cameraVec;
    [SerializeField] Vector3 cameraRot;
    [SerializeField] float speed = 1;
    void Start ()
    {
        cameraTrans = transform;
        cameraTrans.rotation = Quaternion.Euler (cameraRot);
    }
    void LateUpdate ()
    {
        cameraTrans.position = Vector3.Lerp (cameraTrans.position, playerTrans.position + cameraVec, speed * Time.deltaTime);
    }
}