using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    #region Variables
    public GameObject cameraArm;
    private Vector3 _cameraOffset;

    [Range(0.01f, 1.0f)]
    public float smoothFactor = 0.5f;

    public float rotationSpeed;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _cameraOffset = transform.position - cameraArm.transform.position;
    }

    void LateUpdate()
    {
        Quaternion camAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);

        _cameraOffset = camAngle * _cameraOffset;

        Vector3 newPos = cameraArm.transform.position + _cameraOffset;

        transform.position = Vector3.Slerp(transform.position, newPos, smoothFactor);
        transform.LookAt(cameraArm.transform.position);
    }
}
