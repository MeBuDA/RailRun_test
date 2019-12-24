using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Transform cameraTrans;
    private Vector3 vector3;
    [SerializeField] Transform playerTrans;
    [SerializeField] Vector3 cameraVec;
    [SerializeField] Vector3 cameraRot;
    [SerializeField] float speed = 1;
    void Start ()
    {
        cameraTrans = transform;
    }
    void LateUpdate ()
    {
        vector3 = Vector3.Lerp (cameraTrans.position, playerTrans.position + cameraVec, speed * Time.deltaTime);
        vector3.y = playerTrans.position.y + cameraVec.y;
        cameraTrans.position = vector3;
        cameraTrans.rotation = Quaternion.Euler(Vector3.Scale((playerTrans.rotation.eulerAngles + cameraRot) , Vector3.up));
    }
}