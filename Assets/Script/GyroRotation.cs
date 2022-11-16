using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class GyroRotation : MonoBehaviour
{
    private void Start() {
        Input.gyro.enabled = true;
    }

    private void Update() {
        // this.transform.rotation = Input.gyro.attitude;
        // transform.Rotate(0, 0, 180, Space.Self);
        // transform.Rotate(90, 180, 0, Space.World);

        this.transform.rotation = Quaternion.Euler(90f, 90f, 0f) * Input.gyro.attitude * new Quaternion(0, 0, 1, 0);
    }
}