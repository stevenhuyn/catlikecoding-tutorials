using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SlowCameraRotation : MonoBehaviour
{
    public float yRotationSpeed;
    public float xRotationSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(xRotationSpeed * Time.deltaTime,0f, 0f);
    }
}
